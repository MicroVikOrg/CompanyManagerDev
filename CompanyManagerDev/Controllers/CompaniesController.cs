using CompanyManagerDev.Models.Db;
using CompanyManagerDev.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CompanyManagerDev.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ApplicationContext db;
        private readonly IKafkaProducer _kafkaProducer;
        public CompaniesController(ApplicationContext context, IKafkaProducer kafkaProducer)
        {
            db = context;
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company companyModel)
        {
            companyModel.Id = Guid.NewGuid();
            companyModel.CreatedAt = DateTime.Now;
            await _kafkaProducer.ProduceMessage("NewCompanies", JsonConvert.SerializeObject(companyModel));
            await db.Companies.AddAsync(companyModel);
            await db.SaveChangesAsync();
            return Ok(companyModel);
        }
        [HttpPost]
        [Route("employees")]
        public async Task<IActionResult> AddEmployee(string companyId, string userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            if (user == null) BadRequest();
            var company = await db.Companies.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) BadRequest();
            var usersCompanies = new UsersCompanies() { Company = company!, CompanyId = company!.Id, User = user!, UserId = user!.Id };
            await db.UsersCompanies.AddAsync(usersCompanies);
            await db.SaveChangesAsync();
            return Ok(usersCompanies);
        }
        [HttpDelete]
        [Route("employees")]
        public async Task<IActionResult> DeleteEmployee(string userId, string companyId)
        {
            var user = await db.Users.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            if (user == null) BadRequest();
            var company = await db.Companies.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) BadRequest();
            var usersCompanies = new UsersCompanies() { Company = company!, CompanyId = company!.Id, User = user!, UserId = user!.Id };
            db.UsersCompanies.Remove(usersCompanies);
            await db.SaveChangesAsync();
            return Ok(usersCompanies);
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies(string userId)
        {
            var companies = db.UsersCompanies.Where(e => e.UserId == Guid.Parse(userId));
            if (companies == null) BadRequest();
            return Ok(companies);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] Company companyModel)
        {
            db.Companies.Update(companyModel);
            await db.SaveChangesAsync();
            return Ok(companyModel);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCompany([FromBody] Company company)
        {
            db.Companies.Remove(company);
            await db.SaveChangesAsync();
            return Ok(company);
        }
    }
}

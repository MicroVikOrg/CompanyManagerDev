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
        private readonly DbManager<Company> dbManager;
        public CompaniesController(ApplicationContext context, IKafkaProducer kafkaProducer, IDbManagerFactory managerFactory)
        {
            db = context;
            _kafkaProducer = kafkaProducer;
            dbManager = managerFactory.GetDbManager<Company>();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] Company companyModel)
        {
            companyModel.Id = Guid.NewGuid();
            companyModel.CreatedAt = DateTime.UtcNow;
            await dbManager.SaveAsync(companyModel, "NewCompanies");
            return Ok();
        }
        [HttpPost]
        [Route("employees")]
        public async Task<IActionResult> AddEmployee(string companyId, string userId)
        {
            var user = await db.User.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            if (user == null) return BadRequest();
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) return BadRequest();
            var usersCompanies = new UsersCompanies() { Company = company!, CompanyId = company!.Id, User = user!, UserId = user!.Id };
            await db.UsersCompanies.AddAsync(usersCompanies);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("employees")]
        public async Task<IActionResult> DeleteEmployee(string userId, string companyId)
        {
            var user = await db.User.FirstOrDefaultAsync(e => e.Id == Guid.Parse(userId));
            if (user == null) return BadRequest();
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) return BadRequest();
            var usersCompanies = new UsersCompanies() { Company = company!, CompanyId = company!.Id, User = user!, UserId = user!.Id };
            db.UsersCompanies.Remove(usersCompanies);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies(string userId)
        {
            var companies = db.UsersCompanies.Where(e => e.UserId == Guid.Parse(userId));
            if (companies == null) return BadRequest();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCompany([FromBody] Company companyModel)
        {
            db.Company.Update(companyModel);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCompany(string companyId)
        {
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) return BadRequest();
            db.Company.Remove(company);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}

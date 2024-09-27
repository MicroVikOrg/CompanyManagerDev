using CompanyManagerDev.Models.Db;
using CompanyManagerDev.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CompanyManagerDev.Controllers
{
    [Route("api/companies/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IDbManager<Role> _dbManager;
        private ApplicationContext db;

        public RolesController(ApplicationContext context, IDbManagerFactory managerFactory)
        {
            db = context;
            _dbManager = managerFactory.GetDbManager<Role>();
        }
        [HttpGet]
        public async Task<IActionResult> GetRoles(string companyId)
        {
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == Guid.Parse(companyId));
            if (company == null) return BadRequest();
            return Ok(company.Roles);
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == role.CompanyId);
            if (company == null) return BadRequest();
            await _dbManager.SaveAsync(role);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            var company = await db.Company.FirstOrDefaultAsync(e => e.Id == role.CompanyId);
            if (company == null) return BadRequest();
            await _dbManager.UpdateAsync(role);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await db.Role.FirstOrDefaultAsync(e => e.Id == Guid.Parse(roleId));
            if (role == null) return BadRequest();
            db.Role.Remove(role!);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}

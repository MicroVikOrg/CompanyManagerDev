using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagerDev.Models.Db
{
    [Table("companies")]
    public class Company : BaseEntity
    {
        [JsonProperty("id")]
        [Column("id"), Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("company_name")]
        [Column("company_name")]
        public required string CompanyName { get; set; }
        [JsonProperty("created_at")]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<UsersCompanies> UsersCompanies { get; set; } = new List<UsersCompanies>();
    }
}
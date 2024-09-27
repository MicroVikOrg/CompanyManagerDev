using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagerDev.Models.Db
{
    [Table("users_companies")]
    [PrimaryKey("UserId", "CompanyId")]
    public class UsersCompanies
    {
        [JsonIgnore]
        public required User User { get; set; }
        [JsonProperty("user_id")]
        [Column("user_id")]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public required Company Company { get; set; }
        [JsonProperty("company_id")]
        [Column("company_id")]
        public Guid CompanyId { get; set; }
    }
}
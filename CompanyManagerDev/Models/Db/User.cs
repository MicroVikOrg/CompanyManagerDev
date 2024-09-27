using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagerDev.Models.Db
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Key]
        [JsonProperty("id")]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("username")]
        [Column("username")]
        public required string UserName { get; set; }
        [JsonProperty("password")]
        [Column("password")]
        public required string Password { get; set; }
        [JsonProperty("email")]
        [Column("email")]
        public required string Email { get; set; }
        [JsonProperty("created_at")]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("token")]
        [Column("token")]
        public string? Token { get; set; }
        [JsonProperty("verified")]
        [Column("verified")]
        public bool Verified { get; set; }
        [JsonIgnore]
        public List<UsersCompanies> UsersCompanies { get; set; } = new List<UsersCompanies>();
    }
}
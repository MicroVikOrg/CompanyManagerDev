using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyManagerDev.Models.Db
{
    [Table("roles")]
    public class Role
    {
        [JsonProperty("id")]
        [Key, Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("name")]
        [Column("name")]
        public required string Name { get; set; }
        [JsonProperty("description")]
        [Column("description")]
        public string? Description { get; set; }
        [JsonIgnore]
        public required Company Company { get; set; }
        [JsonProperty("company_id")]
        [Column("company_id")]
        public int CompanyId { get; set; }
    }
}
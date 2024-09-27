using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JsonIgnore = Newtonsoft.Json.JsonIgnoreAttribute;
namespace CompanyManagerDev.Models.Db
{
    [Table("roles")]
    public class Role : BaseEntity
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
        [JsonPropertyName("company_id")]
        [Column("company_id")]
        public Guid CompanyId { get; set; }
    }
}
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagerDev.Models.Db
{
    public class Project : BaseEntity
    {
        [Column("id"), Key]
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(64)]
        [Column("name")]
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Column("summary")]
        [JsonProperty("summary")]
        public string? Summary { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("company_id")]
        [JsonProperty("company_id")]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}

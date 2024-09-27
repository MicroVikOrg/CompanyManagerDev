using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyManagerDev.Models.Db
{
    public abstract class BaseEntity
    {
        [Column("id"), Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }
    }
}

using CustomsClearanceCar_API.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomsClearanceCar_API.Database.Models
{
    public class Price : Identity
    {
        public int Year { get; set; }
        public string? Value { get; set; }

        public int EngineCapacityId { get; set; }


        [JsonIgnore]
        public virtual EngineCapacity EngineCapacity { get; set; } = null!;
    }
}

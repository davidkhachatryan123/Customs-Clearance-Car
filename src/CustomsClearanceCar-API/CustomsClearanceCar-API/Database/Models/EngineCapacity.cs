using CustomsClearanceCar_API.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomsClearanceCar_API.Database.Models
{
    public class EngineCapacity : Identity
    {
        public EngineCapacity()
        {
            Prices = new HashSet<Price>();
        }

        public int? Capacity { get; set; }

        public int ModelId { get; set; }


        [JsonIgnore]
        public virtual Model Model { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Price> Prices { get; set; }
    }
}

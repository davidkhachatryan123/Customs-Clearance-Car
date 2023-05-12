using CustomsClearanceCar_API.Database.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomsClearanceCar_API.Database.Models
{
    public class Model : Identity
    {
        public Model()
        {
            EngineCapacities = new HashSet<EngineCapacity>();
        }

        public string? Name { get; set; }

        public int MarkId { get; set; }


        [JsonIgnore]
        public virtual Mark Mark { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<EngineCapacity> EngineCapacities { get; set; }
    }
}

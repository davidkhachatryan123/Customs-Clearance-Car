using CustomsClearanceCar_API.Database.Base;
using System.Text.Json.Serialization;

namespace CustomsClearanceCar_API.Database.Models
{
    public class Mark : Identity
    {
        public Mark()
        {
            Models = new HashSet<Model>();
        }

        public string Name { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Model> Models { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDB_WithExcellData.Models
{
    internal class Car
    {
        public string? Mark { get; set; }
        public string? Model { get; set; }
        public string? EngineCapacity { get; set; }

        public Dictionary<int, string?> Prices { get; set; } = null!;

        public override string ToString() =>
            $"Mark: {Mark}\nModel: {Model}\nEngine Cap.: {EngineCapacity}";
    }
}

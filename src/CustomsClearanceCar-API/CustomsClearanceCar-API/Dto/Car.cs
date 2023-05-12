using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CustomsClearanceCar_API.Dto
{
    public class Car
    {
        private string _mark = null!;
        public string Mark
        {
            get => _mark;
            set => _mark = value.Trim();
        }

        private string _model = null!;
        public string? Model
        {
            get => _model;
            set => _model = value.Trim();
        }

        public int? EngineCapacity { get; set; }

        public int Year { get; set; }
    }
}

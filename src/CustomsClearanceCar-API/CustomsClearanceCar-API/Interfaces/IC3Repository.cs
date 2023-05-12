using CustomsClearanceCar_API.Dto;

namespace CustomsClearanceCar_API.Interfaces
{
    public interface IC3Repository
    {
        Task<string[]> GetMarksAsync();
        Task<string[]?> GetModelsAsync(string mark);
        Task<int[]?> GetEngineCapacitiesAsync(string mark, string? model);
        Task<string> CalculateAsync(Car car);
    }
}

using CustomsClearanceCar_API.Database;
using CustomsClearanceCar_API.Dto;
using CustomsClearanceCar_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomsClearanceCar_API.Services
{
    public class C3Repository : IC3Repository
    {
        private readonly ApplicationContext _context;

        public C3Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<string[]> GetMarksAsync()
            => await _context.Marks
            .Select(mark => mark.Name)
            .ToArrayAsync();

        public async Task<string[]?> GetModelsAsync(string mark)
            => (await _context.Models
            .Include(model => model.Mark)
            .Where(o => o.Mark.Name == mark)
            .Select(o => o.Name)
            .ToArrayAsync())!;

        public async Task<int[]?> GetEngineCapacitiesAsync(string mark, string? model)
            => await _context.EngineCapacities
            .Include(engineCap => engineCap.Model)
            .ThenInclude(model => model.Mark)
            .Where(o => o.Model.Mark.Name == mark && o.Model.Name == model)
            .Select(o => Convert.ToInt32(o.Capacity))
            .ToArrayAsync();

        public async Task<string> CalculateAsync(Car car)
            => (await _context.Prices
            .Include(price => price.EngineCapacity)
            .ThenInclude(engineCap => engineCap.Model)
            .ThenInclude(model => model.Mark)
            .Where(o
                => o.EngineCapacity.Model.Mark.Name == car.Mark
                && o.EngineCapacity.Model.Name == car.Model
                && o.EngineCapacity.Capacity == (car.EngineCapacity ?? 0)
                && o.Year == car.Year)
            .Select(o => o.Value)
            .FirstOrDefaultAsync())!;
    }
}

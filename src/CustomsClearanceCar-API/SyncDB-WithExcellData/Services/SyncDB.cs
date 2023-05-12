using CustomsClearanceCar_API.Database;
using CustomsClearanceCar_API.Database.Base;
using CustomsClearanceCar_API.Database.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SyncDB_WithExcellData.Models;

namespace SyncDB_WithExcellData.Services
{
    internal class SyncDB
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<SyncDB> _logger;

        public SyncDB(ApplicationContext context, ILogger<SyncDB> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task StartSync()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open("Files/Customs-Clearance-Car.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();

                    while (reader.Read()) // Each row of the file
                    {
                        if (reader.GetValue(0)?.ToString() != null)
                            await AddEntitysToDbAsync(new Car
                            {
                                Mark = reader.GetValue(0)?.ToString().Trim(),
                                Model = reader.GetValue(1)?.ToString().Trim(),
                                EngineCapacity = reader.GetValue(2)?.ToString().Trim(),
                                Prices = new Dictionary<int, string?>
                                {
                                    { 2023, reader.GetValue(3)?.ToString().Trim() },
                                    { 2022, reader.GetValue(4)?.ToString().Trim() },
                                    { 2021, reader.GetValue(5)?.ToString().Trim() }
                                }
                            });
                    }
                }
            }
        }

        private async Task AddEntitysToDbAsync(Car car)
        {
            _logger.LogInformation(car.ToString());

            Mark? mark =
                await _context.Marks
                .Where(mark => mark.Name == car.Mark)
                .FirstOrDefaultAsync();

            Model? model =
                await _context.Models
                .Include(model => model.Mark)
                .Where(o => o.Name == car.Model && o.Mark.Name == car.Mark)
                .FirstOrDefaultAsync();

            EngineCapacity? engineCapacity =
                await _context.EngineCapacities
                .Include(engineCap => engineCap.Model)
                .ThenInclude(model => model.Mark)
                .Where(o
                    => o.Capacity == Convert.ToInt32(car.EngineCapacity)
                    && o.Model.Name == car.Model
                    && o.Model.Mark.Name == car.Mark)
                .FirstOrDefaultAsync();

            List<Price> prices =
                await _context.Prices
                .Include(price => price.EngineCapacity)
                .ThenInclude(engineCap => engineCap.Model)
                .ThenInclude(model => model.Mark)
                .Where(o
                    => o.EngineCapacity.Capacity == Convert.ToInt32(car.EngineCapacity)
                    && o.EngineCapacity.Model.Name == car.Model
                    && o.EngineCapacity.Model.Mark.Name == car.Mark)
                .ToListAsync();

            if (mark is null)
            {
                mark = new() { Name = car.Mark! };

                await _context.Marks.AddAsync(mark);
            }

            if (model is null)
            {
                model = new() { Name = car.Model, Mark = mark! };

                await _context.Models.AddAsync(model);
            }

            if (engineCapacity is null)
            {
                engineCapacity = new()
                {
                    Capacity = Convert.ToInt32(car.EngineCapacity),
                    Model = model!
                };

                await _context.EngineCapacities.AddAsync(engineCapacity);
            }

            if (prices.Count < 1)
            {
                foreach (KeyValuePair<int, string?> price in car.Prices)
                    prices.Add(new()
                    {
                        Year = price.Key,
                        Value = price.Value,
                        EngineCapacity = engineCapacity
                    });

                await _context.Prices.AddRangeAsync(prices);
            }

            await _context.SaveChangesAsync();
        }
    }
}

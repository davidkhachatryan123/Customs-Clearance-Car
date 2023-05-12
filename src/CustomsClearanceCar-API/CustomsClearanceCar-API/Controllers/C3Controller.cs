using CustomsClearanceCar_API.Database.Models;
using CustomsClearanceCar_API.Dto;
using CustomsClearanceCar_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomsClearanceCar_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class C3Controller : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;

        public C3Controller(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet("GetMarks")]
        public async Task<IActionResult> GetMarks()
            => Ok(await _repositoryManager.C3.GetMarksAsync());

        [HttpGet("{mark}/GetModels")]
        public async Task<IActionResult> GetModels(string mark)
            => Ok(await _repositoryManager.C3.GetModelsAsync(mark));

        [HttpGet("{mark}/{model?}/GetEngineCapacities")]
        public async Task<IActionResult> GetEngineCapacities(string mark, string? model = null)
            => Ok(await _repositoryManager.C3.GetEngineCapacitiesAsync(mark, model));

        [HttpPost("Calculate")]
        public async Task<IActionResult> Calculate([FromBody] Car car)
            => Ok(new { result = await _repositoryManager.C3.CalculateAsync(car) });
    }
}

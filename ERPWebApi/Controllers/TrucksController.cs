using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TrucksWebApi.Models;
using TrucksWebApi.Services.Interfaces;

namespace TrucksWebApi.Controllers
{
    //TODO: add logging, authorisation, unit testing
    [Route("api/trucks")]
    [ApiController]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;
        private readonly IValidator<TruckDTO> _validator;

        public TrucksController(ITruckService truckService, IValidator<TruckDTO> validator)
        {
            _truckService = truckService;
            _validator = validator;
        }

        // GET: api/trucks
        [HttpGet]
        public IActionResult GetTrucks()
        {
            var trucks = _truckService.GetTrucks();
            return Ok(trucks);
        }

        // GET: api/trucks/{code}
        [HttpGet("{code}")]
        public IActionResult GetTruck(string code)
        {
            var truck = _truckService.GetTruckByCode(code);
            if (truck == null)
                return NotFound();

            return Ok(truck);
        }

        // POST: api/trucks
        [HttpPost]
        public IActionResult CreateTruck([FromBody] TruckDTO truckDTO)
        {
            var results = _validator.Validate(truckDTO);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors.Select(e => e.ErrorMessage));
            }

            var truck = _truckService.CreateTruck(truckDTO);
            return CreatedAtAction(nameof(GetTruck), new { code = truck.Code }, truck);
        }

        // PUT: api/trucks/{code}
        [HttpPut("{code}")]
        public IActionResult UpdateTruck(string code, [FromBody] TruckDTO truckDTO)
        {
            var truck = _truckService.GetTruckByCode(code);
            if (truck == null)
                return NotFound();

            var results = _validator.Validate(truckDTO);
            if (!results.IsValid)
            {
                return BadRequest(results.Errors.Select(e => e.ErrorMessage));
            }

            _truckService.UpdateTruck(code, truckDTO);
            return NoContent();
        }

        // DELETE: api/trucks/{code}
        [HttpDelete("{code}")]
        public IActionResult DeleteTruck(string code)
        {
            _truckService.DeleteTruck(code);
            return NoContent();
        }
    }
}
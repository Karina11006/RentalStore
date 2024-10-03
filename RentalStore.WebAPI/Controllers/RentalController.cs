using Microsoft.AspNetCore.Mvc;
using RentalStore.SharedKernel.Dto;
using RentalStore.Application.Services;
using RentalStore.Domain.Exceptions;
using RentalStore.Domain.Models;

namespace RentalStore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ILogger<RentalController> _logger;

        public RentalController(IRentalService rentalService, ILogger<RentalController> logger)
        {
            _rentalService = rentalService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RentalDto>> Get()
        {
            var result = _rentalService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich wypożyczeń");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetRental")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RentalDto> Get(int id)
        {
            var result = _rentalService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            _logger.LogDebug($"Pobrano wypożyczenie o id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateRentalDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _rentalService.Create(dto);
            _logger.LogDebug($"Utworzono nowe wypożyczenie z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateRentalDto dto)
        {
            try
            {
                _rentalService.Update(id, dto);
                _logger.LogDebug($"Zaktualizowano wypożyczenie z id = {id}");
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas aktualizacji wypożyczenia");
                return StatusCode(500, "Internal server error");
            }
        }



        

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _rentalService.Delete(id);
                _logger.LogDebug($"Usunięto wypożyczenie z id = {id}");
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Complete(int id)
        {
            try
            {
                _rentalService.CompleteRental(id);
                _logger.LogDebug($"Zakończono wypożyczenie z id = {id}");
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpGet("{id}/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RentalDto> GetByIdWithDetails(int id)
        {
            try
            {
                var result = _rentalService.GetByIdWithDetails(id);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
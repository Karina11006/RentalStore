using Microsoft.AspNetCore.Mvc;
using RentalStore.Application.Services;
using RentalStore.Domain.Exceptions;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryService categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            this._categoryService = categoryService;
            this._logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> Get()
        {
            var result = _categoryService.GetAll();
            _logger.LogDebug("Pobrano listę wszystkich kategorii");
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> Get(int id)
        {
            var result = _categoryService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            _logger.LogDebug($"Pobrano kategorie o id = {id}");
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = _categoryService.Create(dto);
            _logger.LogDebug($"Utworzono nową kategorie z id = {id}");
            var actionName = nameof(Get);
            var routeValues = new { id };
            return CreatedAtAction(actionName, routeValues, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryService.Delete(id);
            _logger.LogDebug($"Usunieto kategorie z id = {id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] CategoryDto dto)
        {
            if (id != dto.CategoryId)
            {
                throw new BadRequestException("Id param is not valid");
            }

            var category = _categoryService.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Update(dto);
            _logger.LogDebug($"Zaktualizowano kategoriez id = {id}");
            return NoContent();
        }
    }
}

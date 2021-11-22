using APICatalog.DTOs;
using APICatalog.Entities;
using APICatalog.Pagination;
using APICatalog.Repositories.UnitOfWork;
using APICatalog.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("ControllersWithCorsAllowed")]
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork uof, IConfiguration config, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _uof = uof;
            _config = config;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("welcome/{msg:alpha}")]
        public ActionResult<string> GetWelcome([FromServices] IFromService _service, string msg)
        {
            return _service.Default(msg);
        }

        [HttpGet("products")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetWithProduct()
        {
            _logger.LogInformation(" == Getting all categories with you products ==");
            var categories = await _uof.CategoryRepository.GetCategoryWithProductsAsync();


            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            try
            {
                var categories = await _uof.CategoryRepository.Get().ToListAsync();

                return  _mapper.Map<List<CategoryDTO>>(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying get the categories");
            }
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetWithPaginationData([FromQuery] PageParameters page)
        {
            try
            {
                var categories = await _uof.CategoryRepository.GetCategoriesAsync(page);

                var metadata = new
                {
                    categories.TotalItems,
                    categories.PageSize,
                    categories.CurrentPage,
                    categories.TotalPages,
                    categories.HasNext,
                    categories.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return _mapper.Map<List<CategoryDTO>>(categories);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying get the categories");
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> get(int id)
        {
            try
            {
                Category c = await _uof.CategoryRepository.GetById(c => id == c.CategoryId);

                return null == c ? NotFound($"The category with id = {id} was not found") : _mapper.Map<CategoryDTO>(c); ;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying get the category with id: {id}");
            }

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO c)
        {
            try
            {
                var category = _mapper.Map<Category>(c);
                _uof.CategoryRepository.Add(category);
                await _uof.Commit();

                var cSavedDTO = _mapper.Map<CategoryDTO>(category);

                return CreatedAtRoute("GetCategory", new { id = c.CategoryId }, cSavedDTO);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying create a new category");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO c)
        {
            try
            {
                if (id != c.CategoryId)
                {
                    return BadRequest($"Ids mismatch {id} <> {c.CategoryId}");
                }

                var category = _mapper.Map<Category>(c);

                _uof.CategoryRepository.Update(category);
                await _uof.Commit();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying update the category with id: {id}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {

            try
            {
                Category c = await _uof.CategoryRepository.GetById(c => id == c.CategoryId);

                if (null == c)
                {
                    NotFound($"The category with id = {id} was not found");
                }

                _uof.CategoryRepository.Delete(c);
                await _uof.Commit();

                return _mapper.Map<CategoryDTO>(c);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying delete the category with id: {id}");
            }
        }

    }
}

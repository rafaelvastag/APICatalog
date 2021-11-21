using APICatalog.DTOs;
using APICatalog.Entities;
using APICatalog.Filters;
using APICatalog.Pagination;
using APICatalog.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            // waiting this operation but not blocking the thread
            var products = await _uof.ProductRepository.Get().ToListAsync();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("pagination")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetWithPagination([FromQuery] PageParameters p)
        {
            // waiting this operation but not blocking the thread
            var products = await _uof.ProductRepository.GetProductsAsync(p);

            var metadata = new
            {
                products.TotalItems,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("by-price")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetOrderByPrice()
        {
            var products = await _uof.ProductRepository.GetByPriceAsync();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var p = await _uof.ProductRepository.GetById(p => p.ProductId == id);

            return null == p ? NotFound() : _mapper.Map<ProductDTO>(p);
        }


        // Method for Router examples.
        // Not Ignore Controller prefix route. http:....api/[Controller]/id/param2
        [HttpGet("{id}/{param2}", Name = "GetProduct2")]
        // Ignore Controller prefix route. http:..../id/param2
        [HttpGet("/noController/{id}/{param2}", Name = "GetProduct3")]
        // Param2 is optional
        [HttpGet("{id}/{param2?}", Name = "GetProduct4")]
        // Param2 has a Default value
        [HttpGet("{id}/{param2 = Mac}", Name = "GetProduct5")]
        // Restricted alphanumeric param2
        [HttpGet("{id}/{param2:alpha}", Name = "GetProduct6")]
        // Restricted alphanumeric with length 5 - param2
        [HttpGet("{id}/{param2:alpha:length(5)}", Name = "GetProduct7")]
        public async Task<ActionResult<ProductDTO>> GetRouteExample_NOT_USED(int id, string param2)
        {
            var p = await _uof.ProductRepository.GetById(p => p.ProductId == id);

            return null == p ? NotFound() : _mapper.Map<ProductDTO>(p);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO p)
        {
            var product = _mapper.Map<Product>(p);
            _uof.ProductRepository.Add(product);
            await _uof.Commit();

            var pSavedDTO = _mapper.Map<ProductDTO>(product);

            return new CreatedAtRouteResult("GetProduct", new { id = pSavedDTO.ProductId }, pSavedDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO p)
        {

            if (id != p.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(p);

            _uof.ProductRepository.Update(product);
            await _uof.Commit();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var p = await _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (null == p)
            {
                return NotFound();
            }

            _uof.ProductRepository.Delete(p);
            await _uof.Commit();

            return _mapper.Map<ProductDTO>(p);
        }
    }
}


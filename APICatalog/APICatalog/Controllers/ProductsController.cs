using APICatalog.DTOs;
using APICatalog.Entities;
using APICatalog.Filters;
using APICatalog.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace APICatalog.Controllers
{
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
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            // waiting this operation but not blocking the thread
            var products = _uof.ProductRepository.Get().ToList();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("by-price")]
        public ActionResult<IEnumerable<ProductDTO>> GetOrderByPrice()
        {
            var products = _uof.ProductRepository.GetByPrice().ToList();
            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<ProductDTO> Get(int id)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

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
        public ActionResult<ProductDTO> GetRouteExample_NOT_USED(int id, string param2)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

            return null == p ? NotFound() : _mapper.Map<ProductDTO>(p);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProductDTO p)
        {
            var product = _mapper.Map<Product>(p);
            _uof.ProductRepository.Add(product);
            _uof.Commit();

            var pSavedDTO = _mapper.Map<ProductDTO>(product);

            return new CreatedAtRouteResult("GetProduct", new { id = pSavedDTO.ProductId }, pSavedDTO);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProductDTO p)
        {

            if (id != p.ProductId)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(p);

            _uof.ProductRepository.Update(product);
            _uof.Commit();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProductDTO> Delete(int id)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (null == p)
            {
                return NotFound();
            }

            _uof.ProductRepository.Delete(p);
            _uof.Commit();

            return _mapper.Map<ProductDTO>(p);
        }
    }
}


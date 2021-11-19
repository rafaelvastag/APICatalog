using APICatalog.Entities;
using APICatalog.Filters;
using APICatalog.Repositories.UnitOfWork;
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

        public ProductsController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Product>> Get()
        {
            // waiting this operation but not blocking the thread
            return _uof.ProductRepository.Get().ToList();
        }

        [HttpGet("by-price")]
        public ActionResult<IEnumerable<Product>> GetOrderByPrice()
        {
            return _uof.ProductRepository.GetByPrice().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id, [BindRequired] string name)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

            return null == p ? NotFound() : p;
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
        public ActionResult<Product> GetRouteExample_NOT_USED(int id, string param2)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

            return null == p ? NotFound() : p;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product p)
        {

            _uof.ProductRepository.Add(p);
            _uof.Commit();

            return new CreatedAtRouteResult("GetProduct", new { id = p.ProductId }, p);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product p)
        {

            if (id != p.ProductId)
            {
                return BadRequest();
            }

            _uof.ProductRepository.Update(p);
            _uof.Commit();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var p = _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (null == p)
            {
                return NotFound();
            }

            _uof.ProductRepository.Delete(p);
            _uof.Commit();

            return p;
        }
    }
}


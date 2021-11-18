using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly CatalogDbContext _context;

        public ProductsController(CatalogDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            // waiting this operation but not blocking the thread
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int:min(1)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> Get(int id, [BindRequired] string name)
        {
            var p = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);

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
            var p = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);

            return null == p ? NotFound() : p;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(p);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct", new { id = p.ProductId }, p);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != p.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Product> Delete(int id)
        {
            var p = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (null == p)
            {
                return NotFound();
            }

            _context.Products.Remove(p);
            _context.SaveChanges();

            return p;
        }
    }
}


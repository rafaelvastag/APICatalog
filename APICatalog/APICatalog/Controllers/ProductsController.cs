using APICatalog.Context;
using APICatalog.Entities;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _context.Products.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
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


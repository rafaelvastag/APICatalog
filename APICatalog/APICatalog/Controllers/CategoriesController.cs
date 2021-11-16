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
    public class CategoriesController : ControllerBase
    {

        private readonly CatalogDbContext _context;

        public CategoriesController(CatalogDbContext context)
        {
            _context = context;
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetWithProduct()
        {
            return _context.Categories.AsNoTracking().Include(c => c.Products).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> get(int id)
        {
            Category c = _context.Categories.AsNoTracking().FirstOrDefault();

            return null == c ? NotFound() : c;
        }


        [HttpPost]
        public ActionResult Post([FromBody] Category c)
        {
            _context.Categories.Add(c);
            _context.SaveChanges();

            return CreatedAtRoute("GetCategory", new { id = c.CategoryId }, c);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category c)
        {
            if (id != c.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(c).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {
            Category c = _context.Categories.AsNoTracking().FirstOrDefault(c => id == c.CategoryId);

            if (null == c)
            {
                return NotFound();
            }

            _context.Categories.Remove(c);
            _context.SaveChanges();

            return c;
        }

    }
}

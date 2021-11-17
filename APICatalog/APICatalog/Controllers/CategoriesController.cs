using APICatalog.Context;
using APICatalog.Entities;
using APICatalog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _config;

        public CategoriesController(CatalogDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet("welcome/{msg:alpha}")]
        public ActionResult<string> GetWelcome([FromServices] IFromService _service, string msg)
        {
            return _service.Default(msg);
        }

        [HttpGet("products")]
        public ActionResult<IEnumerable<Category>> GetWithProduct()
        {
            return _context.Categories.AsNoTracking().Include(c => c.Products).ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            try
            {
                return _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying get the categories");
            }
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public ActionResult<Category> get(int id)
        {
            try
            {
                Category c = _context.Categories.AsNoTracking().Include(c => c.Products).FirstOrDefault(c => id == c.CategoryId);

                return null == c ? NotFound($"The category with id = {id} was not found") : c;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying get the category with id: {id}");
            }

        }


        [HttpPost]
        public ActionResult Post([FromBody] Category c)
        {
            try
            {
                _context.Categories.Add(c);
                _context.SaveChanges();

                return CreatedAtRoute("GetCategory", new { id = c.CategoryId }, c);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while trying create a new category");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Category c)
        {
            try
            {
                if (id != c.CategoryId)
                {
                    return BadRequest($"Ids mismatch {id} <> {c.CategoryId}");
                }

                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying update the category with id: {id}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> Delete(int id)
        {

            try
            {
                Category c = _context.Categories.AsNoTracking().FirstOrDefault(c => id == c.CategoryId);

                if (null == c)
                {
                    NotFound($"The category with id = {id} was not found");
                }

                _context.Categories.Remove(c);
                _context.SaveChanges();

                return c;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                      $"Error while trying delete the category with id: {id}");
            }
        }

    }
}

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

        }
    }


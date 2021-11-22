using APICatalog.Context;
using APICatalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogxUnitTests
{
    class DBUnitTestsMockInitializer
    {
        public void Seed(CatalogDbContext context)
        {
            context.Categories.Add
            (new Category { CategoryId = 999, Name = "Bebidas999", imageUrl = "bebidas999.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 2, Name = "Sucos", imageUrl = "sucos1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 3, Name = "Doces", imageUrl = "doces1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 4, Name = "Salgados", imageUrl = "Salgados1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 5, Name = "Tortas", imageUrl = "tortas1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 6, Name = "Bolos", imageUrl = "bolos1.jpg" });

            context.Categories.Add
            (new Category { CategoryId = 7, Name = "Lanches", imageUrl = "lanches1.jpg" });

            context.SaveChanges();
        }
    }
}

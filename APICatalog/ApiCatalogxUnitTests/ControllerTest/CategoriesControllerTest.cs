using APICatalog.Context;
using APICatalog.Controllers;
using APICatalog.DTOs;
using APICatalog.Mapper;
using APICatalog.Repositories.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiCatalogxUnitTests.ControllerTest
{
    class CategoriesControllerTest
    {
        private IMapper mapper;
        private IUnitOfWork uof;

        public static DbContextOptions<CatalogDbContext> dbContext { get; }

        public static string connectionString =
            "server=localhost;userid=developer;password=1234567;database=catalogdb";

        static CategoriesControllerTest()
        {
            dbContext = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
        }

        public CategoriesControllerTest()
        {
            var MapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = MapperConfig.CreateMapper();

            var context = new CatalogDbContext(dbContext);

            uof = new UnitOfWork(context);
        }

        // =============================================== TESTS =======================================

        public void GetCategories_Return_Ok_Result()
        {
            // Arrange
            var controller = new CategoriesController(uof,mapper);
            // Act

            var data = controller.Get();
            // Assert
            Assert.IsType<List<CategoryDTO>>(data.Value);
        }
    }
}

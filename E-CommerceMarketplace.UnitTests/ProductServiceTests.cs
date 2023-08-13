using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace E_CommerceMarketplace.UnitTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private IRepository repo;
        private ILogger<ProductService> logger;
        private IProductService productService;
        private ApplicationDbContext applicationDbContext;

        private Vendor _vendor = new Vendor()
        {
            Id = 2,
            FirstName = "Mario",
            LastName = "Anastasov",
            PhoneNumber = "0988340189",
            User_Id = "ef9ccfe6-5024-4250-88d8-8ce947882b16"
        };

        private Category _category = new Category()
        {
            Id = 1,
            Name = "Electronics"
        };

        private Status _statusStocked = new Status()
        {
            Id = 1,
            Description = "Stocked"
        };

        private Status _statusUnavailable = new Status()
        {
            Id = 2,
            Description = "Unavailable"
        };

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ECommerceDb")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task TestProductEdit()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddAsync(new ProductService()
            {
                Id = 1,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = ""
            });

            await repo.SaveChangesAsync();

            await productService.Edit(1, new ProductModel()
            {
                Id = 1,
                Address = "",
                ImageUrl = "",
                Title = "",
                Description = "This product is edited",
            });

            var dbHouse = await repo.GetByIdAsync<Product>(1);

            Assert.That(dbHouse.Description, Is.EqualTo("This house is edited"));
        }

        [Test]
        public async Task TestGetLastProduct()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
                new Product(){Id = 2, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
                new Product(){Id = 3, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
                new Product(){Id = 4, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
            });

            await repo.SaveChangesAsync();

            var productsCollection = await productService.GetLastProducts();
            Assert.That(3, Is.EqualTo(productsCollection.Count()));
            Assert.That(productsCollection.Any(p => p.Id == 1), Is.False);
        }

        [Test]
        public async Task TestGetLastProductUnavailable()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
                new Product(){Id = 2, Name="", ImageUrl="", Price = 1, Status = _statusUnavailable, Category = _category, Vendor = _vendor},
                new Product(){Id = 3, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
                new Product(){Id = 4, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category, Vendor = _vendor},
            });

            await repo.SaveChangesAsync();

            var products = await repo.AllReadonly<Product>().ToListAsync();
            var count = products.Count;

            var productsCollection = await productService.GetLastProducts();
            Assert.That(3, Is.EqualTo(productsCollection.Count()));
            Assert.That(productsCollection.Any(p => p.Id == 2), Is.False);
        }

        [TearDown]
        public void Test1()
        {
            applicationDbContext.Dispose();
        }
    }
}
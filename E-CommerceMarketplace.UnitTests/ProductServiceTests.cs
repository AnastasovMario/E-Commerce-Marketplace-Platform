using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Product;
using E_CommerceMarketplace.Core.Models.Vendor;
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
        private Product _product = new Product()
        {
            Id = 1,
            Name = "",
            ImageUrl = "",
            Price = 15,
            Description = ""
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
        public async Task EditProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Category = _category;
            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            await productService.Edit(_product.Id, new ProductEditModel()
            {
                Id = 1,
                Name = "",
                ImageUrl = "",
                CategoryId = 1,
                StatusId = 1,
                Price = 15,
                Description = "This product is edited",
            });

            var dbProduct = await repo.GetByIdAsync<Product>(1);

            Assert.That(dbProduct.Description, Is.EqualTo("This product is edited"));
        }

        [Test]
        public async Task EditProductTestFail()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            var productId = 2;

            var ex = Assert.ThrowsAsync<ApplicationException>(async () => await productService.Edit(productId, new ProductEditModel()
            {
                Id = 1,
                Name = "",
                ImageUrl = "",
                CategoryId = 1,
                StatusId = 1,
                Price = 15,
                Description = "This product is edited",
            }));
            Assert.That(ex.Message, Is.EqualTo($"Database failed to edit product with [{productId}]"));
        }

        [Test]
        public async Task GetLastProductTest()
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
        public async Task GetLastProductUnavailableTest()
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

        [Test]
        public async Task CreateProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Category = _category;
            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var model = new ProductModel()
            {
                Name = "",
                ImageUrl = "",
                CategoryId = 1,
                Price = 15,
                Description = "This product is edited",
            };

            var productId = await productService.Create(model, _vendor.Id);

            var dbProduct = await repo.GetByIdAsync<Product>(productId);

            Assert.That(productId, Is.EqualTo(2));
            Assert.That(_vendor.Id, Is.EqualTo(dbProduct.Vendor.Id));
        }

        [Test]
        public async Task CreateProductTestFail()
        {
            var model = new ProductModel
            {
                Name = "Test Product",
                Price = 100.00m,
                ImageUrl = "test.jpg",
                CategoryId = 1,
                Description = "Test Description"
            };

            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ThrowsAsync(new Exception("Database error"));

            var mockLogger = new Mock<ILogger<ProductService>>();

            var productService = new ProductService(mockRepo.Object, mockLogger.Object);

            Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await productService.Create(model, _vendor.Id);
            });
        }

        [Test]
        public async Task AllCategoriesTest()
        {

            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 2" },
                new Category { Id = 3, Name = "Category 3" }
            });

            await repo.SaveChangesAsync();

            var result = await productService.AllCategories();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.LastOrDefault().Id, Is.EqualTo(3));
            Assert.That(result.LastOrDefault().Name, Is.EqualTo("Category 3"));
        }

        [Test]
        public async Task AllCategoriesNamesTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Category>()
            {
                new Category { Id = 1, Name = "Category 1" },
                new Category { Id = 2, Name = "Category 3" },
                new Category { Id = 3, Name = "Category 3" }
            });

            await repo.SaveChangesAsync();

            var result = await productService.AllCategoriesNames();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.LastOrDefault(), Is.EqualTo("Category 3"));
        }

        [Test]
        public async Task AllStatusesTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Status>()
            {
                new Status { Id = 1, Description = "Status 1" },
                new Status { Id = 2, Description = "Status 2" },
                new Status { Id = 3, Description = "Status 3" }
            });

            await repo.SaveChangesAsync();

            var result = await productService.AllProductStatuses();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.FirstOrDefault().Id, Is.EqualTo(1));
            Assert.That(result.FirstOrDefault().Description, Is.EqualTo("Status 1"));
        }

        [Test]
        public async Task AllStatusesNamesTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Status>()
            {
                new Status { Id = 1, Description = "Status 1" },
                new Status { Id = 2, Description = "Status 3" },
                new Status { Id = 3, Description = "Status 3" }
            });

            await repo.SaveChangesAsync();

            var result = await productService.AllStatusesNames();

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.LastOrDefault(), Is.EqualTo("Status 3"));
        }

        [Test]
        public async Task DeleteTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var productsCount = await repo.AllReadonly<Product>().ToListAsync();

            await productService.Delete(1);

            var newProductsCount = await repo.AllReadonly<Product>().ToListAsync();

            Assert.That(productsCount.Count(), Is.EqualTo(1));
            Assert.That(productsCount.FirstOrDefault().Id, Is.EqualTo(_product.Id));
            Assert.That(newProductsCount.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteThrowsException()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var testId = 2;

            Assert.ThrowsAsync<ApplicationException>(async () => await productService.Delete(testId));

            var ex = Assert.ThrowsAsync<ApplicationException>(async () => await productService.Delete(testId));
            Assert.That(ex.Message, Is.EqualTo($"Database failed to delete product with [{testId}]"));
        }

        [Test]
        public async Task GetProductCategoryIdTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            var productService = new ProductService(repo, logger);

            _product.Category = _category;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var categoryId = await productService.GetProductCategoryId(_product.Id);

            Assert.That(categoryId, Is.EqualTo(_category.Id));
        }

        [Test]
        public async Task GetProductDetailsById()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Category = _category;
            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var expectedDetailsServiceModel = new ProductDetailsServiceModel
            {
                Id = _product.Id,
                Name = _product.Name,
                Price = _product.Price,
                Description = _product.Description,
                Status = _product.Status.Description,
                Category = _product.Category.Name,
                ImageUrl = _product.ImageUrl,
                IsAvailable = _product.Status_Id == Status.Stocked.Id,
                Vendor = new VendorServiceModel
                {
                    FirstName = _product.Vendor.FirstName,
                    LastName = _product.Vendor.LastName,
                    PhoneNumber = _product.Vendor.PhoneNumber
                }
            };

            var result = await productService.GetProductDetailsById(1);

            // Assert
            Assert.AreEqual(expectedDetailsServiceModel.Id, result.Id);
            Assert.AreEqual(expectedDetailsServiceModel.Name, result.Name);
            Assert.AreEqual(expectedDetailsServiceModel.Price, result.Price);
            Assert.AreEqual(expectedDetailsServiceModel.Description, result.Description);
            Assert.AreEqual(expectedDetailsServiceModel.Status, result.Status);
            Assert.AreEqual(expectedDetailsServiceModel.Category, result.Category);
            Assert.AreEqual(expectedDetailsServiceModel.ImageUrl, result.ImageUrl);
            Assert.AreEqual(expectedDetailsServiceModel.IsAvailable, result.IsAvailable);
            Assert.AreEqual(expectedDetailsServiceModel.Vendor.FirstName, result.Vendor.FirstName);
            Assert.AreEqual(expectedDetailsServiceModel.Vendor.LastName, result.Vendor.LastName);
            Assert.AreEqual(expectedDetailsServiceModel.Vendor.PhoneNumber, result.Vendor.PhoneNumber);
        }


        [TearDown]
        public void Test1()
        {
            applicationDbContext.Dispose();
        }
    }
}
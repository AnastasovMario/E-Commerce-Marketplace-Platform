using E_Commerce_Marketplace_Platform.Models;
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

namespace E_CommerceMarketplace.UnitTests.Tests
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

        private Vendor _vendor2 = new Vendor()
        {
            Id = 3,
            FirstName = "Stoyan",
            LastName = "Stoyanov",
            PhoneNumber = "+124564745",
            User_Id = "ef9ccfe6-5024-4200-88d8-8ce947882b16"
        };

        private Category _category1 = new Category()
        {
            Id = Category.Electronics.Id,
            Name = "Electronics"
        };

        private Category _category2 = new Category()
        {
            Id = Category.HomeAndGarden.Id,
            Name = "Home & Garden"
        };

        private Status _statusUnavailable = new Status()
        {
            Id = Status.Unavailable.Id,
            Description = "Unavailable"
        };

        private Status _statusStocked = new Status()
        {
            Id = Status.Stocked.Id,
            Description = "Stocked"
        };

        private Product _product = new Product()
        {
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
        public async Task AllProductsTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(3));
            Assert.That(result.Products.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task AllProductsPaginationTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
                new Product(){Id = 4, Name="Test 2", ImageUrl="imgUrl4", Price = 50, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(4));
            Assert.That(result.Products.Count, Is.EqualTo(3));

            model.CurrentPage = 2;

            result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(4));
            Assert.That(result.Products.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AllProductsCategoryTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
                new Product(){Id = 4, Name="Test 2", ImageUrl="imgUrl4", Price = 50, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            model.Category = "Electronics";

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(2));
            Assert.That(result.Products.Count, Is.EqualTo(2));
            Assert.True(result.Products.All(p => p.Category == "Electronics"));
        }

        [Test]
        public async Task AllProductsStatusTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
                new Product(){Id = 4, Name="Test 2", ImageUrl="imgUrl4", Price = 50, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            model.Status = "Stocked";

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(2));
            Assert.That(result.Products.Count, Is.EqualTo(2));
            Assert.True(result.Products.All(p => p.Status == "Stocked"));
        }

        [Test]
        public async Task AllProductsSearchTermTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
                new Product(){Id = 4, Name="Test 2", ImageUrl="imgUrl4", Price = 50, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            model.SearchTerm = "Product";

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(2));
            Assert.That(result.Products.Count, Is.EqualTo(2));
            Assert.True(result.Products.All(p => p.Name.Contains("Product")));

            model.SearchTerm = "Mario";

            result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(2));
            Assert.That(result.Products.Count, Is.EqualTo(2));
            Assert.True(result.Products.All(p => p.Vendor.Contains("Mario")));
        }

        [Test]
        public async Task AllProductsSortingTest()
        {
            var model = new AllProductsQueryModel();
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;

            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddRangeAsync(new List<Product>()
            {
                new Product(){Id = 1, Name="Product 1", ImageUrl="imgUrl1", Price = 10, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="Product 2", ImageUrl="imgUrl2", Price = 20, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="Test 1", ImageUrl="imgUrl3", Price = 40, Status = _statusUnavailable, Category = _category2, Vendor = _vendor2},
            });

            await repo.SaveChangesAsync();

            model.Sorting = ProductSorting.Price;

            var result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(3));
            Assert.That(result.Products.Count, Is.EqualTo(3));
            Assert.That(result.Products.FirstOrDefault().Id, Is.EqualTo(1));
            Assert.That(result.Products.FirstOrDefault().Name, Is.EqualTo("Product 1"));
            Assert.That(result.Products.FirstOrDefault().ImageUrl, Is.EqualTo("imgUrl1"));
            Assert.That(result.Products.FirstOrDefault().Price, Is.EqualTo(10));
            Assert.That(result.Products.FirstOrDefault().Status, Is.EqualTo(_statusStocked.Description));
            Assert.That(result.Products.FirstOrDefault().Category, Is.EqualTo(_category1.Name));
            Assert.That(result.Products.FirstOrDefault().Vendor, Is.EqualTo($"{_vendor.FirstName} {_vendor.LastName}"));


            model.Sorting = ProductSorting.Newest;

            result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(3));
            Assert.That(result.Products.Count, Is.EqualTo(3));
            Assert.That(result.Products.FirstOrDefault().Id, Is.EqualTo(3));
            Assert.That(result.Products.FirstOrDefault().Name, Is.EqualTo("Test 1"));
            Assert.That(result.Products.FirstOrDefault().ImageUrl, Is.EqualTo("imgUrl3"));
            Assert.That(result.Products.FirstOrDefault().Price, Is.EqualTo(40));
            Assert.That(result.Products.FirstOrDefault().Status, Is.EqualTo(_statusUnavailable.Description));
            Assert.That(result.Products.FirstOrDefault().Category, Is.EqualTo(_category2.Name));
            Assert.That(result.Products.FirstOrDefault().Vendor, Is.EqualTo($"{_vendor2.FirstName} {_vendor2.LastName}"));

            model.Sorting = ProductSorting.AvailableFirst;

            result = await productService.All(model.Category, model.Status, model.SearchTerm, model.Sorting, model.CurrentPage, AllProductsQueryModel.ProductsPerPage);

            Assert.That(result.TotalProductsCount, Is.EqualTo(3));
            Assert.That(result.Products.Count, Is.EqualTo(3));
            Assert.That(result.Products.FirstOrDefault().Id, Is.EqualTo(2));
            Assert.That(result.Products.FirstOrDefault().Name, Is.EqualTo("Product 2"));
            Assert.That(result.Products.FirstOrDefault().ImageUrl, Is.EqualTo("imgUrl2"));
            Assert.That(result.Products.FirstOrDefault().Price, Is.EqualTo(20));
            Assert.That(result.Products.FirstOrDefault().Status, Is.EqualTo(_statusStocked.Description));
            Assert.That(result.Products.FirstOrDefault().Category, Is.EqualTo(_category1.Name));
            Assert.That(result.Products.FirstOrDefault().Vendor, Is.EqualTo($"{_vendor.FirstName} {_vendor.LastName}"));

            Assert.That(result.TotalProductsCount, Is.EqualTo(3));
            Assert.That(result.Products.Count, Is.EqualTo(3));
            Assert.That(result.Products.LastOrDefault().Id, Is.EqualTo(3));
            Assert.That(result.Products.LastOrDefault().Name, Is.EqualTo("Test 1"));
            Assert.That(result.Products.LastOrDefault().ImageUrl, Is.EqualTo("imgUrl3"));
            Assert.That(result.Products.LastOrDefault().Price, Is.EqualTo(40));
            Assert.That(result.Products.LastOrDefault().Status, Is.EqualTo(_statusUnavailable.Description));
            Assert.That(result.Products.LastOrDefault().Category, Is.EqualTo(_category2.Name));
            Assert.That(result.Products.LastOrDefault().Vendor, Is.EqualTo($"{_vendor2.FirstName} {_vendor2.LastName}"));
        }

        [Test]
        public async Task EditProductTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Category = _category1;
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
                new Product(){Id = 1, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 4, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
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
                new Product(){Id = 1, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 2, Name="", ImageUrl="", Price = 1, Status = _statusUnavailable, Category = _category1, Vendor = _vendor},
                new Product(){Id = 3, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
                new Product(){Id = 4, Name="", ImageUrl="", Price = 1, Status = _statusStocked, Category = _category1, Vendor = _vendor},
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

            _product.Category = _category1;
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

            _product.Category = _category1;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var categoryId = await productService.GetProductCategoryId(_product.Id);

            Assert.That(categoryId, Is.EqualTo(_category1.Id));
        }

        [Test]
        public async Task GetProductDetailsByIdTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Category = _category1;
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

        [Test]
        public async Task GetProductsByVendorIdTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            var newProduct = new Product()
            {
                Name = "Second product",
                ImageUrl = "",
                Price = 15,
                Description = "",
                Vendor = _vendor2
            };

            await repo.AddAsync(newProduct);

            await repo.SaveChangesAsync();

            var productsCollection = await productService.GetProductsByVendorId(_vendor.Id);

            Assert.That(productsCollection.Count, Is.EqualTo(1));
            Assert.That(productsCollection.FirstOrDefault().Id, Is.EqualTo(_product.Id));
            Assert.That(productsCollection.FirstOrDefault().Vendor, Is.EqualTo($"{_vendor.FirstName} {_vendor.LastName}"));
        }

        [Test]
        public async Task GetProductStatusIdTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var statusId = await productService.GetProductStatusId(_product.Id);

            Assert.That(statusId, Is.EqualTo(_product.Status_Id));

            statusId = await productService.GetProductStatusId(5);

            Assert.That(statusId, Is.EqualTo(0));
        }

        [Test]
        public async Task GetUserProductsTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);
        }

        [Test]
        public async Task HasVendorWithIdTrueTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var hasVendor = await productService.HasVendorWithId(_product.Id, _vendor.User_Id);

            Assert.That(hasVendor, Is.True);
        }

        [Test]
        public async Task HasVendorWithIdFalseTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;
            _product.Vendor = _vendor;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var hasVendor = await productService.HasVendorWithId(_product.Id, "1234");

            Assert.That(hasVendor, Is.False);
        }

        [Test]
        public async Task IsProductAvailableTrueTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusStocked;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var isAvailable = await productService.IsProductAvailable(_product.Id);

            Assert.That(isAvailable, Is.True);
        }

        [Test]
        public async Task IsProductAvailableFalseTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            _product.Status = _statusUnavailable;

            await repo.AddAsync(_product);

            await repo.SaveChangesAsync();

            var isAvailable = await productService.IsProductAvailable(_product.Id);

            Assert.That(isAvailable, Is.False);
        }

        [Test]
        public async Task StatusExistsTrueTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddAsync(_statusUnavailable);
            await repo.SaveChangesAsync();

            var status = await productService.StatusExists(_statusUnavailable.Id);

            Assert.IsTrue(status);
        }

        [Test]
        public async Task StatusExistsFalseTest()
        {
            var loggerMock = new Mock<ILogger<ProductService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            productService = new ProductService(repo, logger);

            await repo.AddAsync(_statusUnavailable);
            await repo.SaveChangesAsync();

            var status = await productService.StatusExists(5);

            Assert.False(status);
        }

        [TearDown]
        public void Test1()
        {
            applicationDbContext.Dispose();
        }
    }
}
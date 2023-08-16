using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace E_CommerceMarketplace.UnitTests.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        private ApplicationDbContext applicationDbContext;
        private IRepository repo;
        private ILogger<ItemService> logger;
        private ILogger<OrderService> orderLogger;
        private IItemService itemService;
        private IOrderService orderService;

        private Category _category1 = new Category()
        {
            Id = Category.Electronics.Id,
            Name = "Electronics"
        };

        private Product _product = new Product()
        {
            Id = 10,
            Name = "Test Product",
            ImageUrl = "imgurl1",
            Vendor_Id = 1,
            Price = 10.0M,
        };

        private Status _statusStocked = new Status()
        {
            Id = Status.Stocked.Id,
            Description = "Stocked"
        };

        private readonly string marioUserId = "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e";

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ECommerceDb")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            repo = new Repository(applicationDbContext);

            var loggerOrder = new Mock<ILogger<OrderService>>();
            orderLogger = loggerOrder.Object;
            var orderService = new OrderService(repo, orderLogger);

            var loggerMock = new Mock<ILogger<ItemService>>();
            logger = loggerMock.Object;
            itemService = new ItemService(repo, orderService, logger);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();
        }

        [Test]
        public async Task CreateTest()
        {
            _product.Category = _category1;
            _product.Status = _statusStocked;

            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var userId = "testUserId";
            var model = new ItemServiceModel
            {
                Quantity = 2,
                Product_Id = _product.Id,
                Price = _product.Price
            };

            await itemService.Create(model, userId);

            var items = await repo.AllReadonly<Item>().Include(i => i.Order).ToListAsync();
            var createdItem = items.FirstOrDefault();

            Assert.NotNull(createdItem);
            Assert.That(createdItem.Quantity, Is.EqualTo(model.Quantity));
            Assert.That(createdItem.Product_Id, Is.EqualTo(_product.Id));
            Assert.That(createdItem.Total, Is.EqualTo(model.Price * model.Quantity));
            Assert.That(createdItem.Order, Is.Not.Null);
            Assert.That(createdItem.Order.User_Id, Is.EqualTo(userId));
        }

        [Test]
        public async Task CreateExceptionTest()
        {
            var userId = "testUserId";
            var model = new ItemServiceModel
            {
                Quantity = -1,
                Price = 10.0M
            };

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await itemService.Create(model, userId);
            });

            Assert.NotNull(ex);
            Assert.That(ex.Message, Is.EqualTo($"Quantity cannot be less than 1."));
        }

        [Test]
        public async Task EditTest()
        {
            _product.Category = _category1;
            _product.Status = _statusStocked;

            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var userId = "testUserId";
            var model = new ItemServiceModel
            {
                Quantity = 2,
                Product_Id = _product.Id,
                Price = _product.Price
            };

            await itemService.Create(model, userId);

            var items = await repo.AllReadonly<Item>().Include(i => i.Order).ToListAsync();
            var createdItem = items.FirstOrDefault();

            var newQuantity = 5;
            model = new ItemServiceModel
            {
                Quantity = newQuantity
            };

            int editedItemId = await itemService.Edit(createdItem.Id, model);

            var editedItem = await repo.GetByIdAsync<Item>(editedItemId);

            Assert.NotNull(editedItem);
            Assert.That(editedItem.Quantity, Is.EqualTo(newQuantity));
        }

        [Test]
        public async Task EditExceptionTest()
        {
            _product.Category = _category1;
            _product.Status = _statusStocked;

            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var model = new ItemServiceModel
            {
                Quantity = 2,
                Product_Id = _product.Id,
                Price = _product.Price
            };

            await itemService.Create(model, marioUserId);

            var items = await repo.AllReadonly<Item>().Include(i => i.Order).ToListAsync();
            var createdItem = items.FirstOrDefault();

            var newQuantity = -1;
            model = new ItemServiceModel
            {
                Quantity = newQuantity
            };

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await itemService.Edit(createdItem.Id, model);
            });

            Assert.NotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("Quantity cannot be less than 1."));
        }

        [Test]
        public async Task GetItemByIdTest()
        {
            var model = new ItemServiceModel
            {
                Quantity = 2,
                Product_Id = 1,
                Price = 10.0M
            };

            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var order = new Order()
            {
                Date = DateTime.Now,
                User_Id = marioUserId
            };

            await repo.AddAsync(order);
            await repo.SaveChangesAsync();

            var item = new Item
            {
                Quantity = model.Quantity,
                Product_Id = _product.Id,
                Total = Math.Round(model.Quantity * model.Price),
                Order_Id = order.Id
            };

            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            var orderService = new OrderService(repo, orderLogger);
            var itemService = new ItemService(repo, orderService, logger);

            var vendor = await repo.GetByIdAsync<Vendor>(_product.Vendor_Id);

            var result = await itemService.GetItemById(item.Id);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(item.Id));
            Assert.That(result.Name, Is.EqualTo(_product.Name));
            Assert.That(result.ImageUrl, Is.EqualTo(_product.ImageUrl));
            Assert.That(result.Price, Is.EqualTo(_product.Price));
            Assert.That(result.Quantity, Is.EqualTo(item.Quantity));
            Assert.That(result.Total, Is.EqualTo(item.Quantity * _product.Price));
            Assert.That(result.Vendor, Is.EqualTo($"{vendor.FirstName} {vendor.LastName}"));
        }

        [Test]
        public async Task ExistsTest()
        {

            var item = new Item
            {
                Quantity = 2,
                Product_Id = 1,
                Total = 20.0M
            };

            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            var exists = await itemService.Exists(item.Id);

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task GetItemsHistoryTest()
        {
            _product.Status = _statusStocked;
            _product.Category = _category1;

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User_Id = marioUserId,
                Sale_Id = 1
            };

            await repo.AddAsync(order);
            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var item1 = new Item
            {
                Quantity = 2,
                Product = _product,
                Total = 20.0M,
                Order_Id = order.Id
            };

            var item2 = new Item
            {
                Quantity = 3,
                Product = _product,
                Total = 30.0M,
                Order_Id = order.Id
            };

            await repo.AddAsync(item1);
            await repo.AddAsync(item2);
            await repo.SaveChangesAsync();

            var itemsHistory = await itemService.GetItemsHistory(marioUserId);

            Assert.NotNull(itemsHistory);
            Assert.That(itemsHistory.Count(), Is.EqualTo(2));
            foreach (var item in itemsHistory)
            {
                Assert.IsTrue(item.IsSold);
            }
        }

        [Test]
        public async Task GetUsersBoughtProductsTest()
        {
            _product.Status = _statusStocked;
            _product.Category = _category1;

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User_Id = marioUserId,
                Sale_Id = 1
            };

            await repo.AddAsync(order);
            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var item1 = new Item
            {
                Quantity = 2,
                Product = _product,
                Total = 20.0M,
                Order_Id = order.Id
            };

            var item2 = new Item
            {
                Quantity = 3,
                Product = _product,
                Total = 30.0M,
                Order_Id = order.Id
            };

            await repo.AddAsync(item1);
            await repo.AddAsync(item2);
            await repo.SaveChangesAsync();

            var boughtProducts = await itemService.GetUsersBoughtProducts(marioUserId);

            Assert.NotNull(boughtProducts);
            Assert.That(boughtProducts.Count(), Is.EqualTo(2));
            foreach (var product in boughtProducts)
            {
                Assert.That(product.Name, Is.EqualTo("Test Product"));
                Assert.That(product.ImageUrl, Is.EqualTo("imgurl1"));
                Assert.That(product.Price, Is.EqualTo(10.0M));
                Assert.That(product.Quantity >= 2, Is.True);
                Assert.That(product.Vendor, Is.EqualTo("Linda Michaels"));
            }
        }

        [Test]
        public async Task HasBuyerWithIdTest()
        {
            _product.Status = _statusStocked;
            _product.Category = _category1;

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User_Id = marioUserId,
                Sale_Id = 1
            };

            await repo.AddAsync(order);
            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var item = new Item
            {
                Quantity = 2,
                Product = _product,
                Total = 20.0M,
                Order_Id = order.Id
            };

            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            var hasBuyer = await itemService.HasBuyerWithId(item.Id, marioUserId);

            Assert.IsTrue(hasBuyer);
        }

        [Test]
        public async Task IsItemBoughtTest()
        {
            _product.Status = _statusStocked;
            _product.Category = _category1;

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User_Id = marioUserId,
                Sale_Id = 1
            };

            await repo.AddAsync(order);
            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var item = new Item
            {
                Quantity = 2,
                Product = _product,
                Total = 20.0M,
                Order_Id = order.Id
            };

            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            var isBought = await itemService.IsItemBought(item.Id);

            Assert.IsTrue(isBought);
        }

        [Test]
        public async Task RemoveTest()
        {
            _product.Status = _statusStocked;
            _product.Category = _category1;

            var order = new Order
            {
                Date = DateTime.UtcNow,
                User_Id = marioUserId,
                Sale_Id = 1
            };

            await repo.AddAsync(order);
            await repo.AddAsync(_product);
            await repo.SaveChangesAsync();

            var item = new Item
            {
                Quantity = 2,
                Product = _product,
                Total = 20.0M,
                Order_Id = order.Id
            };

            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            await itemService.Remove(item.Id);

            var isItemExists = await itemService.Exists(item.Id);
            Assert.IsFalse(isItemExists);
        }

        [Test]
        public void RemoveExceptionTest()
        {
            var invalidItemId = -1;

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await itemService.Remove(invalidItemId);
            });

            Assert.NotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("Database failed to remove item"));
        }


        [TearDown]
        public void Test1()
        {
            applicationDbContext.Dispose();
        }

    }
}

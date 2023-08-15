using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
using E_CommerceMarketplace.Core.Models.Order;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ECommerceDb")
                .Options;

            applicationDbContext = new ApplicationDbContext(contextOptions);

            applicationDbContext.Database.EnsureDeleted();
            applicationDbContext.Database.EnsureCreated();

            repo = new Repository(applicationDbContext);

            var loggerOrder = new Mock<ILogger<OrderService>>();
            orderLogger = loggerOrder.Object;
            var orderService = new OrderService(repo, orderLogger);
   
            var loggerMock = new Mock<ILogger<ItemService>>();
            logger = loggerMock.Object;
            itemService = new ItemService(repo,orderService,logger);
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
        public async Task CreateExceptionTest_FailedToSave()
        {
            var userId = "testUserId";
            var model = new ItemServiceModel
            {
                Quantity = 2,
                Price = 10.0M
            };

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await itemService.Create(model, userId);
            });

            Assert.NotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("Database failed to save item"));
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

            var newQuantity = -1; // Simulate an invalid quantity
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
    }
}

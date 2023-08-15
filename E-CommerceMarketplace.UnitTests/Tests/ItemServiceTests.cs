using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Item;
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
            var product = new Product
            {
                Name = "Test Product",
                ImageUrl = "imgurl1",
                Vendor_Id = 1,
                Price = 10.0M,
            };
            await repo.AddAsync(product);
            await repo.SaveChangesAsync();

            var userId = "testUserId";
            var model = new ItemServiceModel
            {
                Quantity = 2,
                Product_Id = product.Id,
                Price = product.Price
            };

            await itemService.Create(model, userId);

            var items = await repo.AllReadonly<Item>().ToListAsync();
            var createdItem = items.FirstOrDefault();

            Assert.NotNull(createdItem);
            Assert.That(createdItem.Quantity, Is.EqualTo(model.Quantity));
            Assert.That(createdItem.Product_Id, Is.EqualTo(product.Id));
            Assert.That(createdItem.Total, Is.EqualTo(model.Price * model.Quantity));
            Assert.That(createdItem.Order, Is.Not.Null);
            Assert.That(createdItem.Order.User_Id, Is.EqualTo(userId));
        }

        [Test]
        public async Task EditTest()
        {
            // Create a test item
            var item = new Item
            {
                Quantity = 2,
                Product = new Product { Price = 10.0M }
            };
            await repo.AddAsync(item);
            await repo.SaveChangesAsync();

            var newQuantity = 5;
            var model = new ItemServiceModel
            {
                Quantity = newQuantity
            };

            var editedItemId = await itemService.Edit(item.Id, model);

            var editedItem = await repo.GetByIdAsync<Item>(editedItemId);

            Assert.NotNull(editedItem);
            Assert.AreEqual(newQuantity, editedItem.Quantity);
        }

        // Add more tests for other methods...
    }

}

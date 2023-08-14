using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceMarketplace.UnitTests.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService orderService;
        private ILogger<OrderService> logger;
        private ApplicationDbContext applicationDbContext;

        private Order _order = new Order()
        {
            Date = DateTime.UtcNow,
            Items = new List<Item>()
        };

        private Item _item = new Item()
        {
            Id = 2,
            Quantity = 1,
        };

        private Product _product = new Product()
        {
            Id = 1,
            Name = "",
            ImageUrl = "",
            Price = 15,
            Description = ""
        };

        private readonly string marioUserId = "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e";

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
        public async Task ClearOrderTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var user = await repo.GetByIdAsync<ApplicationUser>(marioUserId);

            _order.User = user;
            _item.Product = _product;
            _item.Total = _product.Price * _item.Quantity;
            _item.Order = _order;

            await repo.AddAsync(_order);
            await repo.AddAsync(_item);
            await repo.SaveChangesAsync();

            var order = await repo.GetByIdAsync<Order>(_order.Id);

            var orderItems = await repo.AllReadonly<Item>()
                .Where(i => i.Order_Id == _order.Id)
                .ToListAsync();

            await orderService.ClearOrder(_order.Id);

            Assert.That(_order.User_Id, Is.EqualTo(marioUserId));

            Assert.That(order.Id, Is.EqualTo(1));

            Assert.True(orderItems.Any());
            Assert.That(orderItems.FirstOrDefault().Id, Is.EqualTo(2));
            Assert.That(orderItems.FirstOrDefault().Total, Is.EqualTo(15));
            Assert.That(orderItems.FirstOrDefault().Product_Id, Is.EqualTo(_product.Id));
            Assert.That(orderItems.FirstOrDefault().Quantity, Is.EqualTo(1));

            var orders = await repo.AllReadonly<Order>().ToListAsync();
            var remainingItemsCount = await repo.AllReadonly<Item>().CountAsync();

            Assert.That(orders.Count(), Is.EqualTo(0));
            Assert.That(remainingItemsCount, Is.EqualTo(0));
        }

        [Test]
        public async Task ClearOrder_ExceptionTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Mock<IRepository>();
            orderService = new OrderService(repo.Object, logger);

            var orderId = 1; 

            repo.Setup(r => r.AllReadonly<Item>())
                .Returns(new List<Item>().AsQueryable());

            var ex = Assert.ThrowsAsync<ApplicationException>(async () => await orderService.ClearOrder(orderId));
            Assert.AreEqual($"Database failed to clear order [{orderId}]", ex.Message);
        }

        [Test]
        public async Task CreateOrderTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var orderId = await orderService.CreateOrder(marioUserId);

            var createdOrder = await repo.GetByIdAsync<Order>(orderId);

            Assert.That(createdOrder.User_Id, Is.EqualTo(marioUserId));
            Assert.That(createdOrder.Date, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public async Task CreateOrderReturnsValidOrderId()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var orderId = await orderService.CreateOrder(marioUserId);

            Assert.That(orderId, Is.Positive);
        }

        [Test]
        public async Task CreateOrderExceptionTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repoMock = new Mock<IRepository>();
            repoMock.Setup(r => r.AddAsync(It.IsAny<Order>())).ThrowsAsync(new Exception("Mock database exception"));

            orderService = new OrderService(repoMock.Object, logger);

            var exception = Assert.ThrowsAsync<ApplicationException>(async () => await orderService.CreateOrder(marioUserId));
            Assert.That(exception.Message, Is.EqualTo("Database failed to create order"));
        }

        [Test]
        public async Task GetCurrentOrderForUserTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            _order.User_Id = marioUserId;

            await repo.AddAsync(_order);
            await repo.SaveChangesAsync();

            var result = await orderService.GetCurrentOrderForUser(marioUserId);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(_order.Id));
            Assert.That(result.Date, Is.EqualTo(_order.Date));

            Assert.NotNull(result.OrderItems);
            Assert.IsEmpty(result.OrderItems);
        }

        [Test]
        public async Task GetOrderDetailsTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var orderId = 1;

            _order.User_Id = marioUserId;

            await repo.AddAsync(_order);
            await repo.SaveChangesAsync();

            var result = await orderService.GetOrderDetails(orderId);

            Assert.NotNull(result);
            Assert.That(result.Id, Is.EqualTo(_order.Id));
            Assert.That(result.Date, Is.EqualTo(_order.Date));
        }

        [Test]
        public async Task GetOrderIdTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            _order.User_Id = marioUserId;

            var order2 = new Order
            {
                Id = 2,
                Date = DateTime.UtcNow,
                DateCompleted = null,
                User_Id = marioUserId,
                Sale_Id = null
            };

            await repo.AddAsync(_order);
            await repo.AddAsync(order2);
            await repo.SaveChangesAsync();

            var result = await orderService.GetOrderId(marioUserId);

            Assert.AreEqual(order2.Id, result);
        }

        [Test]
        public async Task GetOrderItemsTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            _order.User_Id = marioUserId;

            var product = new Product
            {
                Id = 10,
                Name = "Test Product",
                Price = 10.0M,
                ImageUrl = "test.jpg",
                Vendor = new Vendor
                {
                    Id = 2,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "+12346345",
                    User_Id = "testId"
                }
            };

            var orderItem = new Item
            {
                Id = 1,
                Quantity = 2,
                Total = 20.0M,
                Product = product,
                Order = _order
            };

            await repo.AddAsync(_order);
            await repo.AddAsync(product);
            await repo.AddAsync(orderItem);
            await repo.SaveChangesAsync();

            var result = await orderService.GetOrderItems(_order.Id);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));

            var orderItemViewModel = result.FirstOrDefault();
            Assert.That(orderItemViewModel, Is.Not.Null);
            Assert.That(orderItemViewModel.Id, Is.EqualTo(orderItem.Id));
            Assert.That(orderItemViewModel.Product_Id, Is.EqualTo(product.Id));
            Assert.That(orderItemViewModel.Name, Is.EqualTo(product.Name));
            Assert.That(orderItemViewModel.Quantity, Is.EqualTo(orderItem.Quantity));
            Assert.That(orderItemViewModel.Price, Is.EqualTo(product.Price));
            Assert.That(orderItemViewModel.Total, Is.EqualTo(orderItem.Total));
            Assert.That(orderItemViewModel.ImageUrl, Is.EqualTo(product.ImageUrl));
            Assert.That(orderItemViewModel.Vendor, Is.EqualTo($"{product.Vendor.FirstName} {product.Vendor.LastName}"));
        }

        [Test]
        public async Task HasUserWithIdTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            _order.User_Id = marioUserId;

            await repo.AddAsync(_order);
            await repo.SaveChangesAsync();

            var result = await orderService.HasUserWithId(_order.Id, _order.User_Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task PayOrderTest()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var user = new ApplicationUser
            {
                Id = "testUserId",
                UserName = "testUser",
                NormalizedUserName = "TESTUSER",
                Email = "test@example.com",
                NormalizedEmail = "TEST@EXAMPLE.COM"
            };

            await repo.AddAsync(user);

            var product = new Product
            {
                Id = 1,
                Name = "Product 1",
                ImageUrl = "imgulr1",
                Price = 10,
                Description = "Test product",
                Status_Id = Status.Stocked.Id,
                Vendor_Id = 1
            };

            await repo.AddAsync(product);

            _order.User_Id = user.Id;

            await repo.AddAsync(_order);

            var item = new Item
            {
                Id = 1,
                Product_Id = product.Id,
                Order_Id = _order.Id,
                Quantity = 1,
                Total = product.Price
            };

            await repo.AddAsync(item);

            await repo.SaveChangesAsync();

            await orderService.PayOrder(_order.Id);

            var updatedOrder = await repo.GetByIdAsync<Order>(_order.Id);
            var sale = await repo.AllReadonly<Sale>().FirstOrDefaultAsync(s => s.Buyer_Id == user.Id);

            Assert.NotNull(updatedOrder.Sale);
            Assert.That(sale.Id, Is.EqualTo(updatedOrder.Sale_Id));
            Assert.That(DateTime.UtcNow.Date, Is.EqualTo(updatedOrder.DateCompleted?.Date));

            var totalSales = await repo.AllReadonly<Sale>().SumAsync(s => s.Total);
            var totalItems = await repo.AllReadonly<Item>().SumAsync(i => i.Total);

            Assert.That(totalItems, Is.EqualTo(totalSales));
        }

        [Test]
        public async Task PayOrderException()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            logger = loggerMock.Object;
            var repo = new Repository(applicationDbContext);
            orderService = new OrderService(repo, logger);

            var nonExistentOrderId = 999;

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await orderService.PayOrder(nonExistentOrderId);
            });

            Assert.That(ex.Message, Is.EqualTo($"Database failed to pay for order [{nonExistentOrderId}]"));
        }

        [Test]
        public async Task PayOrderErrorWhilePaying()
        {
            var loggerMock = new Mock<ILogger<OrderService>>();
            var repoMock = new Mock<IRepository>();
            logger = loggerMock.Object;
            orderService = new OrderService(repoMock.Object, logger);

            var orderId = 1;

            repoMock.Setup(r => r.GetByIdAsync<Order>(orderId)).ThrowsAsync(new Exception("Mocked exception"));

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await orderService.PayOrder(orderId);
            });

            Assert.AreEqual($"Database failed to pay for order [{orderId}]", ex.Message);
        }

        [TearDown]
        public void Test1()
        {
            applicationDbContext.Dispose();
        }
    }
}

using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Vendor;
using E_CommerceMarketplace.Core.Services;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace E_CommerceMarketplace.UnitTests.Tests
{
    [TestFixture]
    public class VendorServiceTests
    {
        private IRepository repo;
        private ILogger<VendorService> logger;
        private IVendorService vendorService;
        private ApplicationDbContext applicationDbContext;


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
        public async Task CreateTest()
        {
            var loggerMock = new Mock<ILogger<VendorService>>();
            var repositoryMock = new Mock<IRepository>();
            logger = loggerMock.Object;

            var userId = "testUserId";
            var firstName = "John";
            var lastName = "Doe";
            var phoneNumber = "1234567890";

            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "testUser"
            };

            repositoryMock.Setup(r => r.GetByIdAsync<ApplicationUser>(userId)).ReturnsAsync(user);

            vendorService = new VendorService(repositoryMock.Object, logger);

            var model = new BecomeVendorModel
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };

            await vendorService.Create(userId, model);

            repositoryMock.Verify(r => r.AddAsync(It.IsAny<Vendor>()), Times.Once);
            repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            Assert.That(firstName, Is.EqualTo(user.FirstName));
            Assert.That(lastName, Is.EqualTo(user.LastName));
        }

        [Test]
        public async Task CreateUserNotFoundExceptionTest()
        {
            var loggerMock = new Mock<ILogger<VendorService>>();
            var repo = new Repository(applicationDbContext);
            logger = loggerMock.Object;

            vendorService = new VendorService(repo, logger);

            var userId = "testUserId";

            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            var model = new BecomeVendorModel
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890"
            };

            var ex = Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await vendorService.Create(userId, model);
            });

            Assert.That(ex.Message, Is.EqualTo("User was not found."));
        }

        [Test]
        public async Task CreateExceptionTest()
        {
            var loggerMock = new Mock<ILogger<VendorService>>();
            var repo = new Repository(applicationDbContext);
            vendorService = new VendorService(repo, loggerMock.Object);

            var userId = "testUserId";

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "testUser"
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var model = new BecomeVendorModel
            {
                FirstName = "John",
                LastName = "Doe",
            };

            Assert.ThrowsAsync<ApplicationException>(async () =>
            {
                await vendorService.Create(userId, model);
            });

            var vendor = await repo.AllReadonly<Vendor>()
                                 .FirstOrDefaultAsync(v => v.User_Id == userId);

            Assert.Null(vendor); 
        }


        [Test]
        public async Task ExistsByIdTest()
        {
            var loggerMock = new Mock<ILogger<VendorService>>();
            var repo = new Repository(applicationDbContext);
            logger = loggerMock.Object;
            vendorService = new VendorService(repo, logger);

            var exists = await vendorService.ExistsById("dea12856-c198-4129-b3f3-b893d8395082");

            Assert.IsTrue(exists);
        }

        [Test]
        public async Task GetVendorIdTest()
        {
            var repo = new Repository(applicationDbContext);
            var vendorService = new VendorService(repo, null);

            var userId = "testUserId";

            var user = new ApplicationUser()
            {
                Id = userId,
                UserName = "testUser"
            };

            await repo.AddAsync(user);
            await repo.SaveChangesAsync();

            var vendor = new Vendor()
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                User_Id = userId
            };

            await repo.AddAsync(vendor);
            await repo.SaveChangesAsync();

            var vendorId = await vendorService.GetVendorId(userId);

            Assert.That(vendorId, Is.EqualTo(vendor.Id));
        }

        [Test]
        public async Task UserWithPhoneNumberExistsTest()
        {
            var repo = new Repository(applicationDbContext);
            var vendorService = new VendorService(repo, null);

            var phoneNumber = "1234567890";

            var vendorWithPhoneNumber = new Vendor()
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = phoneNumber,
                User = new ApplicationUser { IsActive = true }
            };

            await repo.AddAsync(vendorWithPhoneNumber);
            await repo.SaveChangesAsync();

            var phoneNumberExists = await vendorService.UserWithPhoneNumberExists(phoneNumber);

            Assert.IsTrue(phoneNumberExists);

            var nonExistingPhoneNumber = "9876543210";
            var phoneNumberNotExists = await vendorService.UserWithPhoneNumberExists(nonExistingPhoneNumber);

            Assert.IsFalse(phoneNumberNotExists);
        }

        [TearDown]
        public void TearDown()
        {
            applicationDbContext.Dispose();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Xunit;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CanPurchase_InvalidCustomerId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var customerId = -1;
            var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new TestDbContext(dbContextOptions))
            {
                dbContext.Customers.Add(new Customer
                {
                    Id = 1,
                    Name = "Customer 1"
                });
                dbContext.SaveChanges();

                var customerService = new CustomerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    await customerService.CanPurchase(customerId, 100);
                });
            }
        }
        [Fact]
        public async Task CanPurchase_InvalidPurchaseValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = -10; 
            var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new TestDbContext(dbContextOptions))
            {
                dbContext.Customers.Add(new Customer 
                { 
                    Id = 2, 
                    Name = "Customer 2" 
                });
                dbContext.SaveChanges();

                var customerService = new CustomerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
                {
                    await customerService.CanPurchase(customerId, purchaseValue);
                });
            }
        }
        [Fact]
        public async Task CanPurchase_CustomerDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var customerId = 3; 
            var purchaseValue = 100; 
            var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new TestDbContext(dbContextOptions))
            {
                var customerService = new CustomerService(dbContext);

                // Act & Assert
                await Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    await customerService.CanPurchase(customerId, purchaseValue);
                });
            }
        }
        [Fact]
        public async Task CanPurchase_CustomerHasOrdersInThisMonth_ReturnsFalse()
        {
            // Arrange
            var customerId = 4;
            var purchaseValue = 100;
            var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new TestDbContext(dbContextOptions))
            {
                dbContext.Customers.Add(new Customer
                {
                    Id = 4,
                    Name = "Customer 4"
                });

                var baseDate = DateTime.UtcNow.AddMonths(-1);
                dbContext.Orders.Add(new Order
                {
                    CustomerId = 4,
                    OrderDate = baseDate.AddDays(15)
                });

                dbContext.SaveChanges();

                var customerService = new CustomerService(dbContext);

                // Act
                var result = await customerService.CanPurchase(customerId, purchaseValue);

                // Assert
                Assert.False(result);
            }
        }
        [Fact]
        public async Task CanPurchase_CustomerNeverBoughtBeforeAndPurchaseValueGreaterThan100_ReturnsFalse()
        {
            // Arrange
            var customerId = 5;
            var purchaseValue = 150; 
            var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new TestDbContext(dbContextOptions))
            {
                dbContext.Customers.Add(new Customer
                {
                    Id = 5,
                    Name = "Customer 5"
                });

                dbContext.SaveChanges();

                var customerService = new CustomerService(dbContext);

                // Act
                var result = await customerService.CanPurchase(customerId, purchaseValue);

                // Assert
                Assert.False(result);
            }
        }
    }
}

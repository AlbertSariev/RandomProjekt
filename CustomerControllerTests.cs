using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project.Business;
using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTests
{
    class CustomerControllerTests
    {
        [Test]
        public void GetAllCustomer_Returns_All_Customer()
        {
            var data = new List<Customer>
            {
                new Customer { Id=1, FirstName = "BBB", LastName="aaa", Age=11, Pnone="0876162312"},
                new Customer {Id=1, FirstName = "ABB", LastName="baa", Age=12, Pnone="0876362312"},
                new Customer {Id=1, FirstName = "CBB", LastName="caa", Age=13, Pnone="0872162312"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var controller = new CustomerController(mockContext.Object);
            var customers = controller.GetAll();

            int expectedCount = 3;
            int realCount = customers.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllCustomers does not return all customers");
        }
        [Test]
        public void GetcustomerById_Returns_Correct_customer()
        {
            var data = new List<Customer>
            {
                 new Customer { Id=1, FirstName = "BBB", LastName="aaa", Age=11, Pnone="0876162312"},
                new Customer {Id=1, FirstName = "ABB", LastName="baa", Age=12, Pnone="0876362312"},
                new Customer {Id=1, FirstName = "CBB", LastName="caa", Age=13, Pnone="0872162312"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Customer>>();
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Customers).Returns(mockSet.Object);

            var controller = new CustomerController(mockContext.Object);
            var customer = controller.Get(1);

            Assert.AreEqual(1, customer.Id, "Method GetCustomerById does not return the correct Customer");
        }
        [Test]
        public void AddCustomer_Creates_And_Add_Customer_In_Database()
        {
            var mockSet = new Mock<DbSet<Customer>>();

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(m => m.Customers).Returns(mockSet.Object);

            var controller = new CustomerController(mockContext.Object);

            controller.Add(new Customer { Id = 1, FirstName = "BBB", LastName = "aaa", Age = 11, Pnone = "0876162312" });

            mockSet.Verify(m => m.Add(It.IsAny<Customer>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [Test]
        public void DeleteCustomer_Removes_Customer_From_Database()
        {
            var data = new List<Customer>
            {
                 new Customer { Id=1, FirstName = "BBB", LastName="aaa", Age=11, Pnone="0876162312"},
                new Customer {Id=2, FirstName = "ABB", LastName="baa", Age=12, Pnone="0876362312"},
                new Customer {Id=3, FirstName = "CBB", LastName="caa", Age=13, Pnone="0872162312"},
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<AirportSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AirportSystemContext(options);

            context.AddRange(data);
            context.SaveChanges();

            var customerController = new CustomerController(context);
            var dbSet = context.Set<Customer>();

            var expected = dbSet.Count() - 1;
            customerController.Delete(1);

            int real = dbSet.Count();

            Assert.AreEqual(expected, real, "DeleteCustomer does not remove customer from database");
        }
    }
}

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
    class AirportControllerTests
    {

        [Test]
        public void GetAllAirport_Returns_All_Airport()
        {
            var data = new List<Airport>
            {
                new Airport { Name = "BBB"},
                new Airport { Name = "ZZZ"},
                new Airport { Name = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Airport>>();
            mockSet.As<IQueryable<Airport>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Airports).Returns(mockSet.Object);

            var controller = new AirportController(mockContext.Object);
            var airports = controller.GetAll();

            int expectedCount = 3;
            int realCount = airports.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllAirports does not return all airports");
        }
        [Test]
        public void GetAirportById_Returns_Correct_Airport()
        {
            var data = new List<Airport>
            {
                new Airport { Id = 1, Name = "BBB"},
                new Airport { Id = 2, Name = "ZZZ"},
                new Airport { Id = 3, Name = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Airport>>();
            mockSet.As<IQueryable<Airport>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Airport>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Airports).Returns(mockSet.Object);

            var controller = new AirportController(mockContext.Object);
            var airport = controller.Get(1);

            Assert.AreEqual(1, airport.Id, "Method GetAirportById does not return the correct airport");
        }
        [Test]
        public void AddAirport_Creates_And_Add_Airport_In_Database()
        {
            var mockSet = new Mock<DbSet<Airport>>();

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(m => m.Airports).Returns(mockSet.Object);

            var controller = new AirportController(mockContext.Object);

            controller.Add(new Airport { Name = "PINKO" });

            mockSet.Verify(m => m.Add(It.IsAny<Airport>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [Test]
        public void DeleteAirport_Removes_Airport_From_Database()
        {
            var data = new List<Airport>
            {
                new Airport { Name = "BBB"},
                new Airport { Name = "ZZZ"},
                new Airport { Name = "AAA"},
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<AirportSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AirportSystemContext(options);

            context.AddRange(data);
            context.SaveChanges();

            var airportController = new AirportController(context);
            var dbSet = context.Set<Airport>();

            var expected = dbSet.Count() - 1;
            airportController.Delete(1);

            int real = dbSet.Count();

            Assert.AreEqual(expected, real, "DeleteAirport does not remove airport from database");
        }
    }
}


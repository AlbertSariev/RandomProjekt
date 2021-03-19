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
    class PlaneAirportControllerTests
    {
        [Test]
        public void GetAllFlights_Returns_All_Flights()
        {
            var data = new List<PlaneAirport>
            {
                new PlaneAirport { Id = 1, Gate = "BBB"},
                new PlaneAirport { Id = 2, Gate = "ZZZ"},
                new PlaneAirport { Id = 3, Gate = "AAA"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PlaneAirport>>();
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.PlanesAirports).Returns(mockSet.Object);

            var controller = new PlaneAirportController(mockContext.Object);
            var planeAirports = controller.GetAll();

            int expectedCount = 3;
            int realCount = planeAirports.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllFlights does not return all flights");
        }
        [Test]
        public void GetFlightById_Returns_Correct_Flight()
        {
            var data = new List<PlaneAirport>
            {
                new PlaneAirport { Id = 1, Gate = "BBB"},
                new PlaneAirport { Id = 2, Gate = "ZZZ"},
                new PlaneAirport { Id = 3, Gate = "AAA"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PlaneAirport>>();
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.PlanesAirports).Returns(mockSet.Object);

            var controller = new PlaneAirportController(mockContext.Object);
            var city = controller.Get(1);

            Assert.AreEqual(1, city.Id, "Method GetFlightById does not return the correct flight");
        }

        [Test]
        public void ListAllFlightsByAirport_Returns_The_Right_Flight()
        {
            var data = new List<PlaneAirport>
            {
                new PlaneAirport { Id =1, PlaneId = 3, AirportId = 3, Gate="A" },
                new PlaneAirport { Id =2, PlaneId = 4, AirportId = 4, Gate="B" },
                new PlaneAirport { Id =3, PlaneId = 5, AirportId = 5, Gate="C" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<PlaneAirport>>();
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<PlaneAirport>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.PlanesAirports).Returns(mockSet.Object);

            var controller = new PlaneAirportController(mockContext.Object);
            Airport airport = new Airport { Id = 4, CityId = 3, Name = "Mihi" };
            List<PlaneAirport> flights = controller.GetAllByAirportId(airport);

            var planes = controller.GetAll().Where(x=>x.AirportId==airport.Id);
            int expected = planes.Count();
            int real = flights.Count();

            Assert.AreEqual(expected,real, "Method GetAllFlightsByAirport does not return all flights");
        }
    }
}

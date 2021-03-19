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
    class CityControllerTests
    {
        [Test]
        public void GetAllCities_Returns_All_Cities()
        {
            var data = new List<City>
            {
                new City { Name = "BBB"},
                new City { Name = "ZZZ"},
                new City { Name = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<City>>();
            mockSet.As<IQueryable<City>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<City>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<City>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<City>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Cities).Returns(mockSet.Object);

            var controller = new CityController(mockContext.Object);
            var cities = controller.GetAll();

            int expectedCount = 3;
            int realCount = cities.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllCities does not return all cities");
        }
        [Test]
        public void GetCityById_Returns_Correct_City()
        {
            var data = new List<City>
            {
                new City { Id = 1, Name = "BBB"},
                new City { Id = 2, Name = "ZZZ"},
                new City { Id = 3, Name = "AAA"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<City>>();
            mockSet.As<IQueryable<City>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<City>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<City>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<City>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Cities).Returns(mockSet.Object);

            var controller = new CityController(mockContext.Object);
            var city = controller.Get(1);

            Assert.AreEqual(1, city.Id, "Method GetCityById does not return the correct city");
        }
        [Test]
        public void AddCity_Creates_And_Add_City_In_Database()
        {
            var mockSet = new Mock<DbSet<City>>();

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(m => m.Cities).Returns(mockSet.Object);

            var controller = new CityController(mockContext.Object);

            controller.Add(new City { Name = "PINKO" });

            mockSet.Verify(m => m.Add(It.IsAny<City>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [Test]
        public void DeleteCity_Removes_City_From_Database()
        {
            var data = new List<City>
            {
                new City { Id = 1, Name = "BBB"},
                new City { Id = 2, Name = "ZZZ"},
                new City { Id = 3, Name = "AAA"}
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<AirportSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AirportSystemContext(options);

            context.AddRange(data);
            context.SaveChanges();

            var cityController = new CityController(context);
            var dbSet = context.Set<City>();

            var expected = dbSet.Count() - 1;
            cityController.Delete(1);

            int real = dbSet.Count();

            Assert.AreEqual(expected, real, "DeleteCity does not remove city from database");
        }
    }
}

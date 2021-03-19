using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Project.Business;
using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectTests
{
    public class PlaneControllerTests
    {
        [Test]
        public void GetAllPlanes_Returns_All_Planes()
        {
            var data = new List<Plane>
            {
                new Plane { Name = "BBB", CityId = 2, Tickets = { new Ticket()} },
                new Plane { Name = "ZZZ", CityId = 3, Tickets = { new Ticket()}},
                new Plane { Name = "AAA", CityId = 4, Tickets = { new Ticket()}},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plane>>();
            mockSet.As<IQueryable<Plane>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Planes).Returns(mockSet.Object);

            var controller = new PlaneController(mockContext.Object);
            var planes = controller.GetAll();

            int expectedCount = 3;
            int realCount = planes.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllPlanes does not return all planes");

        }

        [Test]
        public void GetPlaneById_Returns_Correct_Plane()
        {
            var data = new List<Plane>
            {
                new Plane {Id = 1, Name = "BBB", CityId = 2, Tickets = { new Ticket()} },
                new Plane {Id = 2, Name = "ZZZ", CityId = 3, Tickets = { new Ticket()}},
                new Plane {Id = 3, Name = "AAA", CityId = 4, Tickets = { new Ticket()}},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plane>>();
            mockSet.As<IQueryable<Plane>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plane>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Planes).Returns(mockSet.Object);

            var controller = new PlaneController(mockContext.Object);
            var plane = controller.Get(1);

            Assert.AreEqual(1, plane.Id, "Method GetPlaneById does not return the correct plane");
        }

        [Test]
        public void AddPlane_Creates_And_Add_Plane_In_Database()
        {
            var mockSet = new Mock<DbSet<Plane>>();

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(m => m.Planes).Returns(mockSet.Object);

            var controller = new PlaneController(mockContext.Object);

            controller.Add(new Plane { Name = "PINKO" });

            mockSet.Verify(m => m.Add(It.IsAny<Plane>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        //[Test]
        //public void UpdatePlane_Updates_Plane_Via_Context()
        //{
        //    var data = new List<Plane>
        //    {
        //        new Plane {Id = 1, Name = "BBB", CityId = 2, Tickets = { new Ticket()} },
        //        new Plane {Id = 2, Name = "ZZZ", CityId = 3, Tickets = { new Ticket()}},
        //        new Plane {Id = 3, Name = "AAA", CityId = 4, Tickets = { new Ticket()}},
        //    }.AsQueryable();

        //    var options = new DbContextOptionsBuilder<AirportSystemContext>()
        //        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //        .Options;

        //    var context = new AirportSystemContext(options);

        //    context.AddRange(data);
        //    context.SaveChanges();

        //    var planeController = new PlaneController(context);
        //    var dbSet = context.Set<Plane>();

        //    Plane plane = new Plane { Id = 4, Name = "CCC", CityId = 4,Tickets = { new Ticket() } };
        //    context.Add(plane);
            
        //    planeController.Update(plane);
        //    plane.Name = "CCC";

        //    var expected = "CCC";
        //    var actual = dbSet.FirstOrDefault(x => x.Id == 4);

        //    Assert.AreEqual(expected, actual.Name, "UpdatePlane does not update the plane it the database");
        //}

        [Test]
        public void DeletePlane_Removes_Plane_From_Database()
        {
            var data = new List<Plane>
            {
                new Plane {Id = 1, Name = "BBB", CityId = 2, Tickets = { new Ticket()} },
                new Plane {Id = 2, Name = "ZZZ", CityId = 3, Tickets = { new Ticket()}},
                new Plane {Id = 3, Name = "AAA", CityId = 4, Tickets = { new Ticket()}},
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<AirportSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AirportSystemContext(options);

            context.AddRange(data);
            context.SaveChanges();

            var planeController = new PlaneController(context);
            var dbSet = context.Set<Plane>();

            var expected = dbSet.Count() - 1;
            planeController.Delete(1);

            int real = dbSet.Count();

            Assert.AreEqual(expected, real, "DeletePlane does not remove plane from database");
        }

        //[Test]
        //public void ListAllPlanesByDestination_Returns_The_Right_Planes()
        //{
        //    var data = new List<Plane>
        //    {
        //        new Plane { Id = 1, Name = "BBB", CityId = 2, Tickets=new List<Ticket>()},
        //        new Plane { Id = 2, Name = "ZZZ", CityId = 3, Tickets=new List<Ticket>()},
        //        new Plane { Id = 3, Name = "AAA", CityId = 1, Tickets=new List<Ticket>()}
        //    }.AsQueryable();

        //    var options = new DbContextOptionsBuilder<AirportSystemContext>()
        //         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        //         .Options;

        //    var context = new AirportSystemContext(options);

        //    context.AddRange(data);
        //    context.SaveChanges();

        //    var mockContext = new Mock<AirportSystemContext>();
          
        //    City city = new City { Id = 2, Name = "Sofia" };
        //    context.Add(city);
        //    var controller = new PlaneController(mockContext.Object);

        //    Plane destinations = controller.GetAll().FirstOrDefault(x => x.CityId == city.Id);

        //    var planes = controller.GetPlaneByDestination(city.Name);
            

        //    Assert.AreEqual(destinations, planes, "Method GetAllPlanesByDestination does not return all planes");

        //}
    }
}
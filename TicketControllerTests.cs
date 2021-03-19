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
    class TicketControllerTests
    {
        [Test]
        public void GetAllTickets_Returns_All_Tickets()
        {
            var data = new List<Ticket>
            {
                new Ticket { Id = 1, Type = "BBB"},
                new Ticket { Id = 2, Type = "ZZZ"},
                new Ticket { Id = 3, Type = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Tickets).Returns(mockSet.Object);

            var controller = new TicketController(mockContext.Object);
            var tickets = controller.GetAll();

            int expectedCount = 3;
            int realCount = tickets.Count;

            Assert.AreEqual(expectedCount, realCount, "Method GetAllTickets does not return all tickets");
        }
        [Test]
        public void GetTicketById_Returns_Correct_Ticket()
        {
            var data = new List<Ticket>
            {
               new Ticket { Id = 1, Type = "BBB"},
                new Ticket { Id = 2, Type = "ZZZ"},
                new Ticket { Id = 3, Type = "AAA"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ticket>>();
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Ticket>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(c => c.Tickets).Returns(mockSet.Object);

            var controller = new TicketController(mockContext.Object);
            var ticket = controller.Get(1);

            Assert.AreEqual(1, ticket.Id, "Method GetTicketById does not return the correct ticket");
        }
        [Test]
        public void AddTicket_Creates_And_Add_Ticket_In_Database()
        {
            var mockSet = new Mock<DbSet<Ticket>>();

            var mockContext = new Mock<AirportSystemContext>();
            mockContext.Setup(m => m.Tickets).Returns(mockSet.Object);

            var controller = new TicketController(mockContext.Object);

            controller.Add(new Ticket { Id = 1, Type = "BBB" });

            mockSet.Verify(m => m.Add(It.IsAny<Ticket>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
        [Test]
        public void DeleteTicket_Removes_Ticket_From_Database()
        {
            var data = new List<Ticket>
            {
                new Ticket { Id = 1, Type = "BBB"},
                new Ticket { Id = 2, Type = "ZZZ"},
                new Ticket { Id = 3, Type = "AAA"},
            }.AsQueryable();

            var options = new DbContextOptionsBuilder<AirportSystemContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AirportSystemContext(options);

            context.AddRange(data);
            context.SaveChanges();

            var ticketController = new TicketController(context);
            var dbSet = context.Set<Ticket>();

            var expected = dbSet.Count() - 1;
            ticketController.Delete(1);

            int real = dbSet.Count();

            Assert.AreEqual(expected, real, "DeleteTicket does not remove ticket from database");
        }
    }
}

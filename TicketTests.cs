using NUnit.Framework;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTests
{
    class TicketTests
    {
        [Test]
        public void IncreasePriceWhenBisnessClass()
        {
            Ticket ticket = new Ticket();
            double price = ticket.CalculatePrice("Business", 16);
            double expectidPrice = 156;
            Assert.AreEqual(price, expectidPrice, "There is no increase in price for bisiness class.");

        }
        [Test]
        public void IncreasePriceWhenFirstClass()
        {
            Ticket ticket = new Ticket();
            double price = ticket.CalculatePrice("First", 16);
            double expectidPrice = 169;
            Assert.AreEqual(price, expectidPrice, "There is no increase in price for first class.");
        }
        [Test]
        public void DecreasePriceForChildren()
        {
            Ticket ticket = new Ticket();
            double price = ticket.CalculatePrice("Economic", 12);
            double expectidPrice = 65;
            Assert.AreEqual(price, expectidPrice, "There is no decrease in price for children.");
        }
    }
}

using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
    public class TicketController
    {
        private AirportSystemContext context;

        public TicketController()
        {
            this.context = new AirportSystemContext();
        }
        public TicketController(AirportSystemContext context)
        {
            this.context = context;
        }
        public List<Ticket> GetAll()
        {
            return context.Tickets.ToList();
        }
        public Ticket Get(int id)
        {
            var ticket = context.Tickets.FirstOrDefault(e => e.Id == id);
            return ticket;
        }
        public void Add(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            context.SaveChanges();
        }
        public void Update(Ticket ticket)
        {
            var item = context.Tickets.FirstOrDefault(e => e.Id == ticket.Id);
            if (item != null)
            {
                this.context.Entry(item).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Tickets.Remove(item);
            this.context.SaveChanges();
        }
    }
}

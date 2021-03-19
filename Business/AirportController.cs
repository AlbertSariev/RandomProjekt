using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
    public class AirportController
    {
        private AirportSystemContext context;

        public AirportController()
        {
            this.context = new AirportSystemContext();
        }
        public AirportController(AirportSystemContext context)
        {
            this.context = context;
        }

        public List<Airport> GetAll()
        {
            return context.Airports.ToList();
        }
        public Airport Get(int id)
        {
            var airport = context.Airports.FirstOrDefault(e => e.Id == id);
            return airport;
        }
        public void Add(Airport airport)
        {
            context.Airports.Add(airport);
            context.SaveChanges();
        }
        public void Update(Airport airport)
        {
            var item = context.Airports.FirstOrDefault(e => e.Id == airport.Id);
            if (item != null)
            {
                this.context.Entry(item).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Airports.Remove(item);
            this.context.SaveChanges();
        }
    }
}

using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
    public class CityController
    {
        private AirportSystemContext context;

        public CityController()
        {
            this.context = new AirportSystemContext();
        }
        public CityController(AirportSystemContext context)
        {
            this.context = context;
        }

        public List<City> GetAll()
        {
            return context.Cities.ToList();
        }
        public City Get(int id)
        {
            var city = context.Cities.FirstOrDefault(e => e.Id == id);
            return city;
        }
        public void Add(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();
        }
        public void Update(City city)
        {
            var item = context.Cities.FirstOrDefault(e => e.Id == city.Id);
            if (item != null)
            {
                this.context.Entry(item).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Cities.Remove(item);
            this.context.SaveChanges();
        }
    }
}

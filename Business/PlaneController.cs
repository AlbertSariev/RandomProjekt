using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
    public class PlaneController
    {
        private AirportSystemContext context;

        public PlaneController()
        {
            this.context = new AirportSystemContext();
        }
        public PlaneController(AirportSystemContext context)
        {
            this.context = context;
        }

        public List<Plane> GetAll()
        {
            return context.Planes.ToList();
        }
        public Plane Get(int id)
        {
            var plane = context.Planes.FirstOrDefault(e => e.Id == id);
            return plane;
        }
        public void Add(Plane plane)
        {
            context.Planes.Add(plane);
            context.SaveChanges();
        }
        public void Update(Plane plane)
        {
            var item = context.Planes.FirstOrDefault(e => e.Id == plane.Id);

            if (item != null)
            {
                this.context.Entry(item).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Planes.Remove(item);
            this.context.SaveChanges();
        }
        public Plane GetPlaneByDestination(string destination)
        {
            return context.Planes.FirstOrDefault(x => x.City.Name == destination);
        }
    }
}

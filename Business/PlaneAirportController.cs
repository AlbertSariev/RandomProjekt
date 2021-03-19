using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
   public class PlaneAirportController
    {
        private AirportSystemContext context;

        public PlaneAirportController()
        {
            this.context = new AirportSystemContext();
        }

        public List<PlaneAirport> GetAll()
        {
            return context.PlanesAirports.ToList();
        }
        public PlaneAirportController(AirportSystemContext context)
        {
            this.context = context;
        }

        public PlaneAirport Get(int id)
        {
            var flights = context.PlanesAirports.FirstOrDefault(e => e.Id == id);
            return flights;
        }
        public List<PlaneAirport> GetAllByAirportId(Airport airport)
        {
            return context.PlanesAirports.Where(x => x.AirportId == airport.Id).ToList();
        }
        public PlaneAirport GetAllByPlaneIdAndDestination(Plane plane, string destination)
        {
            return context.PlanesAirports.FirstOrDefault(f => f.PlaneId == plane.Id && f.Plane.City.Name == destination);
        }
    }
}

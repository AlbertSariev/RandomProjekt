using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace Project.Data.Models
{
    public class PlaneAirport
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey(nameof(Plane))]
        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }

        
        [ForeignKey(nameof(Airport))]
        public int AirportId { get; set; }

        public virtual Airport Airport { get; set; }

        [Required]
        public string Gate { get; set; }

        public DateTime FlightOn { get; set; }

        public override string ToString()
        {
            return $"Number: {this.Id} \n" +
                $"Plane: {Plane.Name} \n" +
                $"From: {Airport.City.Name} \n" +
                $"To: {Plane.City.Name} \n" +
                $"Gate: {this.Gate} \n" +
                $"On: {this.FlightOn.ToString("dd MMMM yyyy")}г. \n";
        }
    }
}

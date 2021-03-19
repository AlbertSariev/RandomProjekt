using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project.Data.Models
{
    public class Plane
    {
        public Plane()
        {
            this.Tickets = new List<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
       
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }


        public override string ToString()
        {
            return $"Plane -> {this.Name}, Destination -> {City.Name}";
        }
    }
}

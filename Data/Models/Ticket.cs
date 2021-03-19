using Project.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project.Data.Models
{
    public class Ticket : ICalculatePriceTicket
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Plane))]
        public int PlaneId { get; set; }
        public virtual Plane Plane { get; set; }

        [Required]
        [MaxLength(10)]
        public string Type { get; set; }
        public double Price { get; set; }

        public DateTime BoughtOn { get; set; }

        public double CalculatePrice(string type, int age)
        {
            double price = 130;

            switch (type)
            {
                case "First":
                    price += 0.3 * price;
                    break;
                case "Business":
                    price += 0.2 * price;
                    break;
                case "Economic":
                    break;
            }
            if(age < 13)
            {
                price -= 0.5 * price;
            }

            return price;
        }
    }
}

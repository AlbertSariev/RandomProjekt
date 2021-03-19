using Project.Data;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Business
{
    public class CustomerController
    {
        private AirportSystemContext context;

        public CustomerController()
        {
            this.context = new AirportSystemContext();
        }
        public CustomerController(AirportSystemContext context)
        {
            this.context = context;
        }

        public List<Customer> GetAll()
        {
            return context.Customers.ToList();
        }
        public Customer Get(int id)
        {
            var customer = context.Customers.FirstOrDefault(e => e.Id == id);
            return customer;
        }
        public void Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        public void Update(Customer customer)
        {
            var item = context.Customers.FirstOrDefault(e => e.Id == customer.Id);
            if (item != null)
            {
                this.context.Entry(item).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var item = this.Get(id);
            this.context.Customers.Remove(item);
            this.context.SaveChanges();
        }
    }
}

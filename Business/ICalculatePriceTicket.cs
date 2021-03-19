using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Business
{
    interface ICalculatePriceTicket
    {
        double CalculatePrice(string type, int age);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class SideOrder
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }

        public ICollection<Order> Orders { set; get; }
    }
}
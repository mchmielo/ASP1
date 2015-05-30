using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class OrderPizza
    {
        public int ID { set; get; }
        public int PizzaID { set; get; }
        public int DoughID { set; get; }
        public int OrderID { set; get; }
        public int PizzaQuantity { set; get; }

        public virtual Pizza Pizza { set; get; }
        public virtual Dough Dough { set; get; }
        public virtual Order Order { set; get; }
    }
}

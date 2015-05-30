using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class Pizza
    {
        public int ID { set; get; }
        [Display(Name = "Nazwa")]
        public string Name { set; get; }
        public string Picture { set; get; }

        public ICollection<Topping> Toppings { set; get; }
        public ICollection<OrderPizza> OrderPizzas { set; get; }
    }
}
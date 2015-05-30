using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class Topping
    {
        public int ID { set; get; }
        [Display(Name = "Nazwa")]
        public string Name { set; get; }
        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal Price { set; get; }

        public ICollection<Pizza> Pizzas { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class Dough
    {
        public int ID { set; get; }
        [Display(Name="Rozmiar")]
        public int Size { set; get; }
        [Display(Name="Cena")]
        [DataType(DataType.Currency)]
        public decimal Price { set; get; }

        public ICollection<OrderPizza> OrderPizzas { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class Order
    {
        public int ID { set; get; }
        [Display(Name = "Adres zamówienia")]
        public string DeliveryAddress { set; get; }
        [DataType(DataType.Date)]
        [Display(Name="Data")]
        public DateTime Date { set; get; }
        [Display(Name = "Status zamówienia")]
        public string Status { set; get; }

        public ICollection<OrderPizza> OrderPizzas { set; get; }
        public ICollection<SideOrder> SideOrders { set; get; }
    }
}
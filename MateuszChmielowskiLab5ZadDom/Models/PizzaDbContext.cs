using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext() : base("DefaultConnection") 
        {
            Database.SetInitializer<PizzaDbContext>(new DropCreateDatabaseIfModelChanges<PizzaDbContext>());
        }

        public virtual DbSet<Pizza> Pizzas { set; get; }
        public virtual DbSet<Dough> Doughs { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Topping> Toppings { set; get; }
        public virtual DbSet<OrderPizza> OrderPizzas { set; get; }
        public virtual DbSet<SideOrder> SideOrders { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    public class Repository
    {
        public static PizzaDbContext databaseContext;

        static Repository()
        {
            databaseContext = new PizzaDbContext();
        }
        public static IEnumerable<Pizza> GetAllPizzas()
        {
            IEnumerable<Pizza> query = Enumerable.Empty<Pizza>();
            try
            {
                query = from pizza in databaseContext.Pizzas.Include("Toppings") select pizza;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.InnerException != null)
                    Console.WriteLine(e.InnerException.Message);
            }
            return query;
        }

        public static IEnumerable<Dough> GetAllDougs()
        {
            return (from dough in databaseContext.Doughs select dough);
        }

        public static IEnumerable<Topping> GetToppingByName(string Name)
        {
            return (from topping in databaseContext.Toppings select topping).Where(t => t.Name == Name);
        }

        public static IEnumerable<SideOrder> GetAllSideOrders()
        {
            return (from sideOrder in databaseContext.SideOrders select sideOrder);
        }

        public static void AddSomeDough()
        {
            for (int i = 0; i < 3; i++)
            {
                Dough dough = new Dough();
                dough.Price = 10 + i*5;
                dough.Size = 25 + i*9;
                databaseContext.Doughs.Add(dough);
            }
            databaseContext.SaveChanges();
        }
        public static void AddSomeToppings()
        {
            Topping topping = new Topping();
            topping.Name = "ser mozzarella";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "pieczarki";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "sos pomidorowy";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "papryka";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "kukurydza";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "salami";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "szynka";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "kurczak";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "cebula";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            topping.Name = "krewetki";
            topping.Price = 3;
            databaseContext.Toppings.Add(topping);
            topping.Name = "anchois";
            topping.Price = 3;
            databaseContext.Toppings.Add(topping);
            topping.Name = "oliwki";
            topping.Price = 2;
            databaseContext.Toppings.Add(topping);
            databaseContext.SaveChanges();
        }
        public static void AddSomePizzas()
        {
            Pizza pizza = new Pizza();
            pizza.Name = "Margherita";
            pizza.Toppings = Enumerable.Concat(Repository.GetToppingByName("ser mozzarella"), Repository.GetToppingByName("sos pomidorowy")).ToList();
            databaseContext.Pizzas.Add(pizza);
            databaseContext.SaveChanges();
        }
    }
}
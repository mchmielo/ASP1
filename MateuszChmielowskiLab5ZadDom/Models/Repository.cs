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

        public static Pizza GetPizzaByID(int ID)
        {
            return (from pizza in databaseContext.Pizzas.Include("Toppings") select pizza).Where(pizza => pizza.ID == ID).FirstOrDefault();
        }

        public static IEnumerable<Dough> GetAllDougs()
        {
            return (from dough in databaseContext.Doughs select dough);
        }
        public static Dough GetDoughByID(int ID)
        {
            return (from dough in databaseContext.Doughs select dough).Where(dough => dough.ID == ID).FirstOrDefault();
        }

        public static IEnumerable<Topping> GetToppingByName(string Name)
        {
            return (from topping in databaseContext.Toppings select topping).Where(t => t.Name == Name);
        }

        public static IEnumerable<SideOrder> GetAllSideOrders()
        {
            return (from sideOrder in databaseContext.SideOrders select sideOrder);
        }

        public static void AddListOfOrderPizzas(List<OrderPizza> orderPizzas)
        {
            for (int i = 0; i < orderPizzas.Count; i++)
            {
                databaseContext.OrderPizzas.Add(orderPizzas[i]);
            }
            databaseContext.SaveChanges();
        }

        public static int AddOrder(Order order)
        {
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
            return order.ID;
        }

        public static IEnumerable<Order> GetAllOrders()
        {
            return (from order in databaseContext.Orders.Include("OrderPizzas.Pizza.Toppings").Include("OrderPizzas.Dough") select order);
        }
        public static IEnumerable<Order> GetAllUnfinishedOrders()
        {
            return (from order in databaseContext.Orders.Include("OrderPizzas.Pizza.Toppings").Include("OrderPizzas.Dough") select order).Where(order=>order.Status != "KoniecZamowienia");
        }
        public static Order GetOrderByID(int ID)
        {
            return (from order in databaseContext.Orders select order).Where(order => order.ID == ID).FirstOrDefault();
        }

        public static void UpdateOrderStatusByID(int ID, string newStatus)
        {
            Order orderToUpdate = GetOrderByID(ID);
            orderToUpdate.Status = newStatus;
            databaseContext.SaveChanges();
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
    }
}
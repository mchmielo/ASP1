using MateuszChmielowskiLab5ZadDom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MateuszChmielowskiLab5ZadDom.Controllers
{
    public class PizzaController : Controller
    {
        Repository repository = new Repository();
        //
        // GET: /Pizza/

        public ActionResult Index()
        {
            RedirectToAction("ShowAll");
            return View();
        }
        public ActionResult ShowMenu()
        {
            var PizzasAndDoughs = new Tuple<IEnumerable<Pizza>, IEnumerable<Dough>>(Repository.GetAllPizzas(), Repository.GetAllDougs());
            return View(PizzasAndDoughs);
        }

        public ActionResult ShowAll()
        {
            var PizzasAndDoughs = new Tuple<IEnumerable<Pizza>, IEnumerable<Dough> >(Repository.GetAllPizzas(), Repository.GetAllDougs());
            return View(PizzasAndDoughs);
        }
        public ActionResult ChoosePizza()
        {
            var PizzasAndDoughs = new Tuple<IEnumerable<Pizza>, IEnumerable<Dough>, IEnumerable<SideOrder> >(Repository.GetAllPizzas(), Repository.GetAllDougs(), Repository.GetAllSideOrders());
            return View(PizzasAndDoughs);
        }

        public ActionResult ChooseDough(string[] pizzas)
        {
            List<OrderPizza> listOfOrderPizza = new List<OrderPizza>();
            foreach (var pizzaID in pizzas)
            {
                Pizza pizza = Repository.GetPizzaByID(int.Parse(pizzaID));
                OrderPizza orderPizza = new OrderPizza();
                orderPizza.Pizza = pizza;
                orderPizza.PizzaID = int.Parse(pizzaID);
                orderPizza.PizzaQuantity = 1;
                listOfOrderPizza.Add(orderPizza);
            }
            var OrderPizzasAndDoughs = new Tuple<IEnumerable<OrderPizza>, IEnumerable<Dough>>(listOfOrderPizza, Repository.GetAllDougs());
            return View(OrderPizzasAndDoughs);
        }

        public ActionResult OrderSummary(List<OrderPizza> orderPizza)
        {
            for (int i = 0; i < orderPizza.Count; i++)
            {
                orderPizza[i].Dough = Repository.GetDoughByID(orderPizza[i].DoughID);
                orderPizza[i].Pizza = Repository.GetPizzaByID(orderPizza[i].PizzaID);
            }
            var orderPizzasAndOrder = new Tuple<IEnumerable<OrderPizza>, Order>(orderPizza, new Order());
            return View(orderPizzasAndOrder);
        }
        [HttpPost]
        public ActionResult AcceptOrder(List<OrderPizza> orderPizza, Order order)
        {
            order.Date = DateTime.Now;
            order.Status = "PoczatekZamowienia";
            int orderID = Repository.AddOrder(order);
            for (int i = 0; i < orderPizza.Count; i++)
            {
                orderPizza[i].Dough = Repository.GetDoughByID(orderPizza[i].DoughID);
                orderPizza[i].Pizza = Repository.GetPizzaByID(orderPizza[i].PizzaID);
                orderPizza[i].PizzaQuantity = 1;
                orderPizza[i].OrderID = orderID;
            }
            Repository.AddListOfOrderPizzas(orderPizza);
            return View();
        }

        public ActionResult EditOrderStatus()
        {
            var orders = Repository.GetAllUnfinishedOrders();
            return View(orders);
        }

        public ActionResult EditChosenStatus(int id)
        {
            Order order = Repository.GetOrderByID(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult SaveOrder(Order order)
        {
            Repository.UpdateOrderStatusByID(order.ID, order.Status);
            return RedirectToAction("EditOrderStatus");
        }
    }
}

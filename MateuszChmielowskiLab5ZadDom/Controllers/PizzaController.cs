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
        /// <summary>
        /// Obiekt do komunikacji z bazą danych, zawierający kontekst bazy danych
        /// i statyczne metody zapytań.
        /// </summary>
        Repository repository = new Repository();

        /// <summary>
        /// Metoda przekierowująca ze strony Index na ShowAll.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            RedirectToAction("ShowAll");
            return View();
        }
        /// <summary>
        /// Metoda wyświetla menu pizzerii. Do widoku przekazuje obiekt klasy Tuple<IEnumerable<Pizza>,IEnumerable<Dough>>
        /// zawierający wszystkie pizze i ciasta do pizzy.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowMenu()
        {
            var PizzasAndDoughs = new Tuple<IEnumerable<Pizza>, IEnumerable<Dough>>(Repository.GetAllPizzas(), Repository.GetAllDoughs());
            return View(PizzasAndDoughs);
        }
        #region OrderPizza
        /// <summary>
        /// Metoda wyświetla menu pizzerii z możliwością wyboru danych pizz. 
        /// Do widoku przekazuje obiekt klasy Tuple<IEnumerable<Pizza>,IEnumerable<Dough>>
        /// zawierający wszystkie pizze i ciasta do pizzy.
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowAll()
        {
            var PizzasAndDoughs = new Tuple<IEnumerable<Pizza>, IEnumerable<Dough> >(Repository.GetAllPizzas(), Repository.GetAllDoughs());
            return View(PizzasAndDoughs);
        }
        /// <summary>
        /// Metoda otrzymuje z widoku ShowAll string z numerami ID pizz wybranymi przez użytkownika.
        /// Tworzy listę obiektów orderPizza, do których wpisuje wybrane przez użytkownika pizze
        /// i przekazuje ją, razem z listą wszystkich ciast do widoku. W widoku użytkownik wybiera rozmiar
        /// ciasta dla każdej pizzy.
        /// </summary>
        /// <param name="pizzas">
        /// String z numerami ID pizz wybranymi przez użytkownika.
        /// </param>
        /// <returns></returns>
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
            var OrderPizzasAndDoughs = new Tuple<IEnumerable<OrderPizza>, IEnumerable<Dough>>(listOfOrderPizza, Repository.GetAllDoughs());
            return View(OrderPizzasAndDoughs);
        }
        /// <summary>
        /// Metoda otrzymuje z widoku ChooseDough wybrane przez użytkownika pizze razem z rozmiarem
        /// ciasta. Przekazuje do widoku OrderSummary otrzymaną listę oraz obiekt klasy Order.
        /// W widoku OrderSummary użytkownik ma możliwość zobaczyć podsumowanie zamówienia, wpisać 
        /// adres do dostawy i potwierdzić zamówienie.
        /// </summary>
        /// <param name="orderPizza"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metoda otrzymuje z widoku OrderSummary listę pizz i ciast wybrane przez użytkownika oraz
        /// wypełnione pola obiektu klasy Order, następnie wypełnia zamówienie ustawiając status rozpoczęcia
        /// zamówienia i teraźniejszą datę. Zamówienie dodane jest do bazy, a następnie połączone jest relacjami
        /// z pizzami i ciastami za pomocą listy obiektów orderPizza.
        /// </summary>
        /// <param name="orderPizza"></param>
        /// <param name="order"></param>
        /// <returns></returns>
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
#endregion
        #region EditOrderStatus
        /// <summary>
        /// Metoda przekazuje do widoku wszystkie nieukończone zlecenia. Widok wyświetla krótkie podsumowanie
        /// każdego zlecenia i umożliwia przejście do strony z edycją statusu danego zlecenia.
        /// </summary>
        /// <returns></returns>
        public ActionResult EditOrderStatus()
        {
            var orders = Repository.GetAllUnfinishedOrders();
            return View(orders);
        }
        /// <summary>
        /// Widok przesyła zamówienie do widoku wybrane przez użytkownika do edycji.
        /// </summary>
        /// <param name="id">
        /// ID wybranego przez uzytkownika zamówienia.
        /// </param>
        /// <returns></returns>
        public ActionResult EditChosenStatus(int id)
        {
            Order order = Repository.GetOrderByID(id);
            return View(order);
        }
        /// <summary>
        /// Metoda aktualizuje status zamówienia.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveOrder(Order order)
        {
            Repository.UpdateOrderStatusByID(order.ID, order.Status);
            return RedirectToAction("EditOrderStatus");
        }
        #endregion
    }
}

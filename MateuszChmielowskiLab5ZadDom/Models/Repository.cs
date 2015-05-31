using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5ZadDom.Models
{
    /// <summary>
    /// Klasa służy do obsługi zapytań do lokalnej bazy danych.
    /// </summary>
    public class Repository
    {
        /// <summary>
        /// Statyczny kontekst bazy danych, po to aby było tylko jedno połączenie do bazy.
        /// </summary>
        public static PizzaDbContext databaseContext;

        /// <summary>
        /// Konstruktor statyczny bezparametryczny tworzy statyczny kontekst bazy danych.
        /// </summary>
        static Repository()
        {
            databaseContext = new PizzaDbContext();
        }
        #region Pizza
        /// <summary>
        /// Metoda zwraca wszystkie obiekty klasy pizza z bazy danych.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Pizza> GetAllPizzas()
        {
            IEnumerable<Pizza> query = Enumerable.Empty<Pizza>();
            query = from pizza in databaseContext.Pizzas.Include("Toppings") select pizza;
            return query;
        }
        /// <summary>
        /// Metoda zwraca obiekt pizza o podanym ID z bazy danych.
        /// </summary>
        /// <param name="ID">
        /// ID obiektu do zwrócenia.
        /// </param>
        /// <returns></returns>
        public static Pizza GetPizzaByID(int ID)
        {
            return (from pizza in databaseContext.Pizzas.Include("Toppings") select pizza).Where(pizza => pizza.ID == ID).FirstOrDefault();
        }
        #endregion
        #region Dough
        /// <summary>
        /// Metoda zwraca wszystkie obiekty ciasta do pizzy z bazy danych.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Dough> GetAllDoughs()
        {
            return (from dough in databaseContext.Doughs select dough);
        }
        /// <summary>
        /// Metoda zwraca obiekt ciasta do pizzy o podanym ID z bazy danych.
        /// </summary>
        /// <param name="ID">
        /// ID obiektu do zwrócenia
        /// </param>
        /// <returns></returns>
        public static Dough GetDoughByID(int ID)
        {
            return (from dough in databaseContext.Doughs select dough).Where(dough => dough.ID == ID).FirstOrDefault();
        }
        #endregion Dough
        #region Topping
        /// <summary>
        /// Metoda zwraca składniki pizzy na podstawie nazwy.
        /// </summary>
        /// <param name="Name">
        /// Nazwa do filtrowania składników pizzy w bazie danych.
        /// </param>
        /// <returns></returns>
        public static IEnumerable<Topping> GetToppingByName(string Name)
        {
            return (from topping in databaseContext.Toppings select topping).Where(t => t.Name == Name);
        }
        #endregion
        #region SideOrder
        /// <summary>
        /// Metoda zwraca wszystkie dodatki do pizzy z bazy danych.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SideOrder> GetAllSideOrders()
        {
            return (from sideOrder in databaseContext.SideOrders select sideOrder);
        }
        #endregion
        #region OrderPizza
        /// <summary>
        /// Metoda dodaje do bazy danych orderPizzas.Count elementów do tabeli
        /// OrderPizzas, podanych jako lista orderPizzas.
        /// </summary>
        /// <param name="orderPizzas">
        /// Lista pizz na zamówieniu do dodania do bazy danych.
        /// </param>
        public static void AddListOfOrderPizzas(List<OrderPizza> orderPizzas)
        {
            for (int i = 0; i < orderPizzas.Count; i++)
            {
                databaseContext.OrderPizzas.Add(orderPizzas[i]);
            }
            databaseContext.SaveChanges();
        }
        #endregion
        #region Order
        /// <summary>
        /// Metoda służy do dodania podanego jako parametr order zamówienia do bazy danych.
        /// </summary>
        /// <param name="order">
        /// Zamówienie do dodania do bazy danych.
        /// </param>
        /// <returns>
        /// ID dodanego zamówienia.
        /// </returns>
        public static int AddOrder(Order order)
        {
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
            return order.ID;
        }
        /// <summary>
        /// Metoda zwraca wszystkie zamówienia z bazy danych, razem z danymi powiązanymi relacjami.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Order> GetAllOrders()
        {
            return (from order in databaseContext.Orders.Include("OrderPizzas.Pizza.Toppings").Include("OrderPizzas.Dough") select order);
        }
        /// <summary>
        /// Metoda zwraca wszystkie zamówienia, których status jest różny od "KoniecZamowienia" - zamówienia, które nie zostały zakończone.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Order> GetAllUnfinishedOrders()
        {
            return (from order in databaseContext.Orders.Include("OrderPizzas.Pizza.Toppings").Include("OrderPizzas.Dough") select order).Where(order=>order.Status != "KoniecZamowienia");
        }
        /// <summary>
        /// Metoda zwraca zamówienie o podanym jako parametr numerze ID.
        /// </summary>
        /// <param name="ID">
        /// ID obiektu do zwrócenia.
        /// </param>
        /// <returns></returns>
        public static Order GetOrderByID(int ID)
        {
            return (from order in databaseContext.Orders select order).Where(order => order.ID == ID).FirstOrDefault();
        }
        /// <summary>
        /// Metoda aktualizuje zamówienie o podanym jako parametr numerze ID, ustawiając pole Status na wartość
        /// równą parametrowi newStatus.
        /// </summary>
        /// <param name="ID">
        /// ID obiektu do aktualizacji.
        /// </param>
        /// <param name="newStatus">
        /// Nowy status zamówienia.
        /// </param>
        public static void UpdateOrderStatusByID(int ID, string newStatus)
        {
            Order orderToUpdate = GetOrderByID(ID);
            orderToUpdate.Status = newStatus;
            databaseContext.SaveChanges();
        }
        #endregion
        /// <summary>
        /// Metoda dodaje przykładowe dane do tabeli Doughs.
        /// </summary>
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
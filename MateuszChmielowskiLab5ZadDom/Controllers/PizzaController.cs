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
    }
}

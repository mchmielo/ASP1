using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MateuszChmielowskiLab5.Models;

namespace MateuszChmielowskiLab5.Controllers
{
    public class MovieController : Controller
    {
        public LibraryDbContext context = new LibraryDbContext();
        //
        // GET: /Movie/

        public ActionResult Index()
        {

            Movie movie = (from movies in context.Movies select movies).FirstOrDefault();
            return View(new Movie() { ReleaseDate = DateTime.Now });
        }

        public void DbInit()
        {
            context.Movies.Add(new Movie() { Name = "Matrix", ReleaseDate = DateTime.Now.AddYears(-15) });
            for (int i = 0; i < 7; i++)
            {
                context.Movies.Add(new Movie() { Name = "Piła" + i.ToString(), ReleaseDate = DateTime.Now.AddYears((-15) + i) });
            }
            context.SaveChanges();
        }

        public ActionResult ShowAll()
        {
            var query = (from movies in context.Movies select movies);
            return View(query.ToList());
        }
        [HttpPost]
        public ActionResult AddNewMovie(Movie newMovie)
        {
            context.Movies.Add(newMovie);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }
        [HttpPost]
        public ActionResult DeleteById(int movieId)
        {
            Movie movieToDelete = context.Movies.Find(movieId);
            context.Movies.Remove(movieToDelete);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

        public ActionResult UpdateById(int id)
        {
            Movie movieToUpdate = (from movies in context.Movies select movies).Where(x => x.Id == id).FirstOrDefault();
            return View(movieToUpdate);
        }

        [HttpPost]
        public string Save(Movie movieToUpdate)
        {
            try
            {
                Movie movie = context.Movies.Find(movieToUpdate.Id);
                movie.Name = movieToUpdate.Name;
                movie.ReleaseDate = movie.ReleaseDate;
                context.SaveChanges();
            }
            catch(Exception exception)
            {
                return exception.Message;
            }

            return string.Empty;
        }
        [HttpPost]
        public ActionResult AddMovie(Movie newMovie)
        {
            context.Movies.Add(newMovie);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }

    }
}

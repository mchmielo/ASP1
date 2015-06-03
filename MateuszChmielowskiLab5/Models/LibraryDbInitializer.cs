using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace MateuszChmielowskiLab5.Models
{
    public class LibraryDbInitializer : DropCreateDatabaseAlways<LibraryDbContext>
    {
        protected override void Seed(LibraryDbContext context)
        {
            context.Movies.Add(new Movie() { Name = "Matrix", ReleaseDate = DateTime.Now.AddYears(-15) });
            for (int i = 0; i < 7; i++)
            {
                context.Movies.Add(new Movie() { Name = "Piła" + i.ToString(), ReleaseDate = DateTime.Now.AddYears((-15) + i) });
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}

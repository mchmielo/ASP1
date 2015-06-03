using MateuszChmielowskiLab5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("DefaultConnection") 
        {
            Database.SetInitializer<LibraryDbContext>(new LibraryDbInitializer()); 
        }
        public virtual DbSet<Movie> Movies { set; get; }
    }
}
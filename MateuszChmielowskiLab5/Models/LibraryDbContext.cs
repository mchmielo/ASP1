using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("DefaultConnection") { }
        public DbSet<Movies> Movies;
    }
}
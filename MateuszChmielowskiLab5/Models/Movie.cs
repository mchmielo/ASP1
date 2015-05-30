using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MateuszChmielowskiLab5.Models
{
    public class Movie
    {
        public int Id { set; get; }
        [Display(Name="Nazwa")]
        public string Name { set; get; }
        [DataType(DataType.Date)]
        [Display(Name="Data")]
        public DateTime ReleaseDate { set; get; }
    }
}
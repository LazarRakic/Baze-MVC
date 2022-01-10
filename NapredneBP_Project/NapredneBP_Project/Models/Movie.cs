using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        [Display(Name = "Naziv")]
        public String Title { get; set; }

        public String Description { get; set; }

        public String ImageUri { get; set; }

        public int PublishingDate { get; set; }

        public String Genre { get; set; }

        public double Rate { get; set; }

        public int RateCount { get; set; }
        public double RateSum { get; set; }

        public IEnumerable<Person> ListOfActors { get; set; }
        public IEnumerable<Person> ListOfDirectors { get; set; }
    }
}

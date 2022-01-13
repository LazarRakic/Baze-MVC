using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Models
{
    public class MovieDTO
    {
        public Guid Id { get; set; }
        public String Title { get; set; }

        public String Description { get; set; }

        public String ImageUri { get; set; }

        public int PublishingDate { get; set; }

        public double Rate { get; set; }

        public IEnumerable<Person> ListOfActors { get; set; }

        public IEnumerable<Person> ListOfDirectors { get; set; }

        public IEnumerable<string> Labels { get; set; }

        public String Comment { get; set; }

        public Dictionary<string, string[]> keyValueComments { get; set; }

        public MovieDTO()
        {
            keyValueComments = new Dictionary<string, string[]>();
        }
    }
}

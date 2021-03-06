using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Models
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int BornYear { get; set; }
        public List<Relationship> Relationships { get; set; }

        public Person()
        {
            Relationships = new List<Relationship>();
        }
    }
}


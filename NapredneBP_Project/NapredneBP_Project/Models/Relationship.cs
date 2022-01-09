using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NapredneBP_Project.Models
{
    public class Relationship
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PersonName { get; set; }
        public string MovieName { get; set; }
    }
}

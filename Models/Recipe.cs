using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class Recipe
    {
        public string Name{ get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string Category { get; set; }
    }
}

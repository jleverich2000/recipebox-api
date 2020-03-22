using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class Recipe
    {
        public string Name{ get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Direction> Directions { get; set; }
        public CategoryEnum Category { get; set; }
    }
    public class RecipeListItem
    {
        public string Name { get; set; }
        public string RecipeId { get; set; }
    }

    public class Ingredient
    {
        public string unitType { get; set; }
        public float quantity { get; set; }
        public string name { get; set; }
    }

    public class Direction
    {
        public int step { get; set; }
        public string body { get; set; }
    }

    public enum CategoryEnum
    { 
        dinner, 
        breakfast
    }

}

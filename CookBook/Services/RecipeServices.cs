using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Repository;

namespace CookBook.Services
{
    public class RecipeServices : IRecipeServices
    {
        private readonly Recipe _recipeItems;
        private readonly AppDb _appDb;

        public RecipeServices()
        {
            _recipeItems = new Recipe();
            _appDb = new AppDb();

        }

        public Recipe AddRecipeItems(Recipe items)
        {
           
            return items;
        }

        public Recipe GetRecipeItems(string name)
        {
            Recipe recipe = new Recipe();
            recipe = _appDb.GetRecipeByName(name);
            return recipe;
        }


    }
}

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

        public void SaveRecipe(Recipe recipe)
        {
            var recipeId = Guid.NewGuid().ToString();
            var recipeDbo = new RecipeDbo(recipeId, recipe.Name, recipe.Category);
            _appDb.SaveToRecipeTable(recipeDbo);
            _appDb.SaveToIngredientsTable(recipeId, recipe.Ingredients);
            _appDb.SaveToDirectionsTable(recipeId, recipe.Directions);

        }

        public Recipe GetRecipeItems(string name)
        {
            Recipe recipe = new Recipe();
            recipe = _appDb.GetRecipeByName(name);
            return recipe;
        }

        public Recipe GetRecipeById(string recipeId)
        {
            Recipe recipe = new Recipe();
            recipe = _appDb.GetRecipeById(recipeId);
            recipe.Ingredients = _appDb.GetIngredientsById(recipeId);
            recipe.Directions = _appDb.GetDirectionsById(recipeId);

            return recipe;
        }

        public List<SearchResult> Search(string term)
        {
           return _appDb.SearchByTerm(term);
            
        }

    }
}

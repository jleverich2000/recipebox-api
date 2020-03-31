using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public interface IRecipeServices
    {
        void SaveRecipe(Recipe items);

        Recipe GetRecipeItems(string name);

        List<SearchResult> Search(string term);

        Recipe GetRecipeById(string recipeId);

    }
}

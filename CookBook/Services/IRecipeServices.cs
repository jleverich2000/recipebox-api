using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public interface IRecipeServices
    {
        void AddRecipeItems(Recipe items);

       Recipe GetRecipeItems(String name);
    }
}

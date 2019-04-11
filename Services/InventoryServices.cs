using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Repository;

namespace CookBook.Services
{
    public class InventoryServices : IInventoryServices
    {
        private readonly Recipe _inventoryItems;
        private readonly AppDb _appDb;

        public InventoryServices()
        {
            _inventoryItems = new Recipe();
            _appDb = new AppDb();

        }

        public Recipe AddInventoryItems(Recipe items)
        {
           
            return items;
        }

        public Recipe GetInventoryItems(string name)
        {
            Recipe recipe = new Recipe();
            recipe = _appDb.GetRecipeByName(name);
            return recipe;
        }


    }
}

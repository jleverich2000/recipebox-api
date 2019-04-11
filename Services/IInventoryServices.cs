using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Services
{
    public interface IInventoryServices
    {
        Recipe AddInventoryItems(Recipe items);

       Recipe GetInventoryItems(String name);
    }
}

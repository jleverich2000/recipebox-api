using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using CookBook.Services;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace CookBook.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IRecipeServices _services;

        public InventoryController(IRecipeServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("AddInventoryItems")]

        public Recipe AddInventoryItems(Recipe items)
        {
            var inventoryItems = _services.AddRecipeItems(items);

            return inventoryItems;
        }

        [HttpGet]
        [Route("GetRecipe")]
        public JsonResult GetRecipe()
        {
            string pattern = @"^\s*""?|""?\s*$";
            Regex rgx = new Regex(pattern);
            string name = rgx.Replace(HttpContext.Request.Query["name"].ToString(), "");

            Recipe recipe = _services.GetRecipeItems(name);

            return new JsonResult(JsonConvert.SerializeObject(recipe));
        }

        [HttpGet]
        [Route("Get-test")]
        public JsonResult GetTest()
        {
           
            return new JsonResult("this was from my API");
        }

    }

}
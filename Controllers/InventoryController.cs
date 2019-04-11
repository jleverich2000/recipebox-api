using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using CookBook.Services;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace CookBook.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryServices _services;

        public InventoryController(IInventoryServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("AddInventoryItems")]

        public Recipe AddInventoryItems(Recipe items)
        {
            var inventoryItems = _services.AddInventoryItems(items);

            return inventoryItems;
        }

        [HttpGet]
        [Route("GetRecipe")]
        public JsonResult GetRecipe()
        {
            string pattern = @"^\s*""?|""?\s*$";
            Regex rgx = new Regex(pattern);
            string name = rgx.Replace(HttpContext.Request.Query["name"].ToString(), "");

            Recipe recipe = _services.GetInventoryItems(name);

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
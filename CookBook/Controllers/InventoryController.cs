using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using CookBook.Services;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;

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
        [Route("save_recipe")]
        public IActionResult SaveRecipe(Recipe recipe)
        {
            try
            {
                _services.AddRecipeItems(recipe);

            }
            catch(Exception e)
            {
                return BadRequest();
            }

            return Ok();
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
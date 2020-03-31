using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CookBook.Models;
using CookBook.Services;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
                _services.SaveRecipe(recipe);

            }
            catch (Exception e)
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
        [Route("search")]
        public JsonResult GetSearch()
        {
            string pattern = @"^\s*""?|""?\s*$";
            Regex rgx = new Regex(pattern);
            string term = rgx.Replace(HttpContext.Request.Query["term"].ToString(), "");

           List<SearchResult> results = _services.Search(term);

            return new JsonResult(results);

        }

        [HttpGet]
        [Route("recipeById")]
        public JsonResult GetRecipeById()
        {
            string pattern = @"^\s*""?|""?\s*$";
            Regex rgx = new Regex(pattern);
            string recipeId = rgx.Replace(HttpContext.Request.Query["recipeId"].ToString(), "");



            Recipe results = _services.GetRecipeById(recipeId);


            return new JsonResult(results);

        }

        [HttpGet]
        [Route("Get-test")]
        public JsonResult GetTest()
        {
           
            return new JsonResult("this was from my API");
        }

    }

}
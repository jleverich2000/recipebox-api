﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public string Name{ get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Direction> Directions { get; set; }
        public CategoryEnum Category { get; set; }
    }

    public class RecipeDbo
    { 
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public CategoryEnum Category { get; set; }

        public RecipeDbo(string recipeId, string name, CategoryEnum category) {
            RecipeId = recipeId;
            Name = name;
            Category = category;
        }
    }


    public class RecipeListItem
    {
        public string Name { get; set; }
        public string RecipeId { get; set; }
    }

    public class Ingredient
    {
        public string unitType { get; set; }
        public float quantity { get; set; }
        public string name { get; set; }
        public int orderOf { get; set; }
    }

    public class Direction
    {
        public int StepNumber { get; set; }
        public string Instruction { get; set; }
    }

    public class IngredientDbo
    {
        public string RecipeId { get; set; }
        public string unitType { get; set; }
        public float quantity { get; set; }
        public string name { get; set; }
        public int orderOf { get; set; }
    }

    public class DirectionDbo
    {
        public string RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string Instruction { get; set; }
    }


    public enum CategoryEnum
    { 
        dinner, 
        breakfast,
        drink
        
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CookBook.Models;
using System.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;

namespace CookBook.Repository
{
    public class AppDb
    {
        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("server = 127.0.0.1; user id = jleverich; password = 9673; port = 3306; database = cookbook; ");
        }
        public void SaveRecipeToDB(Recipe recipe)
        {
            var name = recipe.Name;
            var ingredients = JsonConvert.SerializeObject(recipe.Ingredients);
            var directions = JsonConvert.SerializeObject(recipe.Directions);
            var category = recipe.Category;

            String query = "INSERT INTO recipes (name, ingredients, directions, category) VALUES (@name,@ingredients,@directions, @category)";

            using (MySqlCommand command = new MySqlCommand(query, Connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@ingredients", ingredients);
                command.Parameters.AddWithValue("@directions", directions);
                command.Parameters.AddWithValue("@category", category);

                Connection.Open();
                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    Console.WriteLine("Error inserting data into Database!");
            }

        }

        public Recipe GetRecipeByName(string name)
        {
            Recipe recipe = new Recipe();
            if (Connection.State == ConnectionState.Closed) {
                Connection.Open();
            }
            string sql = "SELECT * FROM recipes WHERE name='"+ name +"'";
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                recipe.Name = dataReader["name"].ToString();
                //recipe.Ingredients = dataReader["ingredients"].ToString();
                //recipe.Directions = dataReader["directions"].ToString();
                //recipe.Category = dataReader["category"].ToString();
            }

            //close Data Reader
            dataReader.Close();

            return recipe;
        }

        public Recipe GetRecipeById(string recipeId)
        {
            Recipe recipe = new Recipe();
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            string sql = "SELECT * FROM recipes WHERE id='" + recipeId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                recipe.RecipeId = dataReader["id"].ToString();
                recipe.Name = dataReader["name"].ToString();
                CategoryEnum category = (CategoryEnum)Enum.Parse(typeof(CategoryEnum), dataReader["category"].ToString(), true);
                recipe.Category = category;
            }

            //close Data Reader
            dataReader.Close();

            return recipe;
        }

        public List<Ingredient> GetIngredientsById(string recipeId)
        {

            List<Ingredient> ingredientList = new List<Ingredient>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            string sql = "SELECT * FROM ingredient WHERE recipe_id='" + recipeId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.SingleResult);

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                var ingredient = new Ingredient();

                ingredient.name = dataReader["name"].ToString();
                ingredient.unitType = dataReader["unit_type"].ToString();
                ingredient.quantity = float.Parse(dataReader["quantity"].ToString());
                ingredientList.Add(ingredient);
            }

            //close Data Reader
            dataReader.Close();

            return ingredientList;
        }

        public List<Direction> GetDirectionsById(string recipeId)
        {

            List<Direction> directionList = new List<Direction>();

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            string sql = "SELECT * FROM directions WHERE recipe_id='" + recipeId + "'";
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.SingleResult);

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                var direction = new Direction();

                direction.Instruction = dataReader["instruction"].ToString();
                direction.StepNumber = int.Parse( dataReader["step_number"].ToString());
                directionList.Add(direction);
            }

            //close Data Reader
            dataReader.Close();

            return directionList;
        }

        public List<SearchResult> SearchByTerm(string term)
        {
            var listOfSearchResults = new List<SearchResult>();
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            string sql = "SELECT * FROM recipes WHERE name LIKE '%" + term + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, Connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();

            //Read the data and store them in the list
            while (dataReader.Read())
            {
                var result = new SearchResult();
                result.Id = dataReader["id"].ToString();
                result.Name = dataReader["name"].ToString();

                listOfSearchResults.Add(result);
            }

            //close Data Reader
            dataReader.Close();

            return listOfSearchResults;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}

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

        public void SaveToRecipeTable(RecipeDbo recipe)
        {
            String query = "INSERT INTO recipes (name, id, category) VALUES (@name,@id, @category)";

            using (MySqlCommand command = new MySqlCommand(query, Connection))
            {
                command.Parameters.AddWithValue("@name", recipe.Name);
                command.Parameters.AddWithValue("@id", recipe.RecipeId);
                command.Parameters.AddWithValue("@category", recipe.Category);

                Connection.Open();
                int result = command.ExecuteNonQuery();
                Connection.Close();
                // Check Error
                if (result < 0)
                    Console.WriteLine("Error inserting data into Database!");
            }
        }

        public void SaveToIngredientsTable(string recipeId, List<Ingredient> ingredients)
        {
            foreach(var ingredient in ingredients)
            {
                String query = "INSERT INTO ingredient (recipe_id, name, unit_type, quantity, order_of ) VALUES (@recipe_id, @name, @unit_Type, @quantity, @order_Of)";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@recipe_id", recipeId);
                    command.Parameters.AddWithValue("@name", ingredient.name);
                    command.Parameters.AddWithValue("@unit_Type", ingredient.unitType);
                    command.Parameters.AddWithValue("@quantity", ingredient.quantity);
                    command.Parameters.AddWithValue("@order_Of", ingredient.orderOf);


                    Connection.Open();
                    int result = command.ExecuteNonQuery();
                    Connection.Close();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }
        }

        public void SaveToDirectionsTable(string recipeId, List<Direction> directions)
        {
            foreach (Direction direction in directions)
            {
                String query = "INSERT INTO directions (recipe_id, instruction, step_number ) VALUES (@recipe_id, @instruction, @step_number)";

                using (MySqlCommand command = new MySqlCommand(query, Connection))
                {
                    command.Parameters.AddWithValue("@recipe_id", recipeId);
                    command.Parameters.AddWithValue("@instruction", direction.Instruction);
                    command.Parameters.AddWithValue("@step_number", direction.StepNumber);

                    Connection.Open();
                    int result = command.ExecuteNonQuery();
                    Connection.Close();
                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
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

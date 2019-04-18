using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CookBook.Models;
using System.Data;

namespace CookBook.Repository
{
    public class AppDb
    {
        public MySqlConnection Connection;

        public AppDb()
        {
            Connection = new MySqlConnection("server = 127.0.0.1; user id = jleverich; password = 9673; port = 3306; database = cookbook; ");
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
                recipe.Ingredients = dataReader["ingredients"].ToString();
                recipe.Directions = dataReader["directions"].ToString();
                recipe.Category = dataReader["category"].ToString(); 
            }

            //close Data Reader
            dataReader.Close();

            return recipe;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}

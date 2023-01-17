using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;

namespace MyKitchen_ServerSide1.Models.DAL
{
    public class DataServices
    {
        private SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;
        }



        public List<Recipe> ReadAllRecipes() /////////////////////////////////////////////////////
        {

            SqlConnection con1 = Connect();
            SqlConnection con2 = Connect();
            //// Create Command
            SqlCommand command = CreateGetRecipes(con1);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Recipe> recipeList = new List<Recipe>();

            while (dr.Read())
            {
                int recipesID = Convert.ToInt32(dr["recipesID"]);
                string name = dr["name"].ToString();
                string image = dr["image"].ToString();
                string cookingMethod = dr["cookingMethod"].ToString();
                string time = dr["time"].ToString();
                int[] ingredient_data;

                using (SqlCommand command2 = new SqlCommand("SELECT ingredientsID FROM [dbo].[IngredientsInRecipes] WHERE recipesID = @value", con2))
                {
                    command2.Parameters.AddWithValue("@value", recipesID);

                    using (SqlDataReader reader = command2.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        List<string> values = new List<string>();
                        while (reader.Read())
                        {
                            values.Add(reader["ingredientsID"].ToString());
                        }

                        string[] ingredientsChosen = values.ToArray();
                        ingredient_data = Array.ConvertAll(ingredientsChosen, int.Parse);
                    }
                    
                }

                recipeList.Add(new Recipe(recipesID, name, image, cookingMethod, time, ingredient_data));

            }

            con1.Close();
            con2.Close();

            return recipeList;
        }
        private SqlCommand CreateGetRecipes(SqlConnection con)
        {

            SqlCommand command = new SqlCommand();

            command.CommandText = "SPGetAllRecipes";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }



        public List<Ingredient> ReadAllIngredients()
        {

            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateGetIngredients(con);

            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

            List<Ingredient> IngredientList = new List<Ingredient>();
            while (dr.Read())
            {
                int ingredientsID = Convert.ToInt32(dr["ingredientsID"]);
                string name = dr["name"].ToString();
                string image = dr["image"].ToString();
                string calories = dr["calories"].ToString();

                IngredientList.Add(new Ingredient(ingredientsID,name, image, calories));

            }

            con.Close();

            return IngredientList;
        }
        private SqlCommand CreateGetIngredients(SqlConnection con)
        {

            SqlCommand command = new SqlCommand();

            command.CommandText = "SPGetAllIngredients";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }




        public int Insert(Recipe recipe)
        {
            SqlConnection con = Connect();


            // Create Command
            SqlCommand command = CreateInsertCommand(con, recipe);

            // Execute
            int numAffected = command.ExecuteNonQuery();



            string query = @" SELECT MAX(recipesID) FROM [dbo].[Recipes]";
            SqlCommand myCommand2 = new SqlCommand(query, con);
            int recipesID = (Int32)myCommand2.ExecuteScalar();

            foreach (int value in recipe.Ingredient_data)
            {
                string query1 = $"INSERT INTO [dbo].[IngredientsInRecipes] ([recipesID],[ingredientsID]) VALUES (@recipesID,@value)";
                SqlCommand command3 = new SqlCommand(query1, con);
                command3.Parameters.AddWithValue("@recipesID", recipesID);
                command3.Parameters.AddWithValue("@value", value);
                command3.ExecuteNonQuery();
                
            }


            // Close Connection
            con.Close();

            return numAffected;

        }
        private SqlCommand CreateInsertCommand(SqlConnection con, Recipe recipe)
        {

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@name", recipe.Name);
            command.Parameters.AddWithValue("@image", recipe.Image);
            command.Parameters.AddWithValue("@cookingMethod", recipe.CookingMethod);
            command.Parameters.AddWithValue("@time", recipe.Time);

            command.CommandText = "spInsertRecipe";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }



    public int Insert(Ingredient ingredient)
        {
            SqlConnection con = Connect();

            // Create Command
            SqlCommand command = CreateInsertCommand(con, ingredient);

            // Execute
            int numAffected = command.ExecuteNonQuery();

            // Close Connection

            con.Close();

            return numAffected;

        }
        private SqlCommand CreateInsertCommand(SqlConnection con, Ingredient ingredient)
        {

            SqlCommand command = new SqlCommand();

            command.Parameters.AddWithValue("@name", ingredient.Name);
            command.Parameters.AddWithValue("@image", ingredient.Image);
            command.Parameters.AddWithValue("@calories", ingredient.Calories);

            command.CommandText = "spInsertIngredient";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds

            return command;
        }
    }
}
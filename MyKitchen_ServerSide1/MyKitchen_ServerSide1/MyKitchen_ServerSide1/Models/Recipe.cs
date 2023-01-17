using MyKitchen_ServerSide1.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyKitchen_ServerSide1.Models
{
    public class Recipe
    {
        int recipesID;
        string name;
        string image;
        string cookingMethod;
        string time;

        int[] ingredient_data;

        public Recipe() { }

        public Recipe(int recipesID, string name, string image, string cookingMethod, string time, int[] ingredient_data)
        {
            this.RecipesID = recipesID;
            this.Name = name;
            this.Image = image;
            this.CookingMethod = cookingMethod;
            this.Time = time;
            this.Ingredient_data = ingredient_data;
        }

        public int RecipesID { get => recipesID; set => recipesID = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string CookingMethod { get => cookingMethod; set => cookingMethod = value; }
        public string Time { get => time; set => time = value; }
        public int[] Ingredient_data { get => ingredient_data; set => ingredient_data = value; }



        public int Insert()
        {
            DataServices ds = new DataServices();
            int numInserted = ds.Insert(this);
            return numInserted;

        }

        public List<Recipe> Read()
        {
            DataServices ds = new DataServices();
            List<Recipe> rList = ds.ReadAllRecipes();
            return rList;
        }




    }
}
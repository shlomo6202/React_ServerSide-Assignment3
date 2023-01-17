using MyKitchen_ServerSide1.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyKitchen_ServerSide1.Models
{
    public class Ingredient
    {
        int ingredientsID;
        string name;
        string image;
        string calories;

        public Ingredient(int ingredientsID,string name, string image, string calories)
        {
            this.IngredientsID = ingredientsID;
            this.name = name;
            this.image = image;
            this.calories = calories;
        }

        public int IngredientsID { get; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string Calories { get => calories; set => calories = value; }

        public Ingredient() { }

        public int Insert()
        {
            DataServices ds = new DataServices();
            int numInserted = ds.Insert(this);
            return numInserted;

        }

        public List<Ingredient> Read()
        {
            DataServices ds = new DataServices();
            List<Ingredient> IngredientList = ds.ReadAllIngredients();
            return IngredientList;
        }
    }
}
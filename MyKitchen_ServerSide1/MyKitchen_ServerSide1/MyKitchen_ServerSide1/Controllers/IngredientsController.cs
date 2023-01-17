using MyKitchen_ServerSide1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyKitchen_ServerSide1.Controllers
{
    public class IngredientsController : ApiController
    {
        // GET api/<controller>
        public List<Ingredient> Get()
        {
            Ingredient ingredient = new Ingredient();
            return ingredient.Read();
        }

        //GET api/<controller>/5
        public void Get(int id)
        {

        }

        // POST api/<controller>
        public int Post([FromBody] Ingredient ingredient)
        {
            ingredient.Insert();
            return 1;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
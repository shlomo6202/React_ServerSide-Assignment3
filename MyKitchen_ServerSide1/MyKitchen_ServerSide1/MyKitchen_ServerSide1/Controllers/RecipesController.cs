using MyKitchen_ServerSide1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyKitchen_ServerSide1.Controllers
{
    public class RecipesController : ApiController
    {
        // GET api/<controller>
        public List<Recipe> Get()
        {
            Recipe r = new Recipe();
            return r.Read();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public int Post([FromBody] Recipe recipe)
        {
            recipe.Insert();
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
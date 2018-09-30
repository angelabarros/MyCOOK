using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using mycook.Models;

namespace mycook.Controllers
{
    public class ingredients_recipeAPIController : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/ingredients_recipeAPI
        public IQueryable<ingredients_recipe> Getingredients_recipe()
        {
            return db.ingredients_recipe;
        }

        // GET: api/ingredients_recipeAPI/5
        [ResponseType(typeof(ingredients_recipe))]
        public IHttpActionResult Getingredients_recipe(decimal id)
        {
            ingredients_recipe ingredients_recipe = db.ingredients_recipe.Find(id);
            if (ingredients_recipe == null)
            {
                return NotFound();
            }

            return Ok(ingredients_recipe);
        }

        // PUT: api/ingredients_recipeAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putingredients_recipe(decimal id, ingredients_recipe ingredients_recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredients_recipe.id_recipe)
            {
                return BadRequest();
            }

            db.Entry(ingredients_recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ingredients_recipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ingredients_recipeAPI
        [ResponseType(typeof(ingredients_recipe))]
        public IHttpActionResult Postingredients_recipe(ingredients_recipe ingredients_recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ingredients_recipe.Add(ingredients_recipe);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ingredients_recipeExists(ingredients_recipe.id_recipe))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = ingredients_recipe.id_recipe }, ingredients_recipe);
        }

        // DELETE: api/ingredients_recipeAPI/5
        [ResponseType(typeof(ingredients_recipe))]
        public IHttpActionResult Deleteingredients_recipe(decimal id)
        {
            ingredients_recipe ingredients_recipe = db.ingredients_recipe.Find(id);
            if (ingredients_recipe == null)
            {
                return NotFound();
            }

            db.ingredients_recipe.Remove(ingredients_recipe);
            db.SaveChanges();

            return Ok(ingredients_recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ingredients_recipeExists(decimal id)
        {
            return db.ingredients_recipe.Count(e => e.id_recipe == id) > 0;
        }
    }
}
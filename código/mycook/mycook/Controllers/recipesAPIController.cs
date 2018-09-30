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
    public class recipesAPIController : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/recipesAPI
        public IQueryable<recipe> Getrecipes()
        {
            return db.recipes;
        }


        public IQueryable<recipe> Getrecipes(decimal id, decimal number)
        {
            user u = db.users.Find(id);

            DbSet<recipe> receitas = null;

            foreach(recipe r in db.recipes)
            {
                foreach(preference p in db.preferences)
                {
                    if (r.preferences.Contains(p))
                    {
                        receitas.Add(r);
                    }
                }
            }
            return receitas;
        }


        // GET: api/recipesAPI/5
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Getrecipe(decimal id)
        {
            recipe recipe = db.recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        // PUT: api/recipesAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putrecipe(decimal id, recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != recipe.id_recipe)
            {
                return BadRequest();
            }

            db.Entry(recipe).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!recipeExists(id))
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

        // POST: api/recipesAPI
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Postrecipe(recipe recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.recipes.Add(recipe);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = recipe.id_recipe }, recipe);
        }

        // DELETE: api/recipesAPI/5
        [ResponseType(typeof(recipe))]
        public IHttpActionResult Deleterecipe(decimal id)
        {
            recipe recipe = db.recipes.Find(id);
            if (recipe == null)
            {
                return NotFound();
            }

            db.recipes.Remove(recipe);
            db.SaveChanges();

            return Ok(recipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool recipeExists(decimal id)
        {
            return db.recipes.Count(e => e.id_recipe == id) > 0;
        }
    }
}
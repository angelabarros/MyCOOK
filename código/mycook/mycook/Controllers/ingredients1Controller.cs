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
    public class ingredients1Controller : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/ingredients1
        public IQueryable<ingredient> Getingredients()
        {
            return db.ingredients;
        }

        // GET: api/ingredients1/5
        [ResponseType(typeof(ingredient))]
        public IHttpActionResult Getingredient(decimal id)
        {
            ingredient ingredient = db.ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            return Ok(ingredient);
        }

        // PUT: api/ingredients1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putingredient(decimal id, ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredient.id_ingredient)
            {
                return BadRequest();
            }

            db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ingredientExists(id))
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

        // POST: api/ingredients1
        [ResponseType(typeof(ingredient))]
        public IHttpActionResult Postingredient(ingredient ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ingredients.Add(ingredient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ingredient.id_ingredient }, ingredient);
        }

        // DELETE: api/ingredients1/5
        [ResponseType(typeof(ingredient))]
        public IHttpActionResult Deleteingredient(decimal id)
        {
            ingredient ingredient = db.ingredients.Find(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            db.ingredients.Remove(ingredient);
            db.SaveChanges();

            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ingredientExists(decimal id)
        {
            return db.ingredients.Count(e => e.id_ingredient == id) > 0;
        }
    }
}
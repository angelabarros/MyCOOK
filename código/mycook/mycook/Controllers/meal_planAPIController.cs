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
    public class meal_planAPIController : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/meal_planAPI
        public IQueryable<meal_plan> Getmeal_plan()
        {
            return db.meal_plan;
        }

        // GET: api/meal_planAPI/5
        [ResponseType(typeof(meal_plan))]
        public IHttpActionResult Getmeal_plan(decimal id)
        {
            meal_plan meal_plan = db.meal_plan.Find(id);
            if (meal_plan == null)
            {
                return NotFound();
            }

            return Ok(meal_plan);
        }

        // PUT: api/meal_planAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmeal_plan(decimal id, meal_plan meal_plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meal_plan.id_mealplan)
            {
                return BadRequest();
            }

            db.Entry(meal_plan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!meal_planExists(id))
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

        // POST: api/meal_planAPI
        [ResponseType(typeof(meal_plan))]
        public IHttpActionResult Postmeal_plan(meal_plan meal_plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.meal_plan.Add(meal_plan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = meal_plan.id_mealplan }, meal_plan);
        }

        // DELETE: api/meal_planAPI/5
        [ResponseType(typeof(meal_plan))]
        public IHttpActionResult Deletemeal_plan(decimal id)
        {
            meal_plan meal_plan = db.meal_plan.Find(id);
            if (meal_plan == null)
            {
                return NotFound();
            }

            db.meal_plan.Remove(meal_plan);
            db.SaveChanges();

            return Ok(meal_plan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool meal_planExists(decimal id)
        {
            return db.meal_plan.Count(e => e.id_mealplan == id) > 0;
        }
    }
}
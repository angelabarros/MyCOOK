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
    public class stepsAPIController : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/stepsAPI
        public IQueryable<step> Getsteps()
        {
            return db.steps;
        }

        // GET: api/stepsAPI/5
        [ResponseType(typeof(step))]
        public IHttpActionResult Getstep(decimal id)
        {
            step step = db.steps.Find(id);
            if (step == null)
            {
                return NotFound();
            }

            return Ok(step);
        }

        // PUT: api/stepsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstep(decimal id, step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != step.id_step)
            {
                return BadRequest();
            }

            db.Entry(step).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stepExists(id))
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

        // POST: api/stepsAPI
        [ResponseType(typeof(step))]
        public IHttpActionResult Poststep(step step)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.steps.Add(step);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = step.id_step }, step);
        }

        // DELETE: api/stepsAPI/5
        [ResponseType(typeof(step))]
        public IHttpActionResult Deletestep(decimal id)
        {
            step step = db.steps.Find(id);
            if (step == null)
            {
                return NotFound();
            }

            db.steps.Remove(step);
            db.SaveChanges();

            return Ok(step);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stepExists(decimal id)
        {
            return db.steps.Count(e => e.id_step == id) > 0;
        }
    }
}
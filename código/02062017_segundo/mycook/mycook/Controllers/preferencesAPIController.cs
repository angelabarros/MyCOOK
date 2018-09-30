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
    public class preferencesAPIController : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/preferences
        public IQueryable<preference> Getpreferences()
        {
            return db.preferences;
        }

        // GET: api/preferences/5
        [ResponseType(typeof(preference))]
        public IHttpActionResult Getpreference(decimal id)
        {
            preference preference = db.preferences.Find(id);
            if (preference == null)
            {
                return NotFound();
            }

            return Ok(preference);
        }

        // PUT: api/preferences/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpreference(decimal id, preference preference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preference.id_preference)
            {
                return BadRequest();
            }

            db.Entry(preference).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!preferenceExists(id))
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

        // POST: api/preferences
        [ResponseType(typeof(preference))]
        public IHttpActionResult Postpreference(preference preference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.preferences.Add(preference);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preference.id_preference }, preference);
        }

        // DELETE: api/preferences/5
        [ResponseType(typeof(preference))]
        public IHttpActionResult Deletepreference(decimal id)
        {
            preference preference = db.preferences.Find(id);
            if (preference == null)
            {
                return NotFound();
            }

            db.preferences.Remove(preference);
            db.SaveChanges();

            return Ok(preference);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool preferenceExists(decimal id)
        {
            return db.preferences.Count(e => e.id_preference == id) > 0;
        }
    }


}


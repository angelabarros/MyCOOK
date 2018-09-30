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
    public class userAPI2Controller : ApiController
    {
        private mycookEntities db = new mycookEntities();

        // GET: api/userAPI2
        public IQueryable<user> Getusers()
        {
            return db.users;
        }

        // GET: api/userAPI2/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Getuser(decimal id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/userAPI2/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuser(decimal id, user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.id_user)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!userExists(id))
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

        // POST: api/userAPI2
        [ResponseType(typeof(user))]
        public IHttpActionResult Postuser(user user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = user.id_user }, user);
        }

        // DELETE: api/userAPI2/5
        [ResponseType(typeof(user))]
        public IHttpActionResult Deleteuser(decimal id)
        {
            user user = db.users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool userExists(decimal id)
        {
            return db.users.Count(e => e.id_user == id) > 0;
        }
    }
}
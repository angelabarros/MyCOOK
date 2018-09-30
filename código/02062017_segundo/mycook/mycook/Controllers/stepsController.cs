using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mycook.Models;

namespace mycook.Controllers
{
    public class stepsController : Controller
    {
        private mycookEntities db = new mycookEntities();

        // GET: steps
        public ActionResult Index()
        {
            var steps = db.steps.Include(s => s.recipe);
            return View(steps.ToList());
        }

        // GET: steps/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            step step = db.steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        // GET: steps/Create
        public ActionResult Create()
        {
            ViewBag.id_recipe = new SelectList(db.recipes, "id_recipe", "name_recipe");
            return View();
        }

        // POST: steps/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_step,id_recipe,description,number_order")] step step)
        {
            if (ModelState.IsValid)
            {
                db.steps.Add(step);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_recipe = new SelectList(db.recipes, "id_recipe", "name_recipe", step.id_recipe);
            return View(step);
        }

        // GET: steps/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            step step = db.steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_recipe = new SelectList(db.recipes, "id_recipe", "name_recipe", step.id_recipe);
            return View(step);
        }

        // POST: steps/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_step,id_recipe,description,number_order")] step step)
        {
            if (ModelState.IsValid)
            {
                db.Entry(step).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_recipe = new SelectList(db.recipes, "id_recipe", "name_recipe", step.id_recipe);
            return View(step);
        }

        // GET: steps/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            step step = db.steps.Find(id);
            if (step == null)
            {
                return HttpNotFound();
            }
            return View(step);
        }

        // POST: steps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            step step = db.steps.Find(id);
            db.steps.Remove(step);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

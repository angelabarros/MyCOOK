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
    public class ingredientsController : Controller
    {
        private mycookEntities db = new mycookEntities();

        // GET: ingredients
        public ActionResult Index()
        {
            IEnumerable<ingredient> x = db.ingredients.ToList();

            List<ingredient> aux = new List<ingredient>();
            foreach (ingredient ing in x.ToList())
            {
                if (ing.status.Equals("ON"))
                {
                    aux.Add(ing);
                }
            }
            

            return View(aux);
        }

        // GET: ingredients/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingredient ingredient = db.ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: ingredients/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ingredients/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ingredient,name_ingredient")] ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.ingredients.Add(ingredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        // GET: ingredients/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingredient ingredient = db.ingredients.Find(id);
            ingredient.status = "ON";
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: ingredients/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ingredient,name_ingredient")] ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // GET: ingredients/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ingredient ingredient = db.ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ingredient ingredient = db.ingredients.Find(id);
            //db.ingredients.Remove(ingredient);
            ingredient.status = "OFF";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateINGREDIENT(mycook.Models.ingredient usermodel)
        {
            mycookEntities me = new mycookEntities();



            var ingDetails = me.ingredients.Where(x => x.name_ingredient == usermodel.name_ingredient).FirstOrDefault();

            if (ingDetails == null)
            {
                ingredient newING = new ingredient();
                newING.name_ingredient = usermodel.name_ingredient;
                newING.status = "ON";

                me.ingredients.Add(newING);
                me.SaveChanges();

                TempData["msg"] = "Record Saved Successfully.";
                // ViewBag.DataExists = true;
                // ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                return RedirectToAction("Index");

            }
            else
            {
                usermodel.ErrorMessage = "Error";
            }


            return View("Create");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult testeEdit([Bind(Include = "id_user,username,password,role, subscription")] ingredient i, mycook.Models.ingredient usermodel)
        {
            mycookEntities me = new mycookEntities();



            var ingDetails = me.ingredients.Where(x => x.id_ingredient == usermodel.id_ingredient).FirstOrDefault();

            if (ingDetails != null)
            {
                ingredient editado = ingDetails;
                editado.name_ingredient = usermodel.name_ingredient;
                editado.status = "ON";
                usermodel.status = "ON";

                i.status = "ON";

                me.SaveChanges();
                db.Entry(i).State = EntityState.Modified;
                //db.SaveChanges();
                // return RedirectToAction("Index");
                TempData["msg"] = "Record Saved Successfully.";
                // ViewBag.DataExists = true;
                // ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                return RedirectToAction("Index");

            }
            else
            {
                usermodel.ErrorMessage = "Error";
            }
            return RedirectToAction("Index");
        }
    }
}

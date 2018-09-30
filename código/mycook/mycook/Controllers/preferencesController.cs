using mycook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace mycook.Controllers
{
    public class preferencesController : Controller
    {

        mycookEntities db = new mycookEntities();
        // GET: preferences
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult Edit(mycook.Models.preference usermodel, decimal id, [Bind(Include = "id_user,username,password,role, subscription")] preference pref)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            preference u = db.preferences.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id_user = new SelectList(db., "id_recipe", "name_recipe", step.id_recipe);

            String descr = usermodel.description;
            String type = usermodel.type;

          

            preference nv = u;
            

            //u.username = username;
            //u.role = role;
            //u.password = password;
            //u.subscription = subscription;



            db.SaveChanges();

            //update_user(u, password, role, username, subscription);

            return View(u);
        }




        [HttpPost]
        public ActionResult testeEdit(preference p, mycook.Models.preference usermodel)
        {
            mycookEntities me = new mycookEntities();



            var prefDetails = me.preferences.Where(x => x.id_preference == usermodel.id_preference).FirstOrDefault();

            if (prefDetails != null)
            {
                preference editado = prefDetails;
                editado.description = usermodel.description;
                editado.type = usermodel.type;


                ;
                me.SaveChanges();
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Preferences");

            }
            else
            {
               
            }
            return RedirectToAction("Preferences");
        }


    }
}
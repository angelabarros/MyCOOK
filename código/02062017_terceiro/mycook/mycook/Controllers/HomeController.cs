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
    public class HomeController : Controller
    {
        private mycookEntities db = new mycookEntities();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Message = "teste1";

            return View();
        }

        public ActionResult Preferences(int page = 1, string sort = "description", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1)
            {
                page = 1;
            }
            int skip = (page * pageSize) - pageSize;
            var data = GetUsers(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
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
            ViewData["TYPE"] = null;

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
                editado.status = "ON";
                usermodel.status = "ON";

                if(usermodel.type.ToLower().Equals("free") || usermodel.type.ToLower().Equals("paid"))
                {

                    me.SaveChanges();
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Preferences");
                }
                else
                {
                    ViewData["TYPE"] = "Data was saved successfully.";
                    return View("Edit", usermodel);
                }

                

            }
            else
            {

            }
            return RedirectToAction("Preferences");
        }

        public List<preference> GetUsers(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (mycookEntities dc = new mycookEntities())
            {
                var v = (from u in dc.preferences
                         where

                         (u.description.Contains(search) ||
                         u.type.Contains(search)) 

                         orderby sort + " " + sortdir


                         select u);

                totalRecord = v.Count();
                List<preference> p =  v.ToList();
                //v = v.OrderBy(sort + " " + sortdir);
                
                //p = p.OrderBy(x => x.description);
                if (pageSize > 0)
                {
                  //  v = v.Skip(skip).Take(pageSize);

                }
                //return v.ToList();
                return p;
            }
        }



        // GET: apagar/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            preference p = db.preferences.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        // POST: apagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            preference p = db.preferences.Find(id);
            // db.users.Remove(user);
            p.status = "OFF";
            db.SaveChanges();
            return RedirectToAction("preferences");
        }



    }
}
using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Net;
using System.Data.Entity;

namespace mycook.Controllers
{
    public class userController : Controller
    {

        private mycookEntities db = new mycookEntities();
        // GET: user
        public ActionResult Index(int page =1, string sort = "username", string sortdir ="asc", string search="")
        {
            if(((mycook.Models.user)System.Web.HttpContext.Current.Session["user_logged"]) == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int pageSize = 10;
            int totalRecord = 0;
            if(page< 1)
            {
                page = 1;
            }
            int skip = (page * pageSize) - pageSize;
            var data = GetUsers(search, sort, sortdir, skip, pageSize,"user",out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public ActionResult teste()
        {
            
            return View();
        }

        public ActionResult Edit(mycook.Models.user usermodel, decimal id, [Bind(Include = "id_user,username,password,role, subscription")] user user)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user u = db.users.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            //ViewBag.id_user = new SelectList(db., "id_recipe", "name_recipe", step.id_recipe);

            String username = usermodel.username;

            String role = usermodel.role;

            // String pass = usermodel.

            String password = usermodel.password;

            String subscription = usermodel.subscription;

            user nv = u;

            ViewBag.role_user = new SelectList(db.users, "role", user.role);

            //u.username = username;
            //u.role = role;
            //u.password = password;
            //u.subscription = subscription;



            db.SaveChanges();

            //update_user(u, password, role, username, subscription);

            return View(u);
        }

        //public ActionResult teste(mycook.Models.user usermodel, decimal id)
        //{
        //    String x = usermodel.username;
        //    return RedirectToAction("admins","user");
        //}


        //public void update_user(user u, string pass, string role, string username, string sub)
        //{
        //    u.subscription = sub;
        //    u.username = username;
        //    u.role = role;
        //    u.password = pass;

        //    db.SaveChanges();

        //}


        // GET: apagar/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: apagar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            user user = db.users.Find(id);
            // db.users.Remove(user);
            user.status = "OFF";
            db.SaveChanges();
            return RedirectToAction("admins");
        }


        [HttpPost]
        public ActionResult teste(mycook.Models.user usermodel)
        {
            mycookEntities me = new mycookEntities();



            var userDetails = me.users.Where(x => x.username == usermodel.username).FirstOrDefault();

            if (userDetails == null)
            {
                user newAdmin = new user();
                newAdmin.username = usermodel.username;
                newAdmin.password = usermodel.password;
                newAdmin.subscription = usermodel.subscription;
                newAdmin.role = "Admin";


                me.users.Add(newAdmin);
                me.SaveChanges();

                TempData["msg"] = "Record Saved Successfully.";
                // ViewBag.DataExists = true;
                // ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                return RedirectToAction("admins");

            }
            else
            {
                usermodel.LoginErrorMessage = "Error";
            }
            return View("create");
        }


        [HttpPost]
        public ActionResult testeEdit([Bind(Include = "id_user,username,password,role, subscription")] user user, mycook.Models.user usermodel)
        {
            mycookEntities me = new mycookEntities();



            var userDetails = me.users.Where(x => x.id_user == usermodel.id_user).FirstOrDefault();

            if (userDetails != null)
            {
                user editado = userDetails;
                editado.username = usermodel.username;
                editado.password = usermodel.password;
                editado.subscription = usermodel.subscription;
                editado.role = usermodel.role;
                editado.status = "ON";
                usermodel.status = "ON";
                user.status = "ON";
                me.SaveChanges();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
               // return RedirectToAction("Index");
                TempData["msg"] = "Record Saved Successfully.";
                // ViewBag.DataExists = true;
                // ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                return RedirectToAction("admins");

            }
            else
            {
                usermodel.LoginErrorMessage = "Error";
            }
            return RedirectToAction("admins");
        }



        [HttpPost]
        public ActionResult CreateAdmin(mycook.Models.user usermodel)
        {
            mycookEntities me = new mycookEntities();

            

            var userDetails = me.users.Where(x => x.username == usermodel.username).FirstOrDefault();

            if (userDetails == null)
            {
                user newAdmin = new user();
                newAdmin.username = usermodel.username;
                newAdmin.password = usermodel.password;
                newAdmin.subscription = usermodel.subscription;
                newAdmin.role = "Admin";
                newAdmin.status = "ON";

                me.users.Add(newAdmin);
                me.SaveChanges();

                TempData["msg"] = "Record Saved Successfully.";
               // ViewBag.DataExists = true;
               // ViewBag.Javascript = "<script language='javascript' type='text/javascript'>alert('Data Already Exists');</script>";

                return RedirectToAction("admins");

            }
            else
            {
                usermodel.LoginErrorMessage = "Error";
            }
            


            /*
            mycookEntities me = new mycookEntities();

            List<user> aux = me.users.ToList();

            // userDetails = me.users.Where(x => x.username == usermodel.username && x.password == usermodel.password).FirstOrDefault();
            var userDetails = me.users.Where(x => x.username == usermodel.username).FirstOrDefault();

            if (userDetails == null)
            {

                if (usermodel.username == null && usermodel.password == null)
                {
                    usermodel.LoginErrorMessage = "Fill both fields please";
                }

                if (usermodel.username != null && usermodel.password != null)
                {
                    usermodel.LoginErrorMessage = "Wrong username and/or password";
                }


                return View("Index", usermodel);
            }
            else
            {
                return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");

            }

    */

            return View("create");
        }


        public ActionResult admins(int page = 1, string sort = "username", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1)
            {
                page = 1;
            }
            int skip = (page * pageSize) - pageSize;
            var data = GetUsers(search, sort, sortdir, skip, pageSize, "admin", out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }


        public ActionResult create()
        {
            return View();
        }



        public List<user> GetUsers(string search, string sort, string sortdir, int skip, int pageSize,string role, out int totalRecord)
        {
            using ( mycookEntities dc = new mycookEntities())
            {
                var v = (from u in dc.users
                         where
                         
                         (u.username.Contains(search) ||
                         u.subscription.Contains(search) ) &&
                         u.role.Equals(role) && u.status.Equals("ON")
                         select u );
               
                totalRecord = v.Count();
                

                if(sort.Equals("Other actions"))
                {
                    return v.ToList();
                }
             
                v = v.OrderBy(sort + " " + sortdir);

                if(pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);

                }
                return v.ToList();
            }
        }
    }
}
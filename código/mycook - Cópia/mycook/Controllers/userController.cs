using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace mycook.Controllers
{
    public class userController : Controller
    {
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

        public ActionResult create()
        {
            return View();
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

        public List<user> GetUsers(string search, string sort, string sortdir, int skip, int pageSize,string role, out int totalRecord)
        {
            using ( mycookEntities dc = new mycookEntities())
            {
                var v = (from u in dc.users
                         where
                         
                         (u.username.Contains(search) ||
                         u.subscription.Contains(search) ) &&
                         u.role.Equals(role)
                         select u );
               
                totalRecord = v.Count();
                
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
using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mycook.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(mycook.Models.user usermodel)
        {
            mycookEntities me = new mycookEntities();

            List<user> aux = me.users.ToList();
            
            var userDetails = me.users.Where(x => x.username == usermodel.username && x.password == usermodel.password).FirstOrDefault();

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
                if (userDetails.role.Equals("admin"))
                {
                    Session["user_logged"] = usermodel;

                    System.Web.HttpContext.Current.Session["user_logged"] = usermodel;
                    ViewBag.logged = usermodel.username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    return RedirectToAction("fail", "Login");


                }

            }
        }

        public ActionResult fail()
        {
            return View();
        }

        public ActionResult logout()
        {
            Session.Abandon();
            Session.Clear();
            return View();
        }

    }
}
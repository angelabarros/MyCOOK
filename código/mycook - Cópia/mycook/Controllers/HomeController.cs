using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
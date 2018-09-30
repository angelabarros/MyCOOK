using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;

namespace mycook.Controllers
{
    public class RecipesController : Controller
    {
        // GET: user
        public ActionResult Index(int page = 1, string sort = "name_recipe", string sortdir = "asc", string search = "")
        {
            if (((mycook.Models.user)System.Web.HttpContext.Current.Session["user_logged"]) == null)
            {
                return RedirectToAction("Index", "Login");
            }

            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1)
            {
                page = 1;
            }
            int skip = (page * pageSize) - pageSize;
            var data = GetRecipes(search, sort, sortdir, skip, pageSize, "user", out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }

        public ActionResult CreateRecipeStep1()
        {
            return View();
        }


        public List<recipe> GetRecipes(string search, string sort, string sortdir, int skip, int pageSize, string role, out int totalRecord)
        {
            using (mycookEntities dc = new mycookEntities())
            {
                var v = (from u in dc.recipes
                         where

                         (u.name_recipe.Contains(search) ||
                         u.portions.ToString().Contains(search)) 
                         select u);

                totalRecord = v.Count();

                v = v.OrderBy(sort + " " + sortdir);

                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);

                }
                return v.ToList();
            }
        }

        [HttpPost]
        public ActionResult Step2(mycook.Models.recipe usermode, HttpPostedFileBase file)
        {
            if (usermode.calories_portion == null || usermode.portions == null || usermode.name_recipe == null)
            {
                usermode.ErrorMessage = "Fill all fields please";
            }
            else
            {

                Session["name_of_recipe"] = usermode.name_recipe.ToString();
                Session["calories_per_portion"] = usermode.calories_portion.ToString();
                Session["portions"] = usermode.portions.ToString();

                var path = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        if (System.IO.Path.GetExtension(file.FileName).ToLower() == ".jpg" ||
                                System.IO.Path.GetExtension(file.FileName).ToLower() == ".png" ||
                            System.IO.Path.GetExtension(file.FileName).ToLower() == ".jpeg")
                        {
                            path = System.IO.Path.Combine(Server.MapPath("~/Content/Images"), file.FileName);
                            file.SaveAs(path);
                            ViewBag.UploadSuccess = true;
                        }
                    }
                }

                return View("CreateRecipeStep2");
            }
            return View("CreateRecipeStep1");
        }

        [HttpPost] 
        public ActionResult Step3(mycook.Models.recipe usermode, FormCollection fc)
        {

            mycookEntities me = new mycookEntities();


            string test = fc["txttest"];
            string test2 = fc["txttestnumber"];

            List<ingredient> ingredients = new List<ingredient>();

            //foreach(String s in fc)
            //{

            //    ingredient i = new ingredient();
            //    i.name_ingredient = fc[s];
            //    ingredients.Add(i);

            //}

            List<String> names = test.Split(',').ToList();

            List<int> quantities = test2.Split(',').Select(int.Parse).ToList();
            List<ingredients_recipe> all_ingredients = new List<ingredients_recipe>();

            int contador = 0;
            foreach(String s in names)
            {
                ingredient i = new ingredient();
                ingredients_recipe ir = new ingredients_recipe();

                i.name_ingredient = s;
                ir.ingredient = i;
                ir.quantity_per_portion = quantities[contador];
                ingredients.Add(i);
                me.ingredients.Add(i);


                all_ingredients.Add(ir);

                contador++;
            }

            me.SaveChanges();
           


            int x = ingredients.Count();

            Session["all_ingridients"] = all_ingredients;
            return View("CreateRecipeStep3");
           
        }

        [HttpPost]
        public ActionResult Step4(mycook.Models.recipe usermode, FormCollection fc)
        {

            string test = fc["txttest_step"];

            List<String> steps = test.Split(',').ToList();


            Session["steps"] = steps;


            return View("CreateRecipeStep4");
        }


        [HttpPost]
        public ActionResult Step5(mycook.Models.recipe usermode)
        {

            return View("CreateRecipeStep5");
        }


       
    }
    }

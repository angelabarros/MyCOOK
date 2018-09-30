using mycook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Web.UI.WebControls;
using System.IO;

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
                            Guid n = Guid.NewGuid();

                            path = System.IO.Path.Combine(Server.MapPath("~/Content/Images"), n.ToString());
                            file.SaveAs(path);
                            
                            Session["image_recipe"] = path;
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

            if (names.Count == 1 && names[0]=="")
            {
                ViewData["Success"] = "Data was saved successfully.";
                return View("CreateRecipeStep2");
            }
            else
            {
                List<int> quantities = test2.Split(',').Select(int.Parse).ToList();
                List<ingredients_recipe> all_ingredients = new List<ingredients_recipe>();

                int contador = 0;
                foreach (String s in names)
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


        }

        [HttpPost]
        public ActionResult Step4(mycook.Models.recipe usermode, FormCollection fc)
        {

            string test = fc["txttest_step"];

            List<String> steps = test.Split(',').ToList();
            List<step> steps_half = new List<step>();


            if (steps.Count == 1 && steps[0] == " ")
            {
                ViewData["STEPS"] = "Data was saved successfully.";
                return View("CreateRecipeStep3");
            }
            else
            {

            }

            foreach (String st in steps)
            {
                step ste = new step();
                ste.description = st;
                ste.number_order = steps.IndexOf(st) +1;
                steps_half.Add(ste);

            }


            Session["steps"] = steps_half;


            return View("CreateRecipeStep4");
        }


        [HttpPost]
        public ActionResult Step5(mycook.Models.recipe usermode)
        {

            Session["fat"] = usermode.fat_per_portion;
            Session["cholesterol"] = usermode.cholesterol_per_portion;
            Session["saturated"] = usermode.saturatedfat_per_portion;
            Session["carbohydrate"] = usermode.carbs_per_portion;
            Session["protein"] = usermode.protein_per_portion;
            Session["fibre"] = usermode.fibre_per_portion;
            Session["sodium"] = usermode.sodium_per_portion;
            Session["sugars"] = usermode.sugar_per_portion;



            return View("CreateRecipeStep5");
        }


        [HttpPost]
        public ActionResult Step6(mycook.Models.recipe usermode, FormCollection fc)
        {

            String teste =fc["txt"];

            List<String> tags_aux = teste.Split(',').ToList();

            List<String> name_tags = new List<string>();

            foreach(String s in tags_aux)
            {
                if (!s.Equals("false"))
                {
                    name_tags.Add(s);
                }
            }

            return View("CreateRecipeStep6");
        }


        [HttpPost]
        public ActionResult StepFinal(mycook.Models.recipe usermode)
        {
            mycookEntities me = new mycookEntities();

            int controlo = me.recipes.Count();




           

            recipe nova = new recipe();
            nova.name_recipe = (String) Session["name_of_recipe"];


            String aux = (String)Session["calories_per_portion"];
            int x = Int32.Parse(aux);
            nova.calories_portion = x;

            nova.portions = convert((String)Session["portions"]);

            Decimal aux2 = (Decimal)Session["fat"];
            decimal x2 = aux2;
            nova.fat_per_portion = x2;


            Decimal aux3 = (Decimal)Session["cholesterol"];
            decimal x3 = aux3;
            nova.cholesterol_per_portion = x3;



            Decimal aux4 = (Decimal)Session["saturated"];
            decimal x4 = aux4;
            nova.saturatedfat_per_portion = x4;



            Decimal aux5 = (Decimal)Session["carbohydrate"];
            decimal x5 = aux5;
            nova.carbs_per_portion = x5;


            Decimal aux6 = (Decimal)Session["protein"];
            decimal x6 = aux6;
            nova.protein_per_portion = x6;



            Decimal aux7 = (Decimal)Session["fibre"];
            decimal x7 = aux7;
            nova.fibre_per_portion =x7;



            Decimal aux8 = (Decimal)Session["sodium"];
            decimal x8 = aux8;
            nova.sodium_per_portion = x8;

            Decimal aux9 = (Decimal)Session["sugars"];
            decimal x9 = aux9;
            nova.sugar_per_portion = x9;


            me.recipes.Add(nova);

            me.SaveChanges();


            //STEPS

            List<step> recipe_steps = (List<step>) Session["steps"];

            foreach(step s in recipe_steps)
            {
                step new_step = new step();
                new_step.id_recipe = nova.id_recipe;
                new_step.recipe = nova;
                new_step.number_order = s.number_order;
                new_step.description = s.description;
                me.steps.Add(new_step);
            }

            me.SaveChanges();


            int controlo2 = me.recipes.Count();


            if (controlo2 == controlo +1)
            {
                return RedirectToAction("Index", "Recipes");
            }
            else
            {
                ViewData["WRONG"] = "Data was saved successfully.";
                return View("CreateRecipeStep6");
            }
           

            
        }


        /* NOT WORKING

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase[] files)
        {

            //Ensure model state is valid  
            if (ModelState.IsValid)
            {   //iterating through multiple file collection   
                foreach (HttpPostedFileBase file in files)
                {
                    //Checking file is available to save.  
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = files.Count().ToString() + " files uploaded successfully.";
                    }

                }
            }
            return View();
        }
        */


        private int convert(String s)
        {
           
            int x = Int32.Parse(s);

            return x;
        }
    }
    }

using PersonProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonProject.Controllers
{
    public class MaritalStatusController : Controller
    {

        private PersonListEntities db = new PersonListEntities();
        // GET: MaritalStatus
        public ActionResult Mer(int? id)
        {

            MainViewModel model = new MainViewModel();

           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           model.person  = db.Persons.Find(id);
            //Person person = db.Persons.Find(id);
            if (model.person == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Mer", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Mer([Bind(Include = "person_id,spouse_id, first_name")] Person person, FormCollection fc, int? id, MainViewModel model)
        {

            //SpouseViewModel model = new SpouseViewModel();
            int? sp_id = model.spouseModel.spouse_id;
            int? p_id = person.person_id;



            if (sp_id == p_id)
            {
                ViewBag.Message = "can not marry yourself!";
                return PartialView("_Mer");
                }
                

                if (ModelState.IsValid)
            {
                Person per = db.Persons.Find(p_id);
                if (per == null)
                {
                    return HttpNotFound();
                }


                Person spouseNew = db.Persons.Find(sp_id);
                if (spouseNew.single_married == null)
                {
                    per.Spouse = spouseNew;
                    per.single_married = "Married";
                }
                else
                {

                    ViewBag.Message = "Person is married!";
                    return PartialView("_Mer");
                }

                per.Spouse = spouseNew;
                per.single_married = "Married";
                spouseNew.Spouse = per;
                spouseNew.single_married = "Married";

                db.Entry(per).State = EntityState.Modified;
                db.SaveChanges();


                return View("Person","Index");
            }
           
            return PartialView("_Mer", model);
        }
    }
}
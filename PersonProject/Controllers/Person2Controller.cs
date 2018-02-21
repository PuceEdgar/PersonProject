using PersonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonProject.Controllers
{
    public class Person2Controller : Controller
    {
        //izveidošana
        public ActionResult Index()
        {
            return View();
        }

        //saglabāšana
        [HttpPost]
        public ActionResult Index(PersonModel2 model)
        {
            return View(model);
        }

        //skatīšanās
        public ActionResult View(long personId)
        {
            var db = new PersonListEntities();
            var person = db.Persons.Find(personId);
            var model = new PersonModel2(person);
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonProject.Models;

namespace PersonProject.Controllers
{
    public class PersonController : Controller
    {
        private PersonListEntities db = new PersonListEntities();
        

        // GET: Person
        public ActionResult Index()
        {
            MainViewModel model = new MainViewModel();
           // List<PersonModel> pl = db.Persons.ToList();
            //List<Person> personList = db.Persons.Select(x => new Person {  first_name = x.first_name, last_name = x.last_name, dob = x.dob, phone_number = x.phone_number, primary_address = x.primary_address, single_married = x.single_married, spouse_id = x.spouse_id, person_id = x.person_id   }).ToList();
            model.iePerson = db.Persons.ToList();
            //var optionList = db.Persons.ToList();
            //SelectList list = new SelectList(optionList, "single_married");
            //ViewBag.options = list;

            return View(model);
        }

        public ActionResult Test()
        {
            return View(db.Persons.ToList());
        }


       

  


       /* [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            
            List<Person> persons;
            if (string.IsNullOrEmpty(searchTerm))
            {
                persons = db.Persons.ToList();
            }
            else
            {
                persons = db.Persons
                    .Where(s => s.first_name.StartsWith(searchTerm)).ToList();
            }
            return View(persons);
        }*/

             
        //get auto complete list
        public JsonResult GetListForAutocomplete(string term)
        {
            Person[] matching = string.IsNullOrWhiteSpace(term) ?
                db.Persons.ToArray() :
                db.Persons.Where(p => p.first_name.ToUpper().StartsWith(term.ToUpper()) & p.single_married == null).ToArray();

            return Json(matching.Select(m => new {
                id = m.person_id,
                value = m.first_name,
                label = m.first_name + " " + m.last_name + " " + "(" + m.age + ")"
            }), JsonRequestBehavior.AllowGet);
        }


        
        // GET: Person/Details/5
        //public ActionResult Details(int? id)
        //{
        //    //MainViewModel model = new MainViewModel();
        //    //model.person = db.Persons.Find(id);
        //    Person person = db.Persons.Find(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Person person = db.Persons.Find(id);
        //    if (person == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(person);
        //}

        // GET: Person/Create
        public ActionResult Create()
        {
            return PartialView("_AddPerson");
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "person_id,first_name,last_name,dob,single_married,spouse_id,primary_address,phone_number,address_id, address, primary_address, record_id")] Person person, MainViewModel mainViewModel, FormCollection fc)
        {
            MainViewModel model = new MainViewModel();
            // ViewData["status"] = "create started!";
             List<Address> al = mainViewModel.allAddresses;

           // PersonModel personModel = model;
            //List<Address> al = model.allAddresses;
            

            string selectedRadio = fc["primary"];
            int radio = Convert.ToInt32(selectedRadio);
            //person.primary_address = al[radio].address1;

            //Person newPerson = new Person();
            //newPerson.first_name = model.first_name;
            //newPerson.last_name = model.last_name;
            //newPerson.dob = model.dob;
            //newPerson.phone_number = model.phone_number;



            Person newPerson = new Person();
            newPerson.first_name = fc["personModel.first_name"];
            newPerson.last_name = fc["personModel.last_name"];
            newPerson.dob = Convert.ToDateTime(fc["personModel.dob"]);
            newPerson.phone_number = fc["personModel.phone_number"];
            newPerson.primary_address = al[radio].address1;
            if (newPerson.primary_address == null)
            {
                ViewBag.Message = "Please select primary address!";
                return PartialView("_AddPerson");
            }
            


            if (ModelState.IsValid)
            {
                db.Persons.Add(newPerson);
                
                foreach (var a in al)
                {
                    
                    db.Addresses.Add(a);
                    
                }

                for (int i = 0; i < al.Count(); i++)
                {
                    var personAddress = new Person_Address();
                   personAddress.Person = newPerson;
                    personAddress.Address = al[i];
                    db.Person_Address.Add(personAddress);
                }


                db.SaveChanges();
                ModelState.Clear();

                
                return JavaScript("success()");
            }
            //change to person if not working!
            return View(model);
        }

        // GET: Person/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    MainViewModel model = new MainViewModel();
        //    model.person = db.Persons.SingleOrDefault(p => p.person_id == id);

        //    Person person = db.Persons.Find(id);
        //    if (person == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "person_id,spouse_id, first_name")] Person person, int id, FormCollection fc)
        //{

        //    MainViewModel model = new MainViewModel();
        //    model.person = person;
        //    string sp_name = fc["Spouse.first_name"];
        //    int? sp_id = Convert.ToInt32(fc["person.spouse_id"]);
        //    int p_id = id;

        //    if (sp_id == p_id)
        //    {
        //        ViewData["error"] = "You can not marry yourself!";
        //        return View(model);
        //    }

        //   // ViewData["test"] = sp_name + " " + p_id + " " + sp_id;
            
        //    if (ModelState.IsValid)
        //    {
        //        Person per = db.Persons.Find(p_id);
        //        if (per == null)
        //        {
        //            return HttpNotFound();
        //        }

              
        //        Person spouseNew = db.Persons.Find(sp_id);
        //        if ( spouseNew.single_married == null)
        //        {
        //            per.Spouse = spouseNew;
        //            per.single_married = "Married";
        //        } else
        //        {
        //            ViewData["error"] = "Sorry, this person is already married!";
        //            return View(model);
        //        }
                

        //        spouseNew.Spouse = per;
        //        spouseNew.single_married = "Married";
                                  
        //            db.Entry(per).State = EntityState.Modified;
        //            db.SaveChanges();
                                         
               
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
            

        //}

        // GET: Person/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Person person = db.Persons.Find(id);
        //    if (person == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(person);
        //}

        // POST: Person/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed([Bind(Include = "spouse_id, single_married")] int id)
        //{
        //    Person person = db.Persons.Find(id);

        //    //Person_Address remPerson = db.Person_Address.Find(person.person_id);

        //   /* foreach (var per in db.Person_Address)
        //    {
        //        if (person.person_id == per.person_id)
        //        {
        //            db.Person_Address.Remove(per);
        //        }
        //    }*/

            

        //    //Person newSingle = person.Spouse;
        //    //newSingle.spouse_id = null;
        //    //newSingle.single_married = null;
        //    //db.Entry(newSingle).State = EntityState.Modified;
        //    db.Persons.Remove(person);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult Single(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }

            return View();
        }


        [HttpPost]
        
        public ActionResult Single([Bind(Include = "single_married")] Person person, int? id)
        {

            MainViewModel model = new MainViewModel();
           
            model.person = db.Persons.Find(id);

            //Person singlePerson = db.Persons.Find(person.person_id);

            model.person.single_married = "Single";

            if (ModelState.IsValid)
            {
                db.Entry(model.person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

           
        }

        //// GET: Person/Details/5
        //public ActionResult Married(int? id)
        //{
        //    SpouseViewModel model = new SpouseViewModel();

        //   // MainViewModel model = new MainViewModel();
        //    model.person = db.Persons.Find(id);
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    //Person person = db.Persons.Find(id);
        //    if (model.person == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(model);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Married([Bind(Include = "person_id,spouse_id, first_name")] Person person, int id, FormCollection fc, SpouseViewModel smodel)
        //{
        //    SpouseViewModel model = new SpouseViewModel();
        //    int sp_id = smodel.spouse_id;
        //    int p_id = id;
        //    //MainViewModel model = new MainViewModel();
        //    //model.person = person;
        //    //string sp_name = fc["Spouse.first_name"];
        //    //int? sp_id = Convert.ToInt32(fc["person.spouse_id"]);
        //    //int p_id = id;
        //    //Person spouseNew = db.Persons.Find(sp_id);
            
        //    if (ModelState.IsValid)
        //    {
        //        Person per = db.Persons.Find(p_id);
        //        if (per == null)
        //        {
        //            return HttpNotFound();
        //        }


        //        Person spouseNew = db.Persons.Find(sp_id);
        //        if (spouseNew.single_married == null)
        //        {
        //            per.Spouse = spouseNew;
        //            per.single_married = "Married";
        //        }
        //        else
        //        {

        //            string error = "Sorry, this person is already married!";
        //            //ViewData["error"] = "Sorry, this person is already married!";
        //            return PartialView(error, model);
        //        }

        //        per.Spouse = spouseNew;
        //        per.single_married = "Married";
        //        spouseNew.Spouse = per;
        //        spouseNew.single_married = "Married";

        //        db.Entry(per).State = EntityState.Modified;
        //        db.SaveChanges();


        //        return RedirectToAction("Index");
        //    }
        //    return View(model);


        //}

        public ActionResult Mer(int? id)
        {

            MainViewModel model = new MainViewModel();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            model.person = db.Persons.Find(id);
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

            if (sp_id == null)
            {
                ViewBag.Message = "Sorry, selected person doesn't exist!";

                return PartialView("_Mer");
            }

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

                
                return JavaScript("success()");
                
            }

            return PartialView("_Mer", model);
        }

        public ActionResult Add()
        {
            return PartialView("_AddPerson");
        }

        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "person_id,first_name,last_name,dob,single_married,spouse_id,primary_address,phone_number,phone_number_2,address_id, address, primary_address, record_id")] Person person, MainViewModel mainViewModel, FormCollection fc)
        {
            //MainViewModel model = new MainViewModel();
            
            List<Address> al = mainViewModel.allAddresses;

             PersonModel personModel = mainViewModel.personModel;
            //List<Address> al = model.allAddresses;


            string selectedRadio = fc["primary"];
            int radio = Convert.ToInt32(selectedRadio);
            DateTime now = DateTime.Today;

            Person newPerson = new Person();
            newPerson.first_name = personModel.first_name;
            newPerson.last_name = personModel.last_name;
            newPerson.dob = personModel.dob;

            int result = DateTime.Compare(now, newPerson.dob);

            if (result < 0)
            {
                ViewBag.Message = "Please select proper date!";
                return PartialView("_AddPerson", mainViewModel);
            }


            newPerson.phone_number = personModel.phone_number;
            newPerson.phone_number_2 = personModel.phone_number_2;



            //Person newPerson = new Person();
            //newPerson.first_name = fc["personModel.first_name"];
            //newPerson.last_name = fc["personModel.last_name"];
            //newPerson.dob = Convert.ToDateTime(fc["personModel.dob"]);
            //newPerson.phone_number = fc["personModel.phone_number"];
            newPerson.primary_address = al[radio].address1;
            if (newPerson.primary_address == null)
            {
                ViewBag.Message = "Please select primary address!";
                return PartialView("_AddPerson", mainViewModel);
            }



            if (ModelState.IsValid)
            {
                db.Persons.Add(newPerson);

                foreach (var a in al)
                {
                    if (a.address1 != null)
                    {
                        db.Addresses.Add(a);
                    }
                    

                }

                for (int i = 0; i < al.Count(); i++)
                {
                    
                    if (al[i].address1 != null)
                    {
                        var personAddress = new Person_Address();
                        personAddress.Person = newPerson;
                        personAddress.Address = al[i];
                        db.Person_Address.Add(personAddress);
                    }
                   
                }


                db.SaveChanges();
                ModelState.Clear();


                return JavaScript("success()");
            }
            //change to person if not working!
            return PartialView("_AddPerson", mainViewModel);
        }

    }
}

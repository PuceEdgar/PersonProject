using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonProject.Models;

namespace PersonProject.Controllers
{
    public class Person_AddressController : Controller
    {
        private PersonListEntities db = new PersonListEntities();

        // GET: Person_Address
        public ActionResult Index()
        {
            var person_Address = db.Person_Address.Include(p => p.Address).Include(p => p.Person);
            return View(person_Address.ToList());
        }

        // GET: Person_Address/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person_Address person_Address = db.Person_Address.Find(id);
            if (person_Address == null)
            {
                return HttpNotFound();
            }
            return View(person_Address);
        }

        // GET: Person_Address/Create
        public ActionResult Create()
        {
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "address1");
            ViewBag.person_id = new SelectList(db.Persons, "person_id", "first_name");
            return View();
        }

        // POST: Person_Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "person_id,address_id,record_id")] Person_Address person_Address)
        {
            if (ModelState.IsValid)
            {
                db.Person_Address.Add(person_Address);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "address1", person_Address.address_id);
            ViewBag.person_id = new SelectList(db.Persons, "person_id", "first_name", person_Address.person_id);
            return View(person_Address);
        }

        // GET: Person_Address/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person_Address person_Address = db.Person_Address.Find(id);
            if (person_Address == null)
            {
                return HttpNotFound();
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "address1", person_Address.address_id);
            ViewBag.person_id = new SelectList(db.Persons, "person_id", "first_name", person_Address.person_id);
            return View(person_Address);
        }

        // POST: Person_Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "person_id,address_id,record_id")] Person_Address person_Address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person_Address).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.address_id = new SelectList(db.Addresses, "address_id", "address1", person_Address.address_id);
            ViewBag.person_id = new SelectList(db.Persons, "person_id", "first_name", person_Address.person_id);
            return View(person_Address);
        }

        // GET: Person_Address/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person_Address person_Address = db.Person_Address.Find(id);
            if (person_Address == null)
            {
                return HttpNotFound();
            }
            return View(person_Address);
        }

        // POST: Person_Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person_Address person_Address = db.Person_Address.Find(id);
            db.Person_Address.Remove(person_Address);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

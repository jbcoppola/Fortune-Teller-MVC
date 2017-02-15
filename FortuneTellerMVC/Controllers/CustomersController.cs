using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerEntities db = new FortuneTellerEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
			
			//retirement logic
			ViewBag.Retire = 0;
			
			if (customer.Age % 2 == 0)
			{
				ViewBag.Retire = 40;
			}
			else
			{
				ViewBag.Retire = 30;
			}

			//vacation home logic
			ViewBag.VacationHome = "";

			if (customer.NumberOfSiblings == 0)
			{
				ViewBag.VacationHome = "Las Vegas";
			}
			else if (customer.NumberOfSiblings == 1)
			{
				ViewBag.VacationHome = "Cancun";
			}
			else if (customer.NumberOfSiblings == 2)
			{
				ViewBag.VacationHome = "Australia";
			}
			else if (customer.NumberOfSiblings == 3)
			{
				ViewBag.VacationHome = "France";
			}
			else if (customer.NumberOfSiblings > 3)
			{
				ViewBag.VacationHome = "Jamaica";
			}
			else
			{
				ViewBag.VacationHome = "Antarctica";
			}

			//bank cash logic
			ViewBag.BankCash = 0;
			if (customer.BirthMonth > 0 && customer.BirthMonth < 13)
			{
				if (customer.BirthMonth > 8)
				{
					ViewBag.BankCash = 100000.00;
				}
				else if (customer.BirthMonth > 4)
				{
					ViewBag.BankCash = 80000.00;
				}
				else
				{
					ViewBag.BankCash = 60000.00;
				}
			}
			else
			{
				ViewBag.BankCash = 0.00;
			}

			//vehicle logic
			switch (customer.FavoriteColor.ToLower())
			{
				case "red":
					ViewBag.Vehicle = "WW2 tank with live ammunition";
					break;
				case "orange":
					ViewBag.Vehicle = "steam train retrofitted with car tires";
					break;
				case "yellow":
					ViewBag.Vehicle = "very small and improprietously painted submarine";
					break;
				case "green":
					ViewBag.Vehicle = "blimp emblazoned with logo for the short-lived 1939 comedy troupe \"Axison Allies\"";
					break;
				case "blue":
					ViewBag.Vehicle = "pirate ship loaded with stolen Spanish silver. Avast";
					break;
				case "indigo":
					ViewBag.Vehicle = "featureless chrome orb that hovers ominiously over the road";
					break;
				case "violet":
					ViewBag.Vehicle = "purple Mini Cooper with wood siding";
					break;
				default:
					ViewBag.Vehicle = "squeaky shopping cart";
					break;
			}

			return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,customer.BirthMonth,FavoriteColor,NumberOfcustomer.NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,customer.BirthMonth,FavoriteColor,NumberOfcustomer.NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

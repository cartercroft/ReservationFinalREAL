using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationFinal.DATA.EF;
using Microsoft.AspNet.Identity;

namespace ReservationFinal.MVC.UI.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private ReservationFinalEntities db = new ReservationFinalEntities();
        // GET: Reservations
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var assets = db.OwnerInstruments.Where(i => i.OwnerID == currentUser && i.IsActive).Count();

            if (Request.IsAuthenticated && (User.IsInRole("Employee") || User.IsInRole("Admin"))) //If user is admin or employee, show all reservations
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments, "OwnerInstrumentID", "InstrumentName");
                var reservations = db.Reservations.Include(r => r.Location).Include(r => r.OwnerInstrument);
               return View(reservations.ToList());
            }

            else if (assets != 0) //If user is not a Employee/Admin AND they have active assets, show only their reservations
            {
                ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
                ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments.Where(o => o.OwnerID == currentUser), "OwnerInstrumentID", "InstrumentName");
                var reservations = db.Reservations.Where(u => u.OwnerInstrument.OwnerID == currentUser);
                ViewBag.InstrumentsCount = db.OwnerInstruments.Where(i => i.IsActive && i.OwnerID == currentUser);
                return View(reservations.ToList());
            }

            else //The user has no active assets, send them to the Create Asset View.
            {
                return RedirectToAction("Create", "OwnerInstruments");
            }
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        #region AJAX Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.AsNoTracking().Where(l => l.LocationID == reservation.LocationID).FirstOrDefault();

                if (location.Reservations.Where(r => r.ReservationDate == reservation.ReservationDate).ToList().Count >= location.ReservationLimit && !User.IsInRole("Admin"))
                {
                    throw new Exception();
                }
                else
                {
                    db.Reservations.Add(reservation);
                    db.SaveChanges();
                }
            }

            return Json(new {
            Location = reservation.LocationID,
            Instrument = reservation.OwnerInstrumentID,
            Date = reservation.ReservationDate
            });
        }
        #endregion
        // GET: Reservations/Create
        public ActionResult Create()
        {
            var currentUser = User.Identity.GetUserId();
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName");
            ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments.Where(o => o.OwnerID == currentUser), "OwnerInstrumentID", "InstrumentName");
            return View();
        }

        public ActionResult MaxCapacity()
        {
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,OwnerInstrumentID,LocationID,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.Where(l => l.LocationID == reservation.LocationID).FirstOrDefault();

                if (location.Reservations.Where(r => r.ReservationDate == reservation.ReservationDate).ToList().Count >= location.ReservationLimit && !User.IsInRole("Admin"))
                {
                       return RedirectToAction("MaxCapacity");
                }

                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments, "OwnerInstrumentID", "InstrumentName", reservation.OwnerInstrumentID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            var currentUser = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments.Where(model => model.OwnerID == currentUser), "OwnerInstrumentID", "InstrumentName", reservation.OwnerInstrumentID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,OwnerInstrumentID,LocationID,ReservationDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var location = db.Locations.AsNoTracking().Where(l => l.LocationID == reservation.LocationID).FirstOrDefault();
                //Added AsNoTracking() to fix error

                if (location.Reservations.Where(r => r.ReservationDate == reservation.ReservationDate).ToList().Count >= location.ReservationLimit && !User.IsInRole("Admin"))
                {
                    return RedirectToAction("MaxCapacity");
                }
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "LocationName", reservation.LocationID);
            ViewBag.OwnerInstrumentID = new SelectList(db.OwnerInstruments, "OwnerInstrumentID", "InstrumentName", reservation.OwnerInstrumentID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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

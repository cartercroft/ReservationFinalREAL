using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReservationFinal.DATA.EF;

namespace ReservationFinal.MVC.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class InstrumentTypesController : Controller
    {
        private ReservationFinalEntities db = new ReservationFinalEntities();

        // GET: InstrumentTypes
        public ActionResult Index()
        {
            return View(db.InstrumentTypes.ToList());
        }

        // GET: InstrumentTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentType instrumentType = db.InstrumentTypes.Find(id);
            if (instrumentType == null)
            {
                return HttpNotFound();
            }
            return View(instrumentType);
        }

        #region AJAX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxCreate(InstrumentType type)
        {
            db.InstrumentTypes.Add(type);
            db.SaveChanges();
            return Json(type);
        }
        #endregion

        // GET: InstrumentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstrumentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstrumentTypeID,InstrumentTypeName")] InstrumentType instrumentType)
        {
            if (ModelState.IsValid)
            {
                db.InstrumentTypes.Add(instrumentType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instrumentType);
        }

        // GET: InstrumentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentType instrumentType = db.InstrumentTypes.Find(id);
            if (instrumentType == null)
            {
                return HttpNotFound();
            }
            return View(instrumentType);
        }

        // POST: InstrumentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstrumentTypeID,InstrumentTypeName")] InstrumentType instrumentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrumentType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instrumentType);
        }

        // GET: InstrumentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentType instrumentType = db.InstrumentTypes.Find(id);
            if (instrumentType == null)
            {
                return HttpNotFound();
            }
            return View(instrumentType);
        }

        // POST: InstrumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstrumentType instrumentType = db.InstrumentTypes.Find(id);
            db.InstrumentTypes.Remove(instrumentType);
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

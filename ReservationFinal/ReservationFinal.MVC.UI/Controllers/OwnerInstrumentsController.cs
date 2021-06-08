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
using System.Drawing; // Allows us to work with key functionality for image files
using ReservationFinal.MVC.UI.Utilities;

namespace ReservationFinal.MVC.UI.Controllers
{
    [Authorize]
    public class OwnerInstrumentsController : Controller
    {
        private ReservationFinalEntities db = new ReservationFinalEntities();

        // GET: OwnerInstruments
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var ownerInstruments = db.OwnerInstruments.Where(o => o.OwnerID == currentUser).Include(o => o.InstrumentType).Include(o => o.UserDetail);
            return View(ownerInstruments.ToList());
        }

        // GET: OwnerInstruments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerInstrument ownerInstrument = db.OwnerInstruments.Find(id);
            if (ownerInstrument == null)
            {
                return HttpNotFound();
            }
            return View(ownerInstrument);
        }

        // GET: OwnerInstruments/Create
        public ActionResult Create()
        {
            ViewBag.InstrumentTypeID = new SelectList(db.InstrumentTypes, "InstrumentTypeID", "InstrumentTypeName");
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName");
            return View();
        }

        // POST: OwnerInstruments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerInstrumentID,InstrumentName,OwnerID,InstrumentPhoto,SpecialNotes,IsActive,DateAdded,InstrumentTypeID")] OwnerInstrument ownerInstrument, HttpPostedFileBase instrumentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (instrumentPhoto != null)
                {
                    file = instrumentPhoto.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && instrumentPhoto.ContentLength <= 4194304)
                    {

                        // Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image                        // This informs the program to save the image to this location in our file structure.
                        string savePath = Server.MapPath("~/Content/instImages/");

                        Image convertedImage = Image.FromStream(instrumentPhoto.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }
                }
                // No matter what, update the photoUrl with the value of the file variable
                ownerInstrument.InstrumentPhoto = file;
                #endregion
                db.OwnerInstruments.Add(ownerInstrument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstrumentTypeID = new SelectList(db.InstrumentTypes, "InstrumentTypeID", "InstrumentTypeName", ownerInstrument.InstrumentTypeID);
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerInstrument.OwnerID);
            return View(ownerInstrument);
        }
        // GET: OwnerInstruments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerInstrument ownerInstrument = db.OwnerInstruments.Find(id);
            if (ownerInstrument == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstrumentTypeID = new SelectList(db.InstrumentTypes, "InstrumentTypeID", "InstrumentTypeName", ownerInstrument.InstrumentTypeID);
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerInstrument.OwnerID);
            return View(ownerInstrument);
        }

        // POST: OwnerInstruments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerInstrumentID,InstrumentName,OwnerID,InstrumentPhoto,SpecialNotes,IsActive,DateAdded,InstrumentTypeID")] OwnerInstrument ownerInstrument, HttpPostedFileBase instrumentPhoto)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (instrumentPhoto != null)
                {
                    file = instrumentPhoto.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && instrumentPhoto.ContentLength <= 4194304)
                    {

                        // Create a new file name (using a GUID)
                        file = Guid.NewGuid() + ext;

                        #region Resize Image                        // This informs the program to save the image to this location in our file structure.
                        string savePath = Server.MapPath("~/Content/instImages/");

                        Image convertedImage = Image.FromStream(instrumentPhoto.InputStream);

                        int maxImageSize = 500;

                        int maxThumbSize = 100;

                        ImageServices.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }
                }
                // No matter what, update the photoUrl with the value of the file variable
                ownerInstrument.InstrumentPhoto = file;
                #endregion
                db.Entry(ownerInstrument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstrumentTypeID = new SelectList(db.InstrumentTypes, "InstrumentTypeID", "InstrumentTypeName", ownerInstrument.InstrumentTypeID);
            ViewBag.OwnerID = new SelectList(db.UserDetails, "UserID", "FirstName", ownerInstrument.OwnerID);
            return View(ownerInstrument);
        }

        // GET: OwnerInstruments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerInstrument ownerInstrument = db.OwnerInstruments.Find(id);
            if (ownerInstrument == null)
            {
                return HttpNotFound();
            }
            return View(ownerInstrument);
        }

        // POST: OwnerInstruments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerInstrument ownerInstrument = db.OwnerInstruments.Find(id);
            if (ownerInstrument.IsActive)
            {
                ownerInstrument.IsActive = false;
            }
            else
            {
                ownerInstrument.IsActive = true;
            }

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

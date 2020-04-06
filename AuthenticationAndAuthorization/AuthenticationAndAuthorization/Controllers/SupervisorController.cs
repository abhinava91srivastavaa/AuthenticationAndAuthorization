using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AuthenticationAndAuthorization;

namespace AuthenticationAndAuthorization.Controllers
{
    public class SupervisorController : Controller
    {
        private AuthandAuthorizationEntities db = new AuthandAuthorizationEntities();

        // GET: Supervisor
        public ActionResult Index()
        {
            return View(db.UserprofileandRegs.Where(x => x.UserType!=1).ToList());
        }

        // GET: Supervisor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            if (userprofileandReg == null)
            {
                return HttpNotFound();
            }
            return View(userprofileandReg);
        }

        // GET: Supervisor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supervisor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,EmailId,Password,CommunicationAddress,Status")] UserprofileandReg userprofileandReg)
        {
            if (ModelState.IsValid)
            {
                db.UserprofileandRegs.Add(userprofileandReg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userprofileandReg);
        }

        // GET: Supervisor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            if (userprofileandReg == null)
            {
                return HttpNotFound();
            }
            return View(userprofileandReg);
        }

        // POST: Supervisor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,EmailId,Password,CommunicationAddress,Status")] UserprofileandReg userprofileandReg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userprofileandReg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userprofileandReg);
        }

        // GET: Supervisor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            if (userprofileandReg == null)
            {
                return HttpNotFound();
            }
            return View(userprofileandReg);
        }

        // POST: Supervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserprofileandReg userprofileandReg = db.UserprofileandRegs.Find(id);
            db.UserprofileandRegs.Remove(userprofileandReg);
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

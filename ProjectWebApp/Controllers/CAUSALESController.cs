using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LexAbogadosWeb.Models;

namespace LexAbogadosWeb.Controllers
{
    [Authorize]
    public class CAUSALESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: CAUSALES
        public ActionResult Index()
        {
            List<CAUSALES> causales = new List<CAUSALES>();
            try
            {
                causales = db.CAUSALES.ToList();
                return View(causales);

            }
            catch (Exception)
            {

                return View(causales);
            }
        }

        // GET: CAUSALES/Details/5
        public ActionResult Details(long? id)
        {
            CAUSALES cAUSALES = new CAUSALES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                cAUSALES = db.CAUSALES.Find(id);
                if (cAUSALES == null)
                {
                    return HttpNotFound();
                }
                return View(cAUSALES);
            }
            catch (Exception)
            {

                return View(cAUSALES);
            }

        }

        // GET: CAUSALES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CAUSALES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CAUSAL,NOMBRE")] CAUSALES cAUSALES)
        {
            if (ModelState.IsValid)
            {
                db.CAUSALES.Add(cAUSALES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAUSALES);
        }

        // GET: CAUSALES/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAUSALES cAUSALES = db.CAUSALES.Find(id);
            if (cAUSALES == null)
            {
                return HttpNotFound();
            }
            return View(cAUSALES);
        }

        // POST: CAUSALES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CAUSAL,NOMBRE")] CAUSALES cAUSALES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAUSALES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAUSALES);
        }

        // GET: CAUSALES/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAUSALES cAUSALES = db.CAUSALES.Find(id);
            if (cAUSALES == null)
            {
                return HttpNotFound();
            }
            return View(cAUSALES);
        }

        // POST: CAUSALES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CAUSALES cAUSALES = db.CAUSALES.Find(id);
            db.CAUSALES.Remove(cAUSALES);
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

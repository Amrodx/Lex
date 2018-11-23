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
    public class COMUNASController : Controller
    {
        private ODAO db = new ODAO();

        // GET: COMUNAS
        public ActionResult Index()
        {
            List<COMUNAS> cOMUNAS = new List<COMUNAS>();
            try
            {
                cOMUNAS = db.COMUNAS.Include(c => c.REGIONES).ToList();
                return View(cOMUNAS);
            }
            catch (Exception)
            {
                return View(cOMUNAS);
            }
        }

        // GET: COMUNAS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMUNAS cOMUNAS = db.COMUNAS.Find(id);
            if (cOMUNAS == null)
            {
                return HttpNotFound();
            }
            return View(cOMUNAS);
        }

        // GET: COMUNAS/Create
        public ActionResult Create()
        {
            ViewBag.ID_REGION = new SelectList(db.REGIONES, "ID_REGION", "NOMBRE");
            return View();
        }

        // POST: COMUNAS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_COMUNA,COMUNA,ID_REGION")] COMUNAS cOMUNAS)
        {
            if (ModelState.IsValid)
            {
                db.COMUNAS.Add(cOMUNAS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_REGION = new SelectList(db.REGIONES, "ID_REGION", "NOMBRE", cOMUNAS.ID_REGION);
            return View(cOMUNAS);
        }

        // GET: COMUNAS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMUNAS cOMUNAS = db.COMUNAS.Find(id);
            if (cOMUNAS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_REGION = new SelectList(db.REGIONES, "ID_REGION", "NOMBRE", cOMUNAS.ID_REGION);
            return View(cOMUNAS);
        }

        // POST: COMUNAS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_COMUNA,COMUNA,ID_REGION")] COMUNAS cOMUNAS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cOMUNAS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_REGION = new SelectList(db.REGIONES, "ID_REGION", "NOMBRE", cOMUNAS.ID_REGION);
            return View(cOMUNAS);
        }

        // GET: COMUNAS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COMUNAS cOMUNAS = db.COMUNAS.Find(id);
            if (cOMUNAS == null)
            {
                return HttpNotFound();
            }
            return View(cOMUNAS);
        }

        // POST: COMUNAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            COMUNAS cOMUNAS = db.COMUNAS.Find(id);
            db.COMUNAS.Remove(cOMUNAS);
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

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
    public class REGIONESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: REGIONES
        public ActionResult Index()
        {
            List<REGIONES> rEGIONES = new List<REGIONES>();
            try
            {
                rEGIONES = db.REGIONES.ToList();
                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }
        }

        // GET: REGIONES/Details/5
        public ActionResult Details(long? id)
        {
            REGIONES rEGIONES = new REGIONES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                rEGIONES = db.REGIONES.Find(id);
                if (rEGIONES == null)
                {
                    return HttpNotFound();
                }
                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }

        }

        // GET: REGIONES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: REGIONES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_REGION,NOMBRE")] REGIONES rEGIONES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.REGIONES.Add(rEGIONES);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }

        }

        // GET: REGIONES/Edit/5
        public ActionResult Edit(long? id)
        {
            REGIONES rEGIONES = new REGIONES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                rEGIONES = db.REGIONES.Find(id);
                if (rEGIONES == null)
                {
                    return HttpNotFound();
                }
                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }

        }

        // POST: REGIONES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_REGION,NOMBRE")] REGIONES rEGIONES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rEGIONES).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }

        }

        // GET: REGIONES/Delete/5
        public ActionResult Delete(long? id)
        {
            REGIONES rEGIONES = new REGIONES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                rEGIONES = db.REGIONES.Find(id);
                if (rEGIONES == null)
                {
                    return HttpNotFound();
                }
                return View(rEGIONES);
            }
            catch (Exception)
            {

                return View(rEGIONES);
            }

        }

        // POST: REGIONES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            REGIONES rEGIONES = new REGIONES();
            try
            {
                if (id > 0)
                {
                    rEGIONES = db.REGIONES.Find(id);
                    db.REGIONES.Remove(rEGIONES);
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                return RedirectToAction("Index");
            }

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

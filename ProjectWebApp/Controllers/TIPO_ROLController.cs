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
    public class TIPO_ROLController : Controller
    {
        private ODAO db = new ODAO();

        // GET: TIPO_ROL
        public ActionResult Index()
        {
            return View(db.TIPO_ROL.ToList());
        }

        // GET: TIPO_ROL/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_ROL tIPO_ROL = db.TIPO_ROL.Find(id);
            if (tIPO_ROL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_ROL);
        }

        // GET: TIPO_ROL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPO_ROL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ROL,NOMBRE_ROL,CATEGORIA")] TIPO_ROL tIPO_ROL)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_ROL.Add(tIPO_ROL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPO_ROL);
        }

        // GET: TIPO_ROL/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_ROL tIPO_ROL = db.TIPO_ROL.Find(id);
            if (tIPO_ROL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_ROL);
        }

        // POST: TIPO_ROL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ROL,NOMBRE_ROL,CATEGORIA")] TIPO_ROL tIPO_ROL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_ROL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPO_ROL);
        }

        // GET: TIPO_ROL/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_ROL tIPO_ROL = db.TIPO_ROL.Find(id);
            if (tIPO_ROL == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_ROL);
        }

        // POST: TIPO_ROL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TIPO_ROL tIPO_ROL = db.TIPO_ROL.Find(id);
            db.TIPO_ROL.Remove(tIPO_ROL);
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

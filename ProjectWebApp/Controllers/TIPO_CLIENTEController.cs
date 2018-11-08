using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebApp.Models;

namespace ProjectWebApp.Controllers
{
    public class TIPO_CLIENTEController : Controller
    {
        private ODAO db = new ODAO();

        // GET: TIPO_CLIENTE
        public ActionResult Index()
        {
            return View(db.TIPO_CLIENTE.ToList());
        }

        // GET: TIPO_CLIENTE/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CLIENTE tIPO_CLIENTE = db.TIPO_CLIENTE.Find(id);
            if (tIPO_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CLIENTE);
        }

        // GET: TIPO_CLIENTE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIPO_CLIENTE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TIPO_CLIENTE,NOMBRE_TIPO,TIMESTAMP")] TIPO_CLIENTE tIPO_CLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.TIPO_CLIENTE.Add(tIPO_CLIENTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIPO_CLIENTE);
        }

        // GET: TIPO_CLIENTE/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CLIENTE tIPO_CLIENTE = db.TIPO_CLIENTE.Find(id);
            if (tIPO_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CLIENTE);
        }

        // POST: TIPO_CLIENTE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TIPO_CLIENTE,NOMBRE_TIPO,TIMESTAMP")] TIPO_CLIENTE tIPO_CLIENTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIPO_CLIENTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIPO_CLIENTE);
        }

        // GET: TIPO_CLIENTE/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIPO_CLIENTE tIPO_CLIENTE = db.TIPO_CLIENTE.Find(id);
            if (tIPO_CLIENTE == null)
            {
                return HttpNotFound();
            }
            return View(tIPO_CLIENTE);
        }

        // POST: TIPO_CLIENTE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            TIPO_CLIENTE tIPO_CLIENTE = db.TIPO_CLIENTE.Find(id);
            db.TIPO_CLIENTE.Remove(tIPO_CLIENTE);
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

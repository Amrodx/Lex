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
    public class DETALLE_TRAMITESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: DETALLE_TRAMITES
        public ActionResult Index()
        {
            var dETALLE_TRAMITES = db.DETALLE_TRAMITES.Include(d => d.CONTRATOS);
            return View(dETALLE_TRAMITES.ToList());
        }

        // GET: DETALLE_TRAMITES/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_TRAMITES dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
            if (dETALLE_TRAMITES == null)
            {
                return HttpNotFound();
            }
            return View(dETALLE_TRAMITES);
        }

        // GET: DETALLE_TRAMITES/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO");
            return View();
        }

        // POST: DETALLE_TRAMITES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TRAMITES,ID_CONTRATO,ID_SOLICITUD,CODIGO_TRAMITE,NOMBRE_TRAMITE,COSTO,STATUS,TIMESTAMP,CREATED")] DETALLE_TRAMITES dETALLE_TRAMITES)
        {
            if (ModelState.IsValid)
            {
                db.DETALLE_TRAMITES.Add(dETALLE_TRAMITES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_TRAMITES.ID_CONTRATO);
            return View(dETALLE_TRAMITES);
        }

        // GET: DETALLE_TRAMITES/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_TRAMITES dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
            if (dETALLE_TRAMITES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_TRAMITES.ID_CONTRATO);
            return View(dETALLE_TRAMITES);
        }

        // POST: DETALLE_TRAMITES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TRAMITES,ID_CONTRATO,ID_SOLICITUD,CODIGO_TRAMITE,NOMBRE_TRAMITE,COSTO,STATUS,TIMESTAMP,CREATED")] DETALLE_TRAMITES dETALLE_TRAMITES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dETALLE_TRAMITES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_TRAMITES.ID_CONTRATO);
            return View(dETALLE_TRAMITES);
        }

        // GET: DETALLE_TRAMITES/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_TRAMITES dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
            if (dETALLE_TRAMITES == null)
            {
                return HttpNotFound();
            }
            return View(dETALLE_TRAMITES);
        }

        // POST: DETALLE_TRAMITES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DETALLE_TRAMITES dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
            db.DETALLE_TRAMITES.Remove(dETALLE_TRAMITES);
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

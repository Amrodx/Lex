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
    public class DETALLE_COSTOSController : Controller
    {
        private ODAO db = new ODAO();

        // GET: DETALLE_COSTOS
        public ActionResult Index()
        {
            var dETALLE_COSTOS = db.DETALLE_COSTOS.Include(d => d.CONTRATOS);
            return View(dETALLE_COSTOS.ToList());
        }

        // GET: DETALLE_COSTOS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_COSTOS dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
            if (dETALLE_COSTOS == null)
            {
                return HttpNotFound();
            }
            return View(dETALLE_COSTOS);
        }

        // GET: DETALLE_COSTOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO");
            return View();
        }

        // POST: DETALLE_COSTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DETALLE_COSTO,ID_CONTRATO,TIPO_GASTO,MONTO,BOLETA_DIGITALIZADA,FECHA,AUTORIZADA_REEMBOLZO,ASISTENTE_RESPONZABLE,NOMBRE_NOTARIA,DIRECCION_NOTARIA")] DETALLE_COSTOS dETALLE_COSTOS)
        {
            if (ModelState.IsValid)
            {
                db.DETALLE_COSTOS.Add(dETALLE_COSTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_COSTOS.ID_CONTRATO);
            return View(dETALLE_COSTOS);
        }

        // GET: DETALLE_COSTOS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_COSTOS dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
            if (dETALLE_COSTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_COSTOS.ID_CONTRATO);
            return View(dETALLE_COSTOS);
        }

        // POST: DETALLE_COSTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DETALLE_COSTO,ID_CONTRATO,TIPO_GASTO,MONTO,BOLETA_DIGITALIZADA,FECHA,AUTORIZADA_REEMBOLZO,ASISTENTE_RESPONZABLE,NOMBRE_NOTARIA,DIRECCION_NOTARIA")] DETALLE_COSTOS dETALLE_COSTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dETALLE_COSTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_COSTOS.ID_CONTRATO);
            return View(dETALLE_COSTOS);
        }

        // GET: DETALLE_COSTOS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETALLE_COSTOS dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
            if (dETALLE_COSTOS == null)
            {
                return HttpNotFound();
            }
            return View(dETALLE_COSTOS);
        }

        // POST: DETALLE_COSTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DETALLE_COSTOS dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
            db.DETALLE_COSTOS.Remove(dETALLE_COSTOS);
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

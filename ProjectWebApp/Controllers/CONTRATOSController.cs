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
    public class CONTRATOSController : Controller
    {
        private ODAO db = new ODAO();

        // GET: CONTRATOS
        public ActionResult Index()
        {
            var cONTRATOS = db.CONTRATOS.Include(c => c.ASISTENTES).Include(c => c.PRESUPUESTO);
            return View(cONTRATOS.ToList());
        }

        // GET: CONTRATOS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATOS cONTRATOS = db.CONTRATOS.Find(id);
            if (cONTRATOS == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATOS);
        }

        // GET: CONTRATOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_ASISTENTE = new SelectList(db.ASISTENTES, "ID_ASISTENTE", "RUT");
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD");
            return View();
        }

        // POST: CONTRATOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CONTRATO,FECHA_INICIO,FECHA_TERMINO,ID_PRESUPUESTO,COSTO_ESTIMADO,DOCUMENTO_GENERADO,CORREO_ENVIADO,COMENTARIOS,ID_ASISTENTE")] CONTRATOS cONTRATOS)
        {
            if (ModelState.IsValid)
            {
                db.CONTRATOS.Add(cONTRATOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ASISTENTE = new SelectList(db.ASISTENTES, "ID_ASISTENTE", "RUT", cONTRATOS.ID_ASISTENTE);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", cONTRATOS.ID_PRESUPUESTO);
            return View(cONTRATOS);
        }

        // GET: CONTRATOS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATOS cONTRATOS = db.CONTRATOS.Find(id);
            if (cONTRATOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ASISTENTE = new SelectList(db.ASISTENTES, "ID_ASISTENTE", "RUT", cONTRATOS.ID_ASISTENTE);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", cONTRATOS.ID_PRESUPUESTO);
            return View(cONTRATOS);
        }

        // POST: CONTRATOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CONTRATO,FECHA_INICIO,FECHA_TERMINO,ID_PRESUPUESTO,COSTO_ESTIMADO,DOCUMENTO_GENERADO,CORREO_ENVIADO,COMENTARIOS,ID_ASISTENTE")] CONTRATOS cONTRATOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONTRATOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ASISTENTE = new SelectList(db.ASISTENTES, "ID_ASISTENTE", "RUT", cONTRATOS.ID_ASISTENTE);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", cONTRATOS.ID_PRESUPUESTO);
            return View(cONTRATOS);
        }

        // GET: CONTRATOS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONTRATOS cONTRATOS = db.CONTRATOS.Find(id);
            if (cONTRATOS == null)
            {
                return HttpNotFound();
            }
            return View(cONTRATOS);
        }

        // POST: CONTRATOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CONTRATOS cONTRATOS = db.CONTRATOS.Find(id);
            db.CONTRATOS.Remove(cONTRATOS);
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

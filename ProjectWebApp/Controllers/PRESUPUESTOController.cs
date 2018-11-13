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
    public class PRESUPUESTOController : Controller
    {
        private ODAO db = new ODAO();

        // GET: PRESUPUESTO
        public ActionResult Index()
        {
            var pRESUPUESTO = db.PRESUPUESTO.Include(p => p.CAUSALES).Include(p => p.CLIENTES);
            return View(pRESUPUESTO.ToList());
        }

        // GET: PRESUPUESTO/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESUPUESTO pRESUPUESTO = db.PRESUPUESTO.Find(id);
            if (pRESUPUESTO == null)
            {
                return HttpNotFound();
            }
            return View(pRESUPUESTO);
        }

        // GET: PRESUPUESTO/Create
        public ActionResult Create()
        {
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
            return View();
        }

        // POST: PRESUPUESTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRESUPUESTO,COD_SOLICITUD,FECHA,ESTADO_AVANCE,CONTRATADO,OBSERVACIONES,ID_ASISTENTE,ID_CAUSAL,ID_USUARIO")] PRESUPUESTO pRESUPUESTO)
        {
            if (ModelState.IsValid)
            {
                db.PRESUPUESTO.Add(pRESUPUESTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.CLIENTES);
            return View(pRESUPUESTO);
        }

        // GET: PRESUPUESTO/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESUPUESTO pRESUPUESTO = db.PRESUPUESTO.Find(id);
            if (pRESUPUESTO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.CLIENTES);
            return View(pRESUPUESTO);
        }

        // POST: PRESUPUESTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRESUPUESTO,COD_SOLICITUD,FECHA,ESTADO_AVANCE,CONTRATADO,OBSERVACIONES,ID_ASISTENTE,ID_CAUSAL,ID_USUARIO")] PRESUPUESTO pRESUPUESTO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRESUPUESTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.CLIENTES);
            return View(pRESUPUESTO);
        }

        // GET: PRESUPUESTO/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESUPUESTO pRESUPUESTO = db.PRESUPUESTO.Find(id);
            if (pRESUPUESTO == null)
            {
                return HttpNotFound();
            }
            return View(pRESUPUESTO);
        }

        // POST: PRESUPUESTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PRESUPUESTO pRESUPUESTO = db.PRESUPUESTO.Find(id);
            db.PRESUPUESTO.Remove(pRESUPUESTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Presupuesto()
        {
            return View();
        }
        public ActionResult Contrato()
        {
            return View();
        }
        public ActionResult OrdenPago()
        {
            return View();
        }
        public ActionResult Recepcion()
        {
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES.ToList(), "ID_CAUSAL", "NOMBRE");
            ViewBag.Documento = db.DOCUMENTOS.Where(w => w.MIME_TYPE.Contains("pdf") && w.NOMBRE_ARCHIVO.Contains("SOLICITUD")).FirstOrDefault();

            return View();
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

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
    public class PRESUPUESTOController : Controller
    {
        private ODAO db = new ODAO();

        // GET: PRESUPUESTO
        public ActionResult Index()
        {
            List<PRESUPUESTO> pRESUPUESTO = new List<PRESUPUESTO>();
            try
            {
                pRESUPUESTO = db.PRESUPUESTO.Include(p => p.CAUSALES).Include(p => p.USUARIOS).ToList();
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {
                return View(pRESUPUESTO);
            }

        }

        // GET: PRESUPUESTO/Details/5
        public ActionResult Details(long? id)
        {
            PRESUPUESTO pRESUPUESTO = new PRESUPUESTO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                pRESUPUESTO = db.PRESUPUESTO.Find(id);
                if (pRESUPUESTO == null)
                {
                    return HttpNotFound();
                }
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {

                return View(pRESUPUESTO);
            }

        }

        // GET: PRESUPUESTO/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE");
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
                return View();
            }
            catch (Exception)
            {
                ViewBag.ID_CAUSAL = new List<string>();
                ViewBag.ID_USUARIO = new List<string>();
                return View();
            }

        }

        // POST: PRESUPUESTO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRESUPUESTO,COD_SOLICITUD,FECHA,ESTADO_AVANCE,CONTRATADO,OBSERVACIONES,ID_ASISTENTE,ID_CAUSAL,ID_USUARIO")] PRESUPUESTO pRESUPUESTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PRESUPUESTO.Add(pRESUPUESTO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.ID_USUARIO);
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {
                ViewBag.ID_CAUSAL = new SelectList(new List<string>());
                ViewBag.ID_USUARIO = new SelectList(new List<string>());
                return View(pRESUPUESTO);
            }

        }

        // GET: PRESUPUESTO/Edit/5
        public ActionResult Edit(long? id)
        {
            PRESUPUESTO pRESUPUESTO = new PRESUPUESTO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                pRESUPUESTO = db.PRESUPUESTO.Find(id);
                if (pRESUPUESTO == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.ID_USUARIO);
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {
                ViewBag.ID_CAUSAL = new SelectList(new List<string>());
                ViewBag.ID_USUARIO = new SelectList(new List<string>());
                return View(pRESUPUESTO);
            }

        }

        // POST: PRESUPUESTO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRESUPUESTO,COD_SOLICITUD,FECHA,ESTADO_AVANCE,CONTRATADO,OBSERVACIONES,ID_ASISTENTE,ID_CAUSAL,ID_USUARIO")] PRESUPUESTO pRESUPUESTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pRESUPUESTO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.ID_USUARIO);
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {
                ViewBag.ID_CAUSAL = new SelectList(new List<string>());
                ViewBag.ID_USUARIO = new SelectList(new List<string>());
                return View(pRESUPUESTO);
            }

        }

        // GET: PRESUPUESTO/Delete/5
        public ActionResult Delete(long? id)
        {
            PRESUPUESTO pRESUPUESTO = new PRESUPUESTO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                pRESUPUESTO = db.PRESUPUESTO.Find(id);
                if (pRESUPUESTO == null)
                {
                    return HttpNotFound();
                }
                return View(pRESUPUESTO);
            }
            catch (Exception)
            {

                return View(pRESUPUESTO);
            }

        }

        // POST: PRESUPUESTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                if (id > 0)
                {
                    PRESUPUESTO pRESUPUESTO = db.PRESUPUESTO.Find(id);
                    db.PRESUPUESTO.Remove(pRESUPUESTO);
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

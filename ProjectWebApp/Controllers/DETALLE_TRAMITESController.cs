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
    public class DETALLE_TRAMITESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: DETALLE_TRAMITES
        public ActionResult Index()
        {
            List<DETALLE_TRAMITES> dETALLE_TRAMITES = new List<DETALLE_TRAMITES>();
            try
            {
                dETALLE_TRAMITES = db.DETALLE_TRAMITES.Include(d => d.CONTRATOS).ToList();
                return View(dETALLE_TRAMITES);
            }
            catch (Exception)
            {

                return View(dETALLE_TRAMITES);
            }
        }

        // GET: DETALLE_TRAMITES/Details/5
        public ActionResult Details(long? id)
        {
            DETALLE_TRAMITES dETALLE_TRAMITES = new DETALLE_TRAMITES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
                if (dETALLE_TRAMITES == null)
                {
                    return HttpNotFound();
                }
                return View(dETALLE_TRAMITES);
            }
            catch (Exception)
            {

                return View(dETALLE_TRAMITES);
            }
        }

        // GET: DETALLE_TRAMITES/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO");
                return View();
            }
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList( new List<string>());
                return View();
            }

        }

        // POST: DETALLE_TRAMITES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_TRAMITES,ID_CONTRATO,ID_SOLICITUD,CODIGO_TRAMITE,NOMBRE_TRAMITE,COSTO,STATUS,TIMESTAMP,CREATED")] DETALLE_TRAMITES dETALLE_TRAMITES)
        {
            try
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
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList(new List<string>());
                return View(dETALLE_TRAMITES);
            }

        }

        // GET: DETALLE_TRAMITES/Edit/5
        public ActionResult Edit(long? id)
        {
            DETALLE_TRAMITES dETALLE_TRAMITES = new DETALLE_TRAMITES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
                if (dETALLE_TRAMITES == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_TRAMITES.ID_CONTRATO);
                return View(dETALLE_TRAMITES);
            }
            catch (Exception)
            {

                return View(dETALLE_TRAMITES);
            }

        }

        // POST: DETALLE_TRAMITES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TRAMITES,ID_CONTRATO,ID_SOLICITUD,CODIGO_TRAMITE,NOMBRE_TRAMITE,COSTO,STATUS,TIMESTAMP,CREATED")] DETALLE_TRAMITES dETALLE_TRAMITES)
        {
            try
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
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList(new List<string>());
                return RedirectToAction("Index");
            }
        }

        // GET: DETALLE_TRAMITES/Delete/5
        public ActionResult Delete(long? id)
        {
            DETALLE_TRAMITES dETALLE_TRAMITES = new DETALLE_TRAMITES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
                if (dETALLE_TRAMITES == null)
                {
                    return HttpNotFound();
                }
                return View(dETALLE_TRAMITES);
            }
            catch (Exception)
            {

                return View(dETALLE_TRAMITES);
            }

        }

        // POST: DETALLE_TRAMITES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DETALLE_TRAMITES dETALLE_TRAMITES = new DETALLE_TRAMITES();
            try
            {
                if (id > 0)
                {
                    dETALLE_TRAMITES = db.DETALLE_TRAMITES.Find(id);
                    db.DETALLE_TRAMITES.Remove(dETALLE_TRAMITES);
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

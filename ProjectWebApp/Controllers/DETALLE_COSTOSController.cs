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
    public class DETALLE_COSTOSController : Controller
    {
        private ODAO db = new ODAO();

        // GET: DETALLE_COSTOS
        public ActionResult Index()
        {
            List<DETALLE_COSTOS> dETALLE_COSTOS = new List<DETALLE_COSTOS>();
            try
            {
                dETALLE_COSTOS = db.DETALLE_COSTOS.Include(d => d.CONTRATOS).ToList();
                return View(dETALLE_COSTOS);
            }
            catch (Exception)
            {
                return View(dETALLE_COSTOS);
            }

        }

        // GET: DETALLE_COSTOS/Details/5
        public ActionResult Details(long? id)
        {
            DETALLE_COSTOS dETALLE_COSTOS = new DETALLE_COSTOS();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
                if (dETALLE_COSTOS == null)
                {
                    return HttpNotFound();
                }
                return View(dETALLE_COSTOS);
            }
            catch (Exception)
            {
                return View(dETALLE_COSTOS);
            }

        }

        // GET: DETALLE_COSTOS/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO");
                if (ViewBag.ID_CONTRATO == null)
                {
                    ViewBag.ID_CONTRATO = "";
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }

        }

        // POST: DETALLE_COSTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DETALLE_COSTO,ID_CONTRATO,TIPO_GASTO,MONTO,BOLETA_DIGITALIZADA,FECHA,AUTORIZADA_REEMBOLZO,ASISTENTE_RESPONZABLE,NOMBRE_NOTARIA,DIRECCION_NOTARIA")] DETALLE_COSTOS dETALLE_COSTOS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DETALLE_COSTOS.Add(dETALLE_COSTOS);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_COSTOS.ID_CONTRATO);
                if (ViewBag.ID_CONTRATO == null)
                {
                    ViewBag.ID_CONTRATO = "";
                }
                return View(dETALLE_COSTOS);
            }
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList(new List<string>());
                return View(dETALLE_COSTOS);
            }

        }

        // GET: DETALLE_COSTOS/Edit/5
        public ActionResult Edit(long? id)
        {
            DETALLE_COSTOS dETALLE_COSTOS = new DETALLE_COSTOS();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
                if (dETALLE_COSTOS == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dETALLE_COSTOS.ID_CONTRATO);
                if (ViewBag.ID_CONTRATO == null)
                {
                    ViewBag.ID_CONTRATO = "";
                }
                return View(dETALLE_COSTOS);
            }
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = "";
                return View(dETALLE_COSTOS);
            }

        }

        // POST: DETALLE_COSTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DETALLE_COSTO,ID_CONTRATO,TIPO_GASTO,MONTO,BOLETA_DIGITALIZADA,FECHA,AUTORIZADA_REEMBOLZO,ASISTENTE_RESPONZABLE,NOMBRE_NOTARIA,DIRECCION_NOTARIA")] DETALLE_COSTOS dETALLE_COSTOS)
        {
            try
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
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList(new List<string>());
                return View(dETALLE_COSTOS);
            }
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
            try
            {
                if (id > 0)
                {
                    DETALLE_COSTOS dETALLE_COSTOS = db.DETALLE_COSTOS.Find(id);
                    db.DETALLE_COSTOS.Remove(dETALLE_COSTOS);
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

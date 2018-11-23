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
    public class ORDEN_PAGOController : Controller
    {
        private ODAO db = new ODAO();

        // GET: ORDEN_PAGO
        public ActionResult Index()
        {
            List<ORDEN_PAGO> oRDEN_PAGO = new List<ORDEN_PAGO>();
            try
            {
                oRDEN_PAGO = db.ORDEN_PAGO.Include(o => o.CONTRATOS).ToList(); ;
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {

                return View(oRDEN_PAGO);
            }

        }

        // GET: ORDEN_PAGO/Details/5
        public ActionResult Details(long? id)
        {
            ORDEN_PAGO oRDEN_PAGO = new ORDEN_PAGO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                oRDEN_PAGO = db.ORDEN_PAGO.Find(id);
                if (oRDEN_PAGO == null)
                {
                    return HttpNotFound();
                }
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {

                return View(oRDEN_PAGO);
            }

        }

        // GET: ORDEN_PAGO/Create
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

        // POST: ORDEN_PAGO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ORDEN_PAGO,TIPO_PAGO,ID_CONTRATO,MONTO_TOTAL,ESTADO,IP,CREATED,NUMERO_TRANSACCION,CUOTAS,VALOR_CUOTA")] ORDEN_PAGO oRDEN_PAGO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.ORDEN_PAGO.Add(oRDEN_PAGO);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", oRDEN_PAGO.ID_CONTRATO);
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList(new List<string>());
                return View(oRDEN_PAGO);
            }
        }

        // GET: ORDEN_PAGO/Edit/5
        public ActionResult Edit(long? id)
        {
            ORDEN_PAGO oRDEN_PAGO = new ORDEN_PAGO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                oRDEN_PAGO = db.ORDEN_PAGO.Find(id);
                if (oRDEN_PAGO == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", oRDEN_PAGO.ID_CONTRATO);
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {

                return View(oRDEN_PAGO);
            }

        }

        // POST: ORDEN_PAGO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ORDEN_PAGO,TIPO_PAGO,ID_CONTRATO,MONTO_TOTAL,ESTADO,IP,CREATED,NUMERO_TRANSACCION,CUOTAS,VALOR_CUOTA")] ORDEN_PAGO oRDEN_PAGO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(oRDEN_PAGO).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", oRDEN_PAGO.ID_CONTRATO);
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {
                ViewBag.ID_CONTRATO = new SelectList( new List<string>());
                return View(oRDEN_PAGO);
            }

        }

        // GET: ORDEN_PAGO/Delete/5
        public ActionResult Delete(long? id)
        {
            ORDEN_PAGO oRDEN_PAGO = new ORDEN_PAGO();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                oRDEN_PAGO = db.ORDEN_PAGO.Find(id);
                if (oRDEN_PAGO == null)
                {
                    return HttpNotFound();
                }
                return View(oRDEN_PAGO);
            }
            catch (Exception)
            {

                return View(oRDEN_PAGO);
            }

        }

        // POST: ORDEN_PAGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ORDEN_PAGO oRDEN_PAGO = new ORDEN_PAGO();
            try
            {
                if (id.Equals(null) || id < 1)
                {
                    oRDEN_PAGO = db.ORDEN_PAGO.Find(id);
                    db.ORDEN_PAGO.Remove(oRDEN_PAGO);
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

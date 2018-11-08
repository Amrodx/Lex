using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebApp.Models;

namespace ProjectWebApp.Controllers
{
    public class DOCUMENTOSController : Controller
    {
        private ODAO db = new ODAO();

        // GET: DOCUMENTOS
        public ActionResult Index()
        {
            var dOCUMENTOS = db.DOCUMENTOS.Include(d => d.CONTRATOS).Include(d => d.PRESUPUESTO);
            return View(dOCUMENTOS.ToList());
        }

        // GET: DOCUMENTOS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTOS dOCUMENTOS = db.DOCUMENTOS.Find(id);
            if (dOCUMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dOCUMENTOS);
        }

        // GET: DOCUMENTOS/Create
        public ActionResult Create()
        {
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO");
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD");
            return View();
        }

        // POST: DOCUMENTOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DOCUMENTO,ID_CONTRATO,ID_PRESUPUESTO,FECHA_INGRESO,DOCUMENTO_DIGITAL,PATH")] DOCUMENTOS dOCUMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.DOCUMENTOS.Add(dOCUMENTOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dOCUMENTOS.ID_CONTRATO);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", dOCUMENTOS.ID_PRESUPUESTO);
            return View(dOCUMENTOS);
        }

        // GET: DOCUMENTOS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTOS dOCUMENTOS = db.DOCUMENTOS.Find(id);
            if (dOCUMENTOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dOCUMENTOS.ID_CONTRATO);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", dOCUMENTOS.ID_PRESUPUESTO);
            return View(dOCUMENTOS);
        }

        // POST: DOCUMENTOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DOCUMENTO,ID_CONTRATO,ID_PRESUPUESTO,FECHA_INGRESO,DOCUMENTO_DIGITAL,PATH")] DOCUMENTOS dOCUMENTOS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOCUMENTOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CONTRATO = new SelectList(db.CONTRATOS, "ID_CONTRATO", "DOCUMENTO_GENERADO", dOCUMENTOS.ID_CONTRATO);
            ViewBag.ID_PRESUPUESTO = new SelectList(db.PRESUPUESTO, "ID_PRESUPUESTO", "COD_SOLICITUD", dOCUMENTOS.ID_PRESUPUESTO);
            return View(dOCUMENTOS);
        }

        // GET: DOCUMENTOS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOCUMENTOS dOCUMENTOS = db.DOCUMENTOS.Find(id);
            if (dOCUMENTOS == null)
            {
                return HttpNotFound();
            }
            return View(dOCUMENTOS);
        }

        // POST: DOCUMENTOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            DOCUMENTOS dOCUMENTOS = db.DOCUMENTOS.Find(id);
            db.DOCUMENTOS.Remove(dOCUMENTOS);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SubidaDocumentos(DOCUMENTOS _pdocumentos)
        {

            foreach (string f in Request.Files.Keys)
            {

                HttpPostedFileBase File = Request.Files["IMG_PROFILE"];

                if (Request.Files[f].ContentLength > 0)
                {
                    _pdocumentos.NOMBRE_ARCHIVO = Request.Files[f].FileName;
                    _pdocumentos.MIME_TYPE = Request.Files[f].ContentType;
                    _pdocumentos.DOCUMENTO_DIGITAL = ConvertToByte(Request.Files[f]);
                }
                db.DOCUMENTOS.Add(_pdocumentos);
                db.SaveChanges();



            }
            return View();
        }

        public ActionResult BajadaDocumentos(DOCUMENTOS _pdocumentos)
        {

            {
                var dOCUMENTOS = db.DOCUMENTOS.Include(d => d.CONTRATOS).Include(d => d.PRESUPUESTO);
                return View(dOCUMENTOS.ToList());
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

        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }
    }
}

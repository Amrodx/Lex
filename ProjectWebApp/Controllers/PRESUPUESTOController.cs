using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;
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
        [HttpGet]
        public ActionResult GEProcesos()
        {
            CLIENTES cLIENTE = ((USUARIOS)Session["LoginCredentials"]).CLIENTES.First();
            List<PRESUPUESTO> pRESUPUESTO = db.PRESUPUESTO.
                Include(p => p.CAUSALES).
                Include(p => p.CLIENTES).
                Include(p => p.ASISTENTES).
                Where(w => w.ID_CLIENTE == cLIENTE.ID_CLIENTE).ToList();
            List<PRESUPUESTO> Conciliado = new List<PRESUPUESTO>();
            foreach (PRESUPUESTO item in pRESUPUESTO)
            {
                if (item.ASISTENTES != null)
                {
                    item.ASISTENTES.USUARIOS = db.USUARIOS.Where(w => w.ID_USUARIO == item.ASISTENTES.ID_USUARIO).FirstOrDefault();
                }
                Conciliado.Add(item);
            }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GestionCliente([Bind(Include = "ID_PRESUPUESTO,COD_SOLICITUD,FECHA,ESTADO_AVANCE,CONTRATADO,OBSERVACIONES,ID_ASISTENTE,ID_CAUSAL,ID_USUARIO")] PRESUPUESTO pRESUPUESTO)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.CLIENTES);

                db.PRESUPUESTO.Add(pRESUPUESTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES, "ID_CAUSAL", "NOMBRE", pRESUPUESTO.ID_CAUSAL);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", pRESUPUESTO.CLIENTES);
            return View(pRESUPUESTO);
        }

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
            using (var client = new WebClient())
            {
                var json = client.DownloadString("http://dev-epsilon.cl/api/TRIBUNAL_JUSTICIA/12");
                var serializer = new JavaScriptSerializer();
                List<string> model = serializer.Deserialize<List<string>>(json);
                // TODO: do something with the model
            }
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

        [HttpGet]
        public ActionResult Procesos(long id)
        {
            PRESUPUESTO SetModelo = db.PRESUPUESTO.Where(w => w.ID_PRESUPUESTO == id).First();
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES.ToList(), "ID_CAUSAL", "NOMBRE");
            ViewBag.Documento = db.DOCUMENTOS.Where(w => w.MIME_TYPE.Contains("pdf") && w.NOMBRE_ARCHIVO.Contains("SOLICITUD")).FirstOrDefault();
            return View(SetModelo);
        }
        [HttpGet]
        public ActionResult CrearProcesos()
        {
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES.ToList(), "ID_CAUSAL", "NOMBRE");
            ViewBag.Documento = db.DOCUMENTOS.Where(w => w.MIME_TYPE.Contains("pdf") && w.NOMBRE_ARCHIVO.Contains("SOLICITUD")).FirstOrDefault();
            return View("Procesos");
        }
        [HttpPost]
        public ActionResult Procesos([Bind(Include = "FECHA_NOTIFICA,OBSERVACIONES,ID_CAUSAL")] PRESUPUESTO pRESUPUESTO)
        {
            CLIENTES cLIENTE = ((USUARIOS)Session["LoginCredentials"]).CLIENTES.First();
            if (ModelState.IsValid)
            {
                pRESUPUESTO.CONTRATADO = "N";
                pRESUPUESTO.FECHA = DateTime.Now;
                pRESUPUESTO.COD_SOLICITUD = "SD_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                pRESUPUESTO.ESTADO_AVANCE = "Recepcionado";
                pRESUPUESTO.ID_CLIENTE = cLIENTE.ID_CLIENTE;
                db.PRESUPUESTO.Add(pRESUPUESTO);
                db.SaveChanges();
            }
            pRESUPUESTO = db.PRESUPUESTO.Include(i => i.ASISTENTES).Where(w => w.COD_SOLICITUD == pRESUPUESTO.COD_SOLICITUD && w.FECHA_NOTIFICA == pRESUPUESTO.FECHA_NOTIFICA).First();
            pRESUPUESTO.ASISTENTES.USUARIOS = db.USUARIOS.Where(w => w.ID_USUARIO == pRESUPUESTO.ASISTENTES.ID_USUARIO).First();
            ViewBag.ID_CAUSAL = new SelectList(db.CAUSALES.ToList(), "ID_CAUSAL", "NOMBRE");
            ViewBag.Documento = db.DOCUMENTOS.Where(w => w.MIME_TYPE.Contains("pdf") && w.NOMBRE_ARCHIVO.Contains("SOLICITUD")).FirstOrDefault();
            ViewBag.Presupuesto = pRESUPUESTO;
            ViewBag.Message = "Presupuesto Enviado para Revisión y Posterior Asignación";
            return View("Procesos",pRESUPUESTO);
        }

        public ActionResult GestionAsistencia()
        {

            List<PRESUPUESTO> pRESUPUESTO = db.PRESUPUESTO.
                                                            Include(p => p.CAUSALES).
                                                            Include(p => p.CLIENTES).
                                                            Include(p => p.ASISTENTES).ToList();
            List<PRESUPUESTO> Conciliado = new List<PRESUPUESTO>();
            foreach (PRESUPUESTO item in pRESUPUESTO)
            {
                if (item.ASISTENTES != null)
                {
                    item.ASISTENTES.USUARIOS = db.USUARIOS.Where(w => w.ID_USUARIO == item.ASISTENTES.ID_USUARIO).FirstOrDefault();
                }
                Conciliado.Add(item);
            }

            return View(pRESUPUESTO);
        }
        public ActionResult Asignaciones(long? id)
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

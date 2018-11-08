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
    public class ASISTENTESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: ASISTENTES
        public ActionResult Index()
        {
            var aSISTENTES = db.ASISTENTES.Include(a => a.USUARIOS);
            return View(aSISTENTES.ToList());
        }

        // GET: ASISTENTES/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASISTENTES aSISTENTES = db.ASISTENTES.Find(id);
            if (aSISTENTES == null)
            {
                return HttpNotFound();
            }
            return View(aSISTENTES);
        }

        // GET: ASISTENTES/Create
        public ActionResult Create()
        {
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
            return View();
        }

        // POST: ASISTENTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            if (ModelState.IsValid)
            {
                db.ASISTENTES.Add(aSISTENTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", aSISTENTES.ID_USUARIO);
            return View(aSISTENTES);
        }

        // GET: ASISTENTES/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASISTENTES aSISTENTES = db.ASISTENTES.Find(id);
            if (aSISTENTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", aSISTENTES.ID_USUARIO);
            return View(aSISTENTES);
        }

        // POST: ASISTENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aSISTENTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", aSISTENTES.ID_USUARIO);
            return View(aSISTENTES);
        }

        // GET: ASISTENTES/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASISTENTES aSISTENTES = db.ASISTENTES.Find(id);
            if (aSISTENTES == null)
            {
                return HttpNotFound();
            }
            return View(aSISTENTES);
        }

        // POST: ASISTENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ASISTENTES aSISTENTES = db.ASISTENTES.Find(id);
            db.ASISTENTES.Remove(aSISTENTES);
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

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
    public class CLIENTESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: CLIENTES
        public ActionResult Index()
        {
            var cLIENTES = db.CLIENTES.Include(c => c.COMUNAS).Include(c => c.TIPO_CLIENTE).Include(c => c.PLAN_PAGO).Include(c => c.USUARIOS);
            return View(cLIENTES.ToList());
        }

        // GET: CLIENTES/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTES);
        }

        // GET: CLIENTES/Create
        public ActionResult Create()
        {
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA");
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO");
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN");
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
            return View();
        }

        // POST: CLIENTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENTE,RUT,NOMBRE_RAZON_SOCIAL,ID_TIPO,DIRECCION,CORREO,CONTACTO,FONO1,FONO2,ID_COMUNA,OBSERVACIONES,TIMESTAMP,STATUS_ACTIVACION,ID_PLAN,ID_USUARIO")] CLIENTES cLIENTES)
        {
            if (ModelState.IsValid)
            {
                db.CLIENTES.Add(cLIENTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
        }

        // GET: CLIENTES/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
        }

        // POST: CLIENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CLIENTE,RUT,NOMBRE_RAZON_SOCIAL,ID_TIPO,DIRECCION,CORREO,CONTACTO,FONO1,FONO2,ID_COMUNA,OBSERVACIONES,TIMESTAMP,STATUS_ACTIVACION,ID_PLAN,ID_USUARIO")] CLIENTES cLIENTES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENTES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
        }

        // GET: CLIENTES/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            return View(cLIENTES);
        }

        // POST: CLIENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CLIENTES cLIENTES = db.CLIENTES.Find(id);
            db.CLIENTES.Remove(cLIENTES);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CompletarPerfil()
        {
            USUARIOS _UsuarioLogued = (USUARIOS)Session["LoginCredentials"];

            if (_UsuarioLogued.CLIENTES.Count() == 0)
            {
                ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA");
                ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO");
                ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN");
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
                return View();
            }

            CLIENTES cLIENTES = db.CLIENTES.Find(_UsuarioLogued.CLIENTES.Where(w => w.ID_USUARIO == _UsuarioLogued.ID_USUARIO));
            if (cLIENTES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
        }

        public ActionResult Perfil()
        {
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];

            CLIENTES cli = db.CLIENTES.Where(X => X.ID_USUARIO == perfil.ID_USUARIO).FirstOrDefault();
            if (cli == null)
            {
                return HttpNotFound();
            }
            return View(cli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompletarPerfil([Bind(Include = "RUT,NOMBRE_RAZON_SOCIAL,DIRECCION,CORREO,CONTACTO,FONO1,FONO2,ID_COMUNA,OBSERVACIONES,ID_PLAN")] CLIENTES cLIENTES)
        {
            USUARIOS _UsuarioLogued = (USUARIOS)Session["LoginCredentials"];

            cLIENTES.ID_USUARIO = _UsuarioLogued.ID_USUARIO;
            cLIENTES.ID_TIPO = _UsuarioLogued.ID_ROL == 41?41:21;
            cLIENTES.ID_CLIENTE = 1;
            cLIENTES.TIMESTAMP = DateTime.Now.ToString();
            cLIENTES.STATUS_ACTIVACION = "Activo";
            if (ModelState.IsValid)
            {
                db.CLIENTES.Add(cLIENTES);
                db.SaveChanges();
                Session["PerfilCliente"] = db.CLIENTES.Where(x => x.RUT == cLIENTES.RUT);
                return RedirectToAction("Perfil");

                //db.Entry(cLIENTES).State = EntityState.Modified;
                //db.SaveChanges();

                //return RedirectToAction("Perfil");
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
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

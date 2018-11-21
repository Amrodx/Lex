﻿using System;
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
    public class ASISTENTESController : Controller
    {
        private ODAO db = new ODAO();

        // GET: ASISTENTES
        public ActionResult Index()
        {
            List<ASISTENTES> aSISTENTES = new List<ASISTENTES>();
            try
            {
                aSISTENTES = db.ASISTENTES.Include(a => a.USUARIOS).ToList();
            }
            catch (Exception)
            {

                return HttpNotFound();
            }
            
            return View(aSISTENTES);
        }

        // GET: ASISTENTES/Details/5
        public ActionResult Details(long? id)
        {
            ASISTENTES aSISTENTES = new ASISTENTES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                aSISTENTES = db.ASISTENTES.Find(id);
                if (aSISTENTES == null)
                {
                    return HttpNotFound();
                }
            }
            catch (Exception)
            {
                return View(aSISTENTES);
            }
            return View(aSISTENTES);
        }

        // GET: ASISTENTES/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
            }
            catch (Exception)
            {
                ViewBag.ID_USUARIO = new List<string>();
            }
            return View();
        }

        // POST: ASISTENTES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            try
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
            catch (Exception)
            {
                ViewBag.ID_USUARIO = new List<string>();
                return View(aSISTENTES);
            }
            
        }

        // GET: ASISTENTES/Edit/5
        public ActionResult Edit(long? id)
        {
            ASISTENTES aSISTENTES = new ASISTENTES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                aSISTENTES = db.ASISTENTES.Find(id);
                if (aSISTENTES == null)
                {
                    return HttpNotFound();
                }
                ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", aSISTENTES.ID_USUARIO);
                return View(aSISTENTES);
            }
            catch (Exception)
            {
                ViewBag.ID_USUARIO = new List<string>();
                return View(aSISTENTES);
            }
            
        }

        // POST: ASISTENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            try
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
            catch (Exception)
            {
                ViewBag.ID_USUARIO = new List<string>();
                return View(aSISTENTES);
            }
        }

        // GET: ASISTENTES/Delete/5
        public ActionResult Delete(long? id)
        {
            ASISTENTES aSISTENTES = new ASISTENTES();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                aSISTENTES = db.ASISTENTES.Find(id);
                if (aSISTENTES == null)
                {
                    return HttpNotFound();
                }
                return View(aSISTENTES);
            }
            catch (Exception)
            {
                return View(aSISTENTES);
            }
            
        }

        // POST: ASISTENTES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                ASISTENTES aSISTENTES = db.ASISTENTES.Find(id);
                db.ASISTENTES.Remove(aSISTENTES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult CompletarPerfil()
        {
            USUARIOS _UsuarioLogued = (USUARIOS)Session["LoginCredentials"];
            try
            {
                if (_UsuarioLogued.CLIENTES.Count() == 0)
                {
                    ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA");
                    ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO");
                    ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN");
                    ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER");
                    return View();
                }

                //CLIENTES cLIENTES = db.CLIENTES.Find(_UsuarioLogued.CLIENTES.Where(w => w.ID_USUARIO == _UsuarioLogued.ID_USUARIO));
                //if (cLIENTES == null)
                //{
                //    return HttpNotFound();
                //}
                //ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
                //ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
                //ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
                //ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
                return View();
            }
            catch (Exception)
            {
                return HttpNotFound();
            }
            
        }

        public ActionResult Perfil()
        {
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];
            ASISTENTES asi = new ASISTENTES();
            try
            {
                asi = db.ASISTENTES.Where(X => X.ID_USUARIO == perfil.ID_USUARIO).FirstOrDefault();
                if (asi == null)
                {
                    return HttpNotFound();
                }
                return View(asi);
            }
            catch (Exception)
            {
                return View(asi);
            }
            
        }


        public ActionResult DataContact(long? id)
        {
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];
            ASISTENTES asis = new ASISTENTES();
            try
            {
                asis = db.ASISTENTES.Where(x => x.ID_USUARIO == perfil.ID_USUARIO).FirstOrDefault();
                if (asis == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
                //ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
                //ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
                //ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
                return View(asis);
            }
            catch (Exception)
            {
                return View(asis);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataContact([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(aSISTENTES).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View(aSISTENTES);
            }
            
            //ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            //ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            //ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            //ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(aSISTENTES);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CompletarPerfil([Bind(Include = "ID_ASISTENTE,RUT,NOMBRES,APELLIDO_PATERNO,APELLIDO_MATERNO,CARGO,TITULO_ACADEMICO,TIMESTAMP,ID_USUARIO")] ASISTENTES aSISTENTES)
        {
            USUARIOS _UsuarioLogued = (USUARIOS)Session["LoginCredentials"];
            try
            {
                aSISTENTES.ID_USUARIO = _UsuarioLogued.ID_USUARIO;
                // aSISTENTES.ID_TIPO = _UsuarioLogued.ID_ROL == 41 ? 41 : 21;
                aSISTENTES.ID_ASISTENTE = 1;
                aSISTENTES.TIMESTAMP = DateTime.Now;
                // aSISTENTES.STATUS_ACTIVACION = "Activo";
                if (ModelState.IsValid)
                {
                    db.ASISTENTES.Add(aSISTENTES);
                    db.SaveChanges();
                    Session["PerfilCliente"] = db.ASISTENTES.Where(x => x.RUT == aSISTENTES.RUT);
                    return RedirectToAction("Perfil");

                    //db.Entry(cLIENTES).State = EntityState.Modified;
                    //db.SaveChanges();

                    //return RedirectToAction("Perfil");
                }
                //ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
                //ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
                //ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
                //ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
                return View(aSISTENTES);
            }
            catch (Exception)
            {
                return View(aSISTENTES);
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

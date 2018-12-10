using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectWebApp.Models;
using ProjectWebApp.DataNotation;

namespace ProjectWebApp.Controllers
{
    public class CLIENTESController : Controller
    {
        private ODAO db = new ODAO();


        // POST: CLIENTES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Perfil([Bind(Include = "ID_CLIENTE,RUT,NOMBRE_RAZON_SOCIAL,ID_TIPO,DIRECCION,CORREO,CONTACTO,FONO1,FONO2,ID_COMUNA,OBSERVACIONES,TIMESTAMP,STATUS_ACTIVACION,ID_PLAN,ID_USUARIO")] CLIENTES cLIENTES)
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
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];
            //if ((long?)perfil.ID_USUARIO == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
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

        public ActionResult Landing(long? id)
        {
            Dictionary<string, int> ValoresGraf = new Dictionary<string, int>();
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];
            List<PRESUPUESTO> PreSupuesto = new List<PRESUPUESTO>();
            CLIENTES cli = db.CLIENTES.Include(i => i.USUARIOS).Include(i => i.TIPO_CLIENTE).Include(i => i.PLAN_PAGO).Where(X => X.ID_USUARIO == perfil.ID_USUARIO).First();
            cli.COMUNAS = db.COMUNAS.Include(i => i.REGIONES).Where(w => w.ID_COMUNA == cli.ID_COMUNA).First();
            PreSupuesto = db.PRESUPUESTO.Include(i => i.CAUSALES).Where(w => w.ID_CLIENTE == cli.ID_CLIENTE).ToList();
            cli.PRESUPUESTO = PreSupuesto;
            foreach (var item in PreSupuesto.GroupBy(g => g.ESTADO_AVANCE).Select(group => new { Campo = group.Key, campo2 = group.Count() }).ToArray())
            {
                ValoresGraf.Add(item.Campo, item.campo2);
            } 

            //ViewBag.Graf = db.PRESUPUESTO.GroupBy(g => g.ESTADO_AVANCE).Select(group => Tuple.Create(group.Key, group.Count()));

            if (cli == null)
            {
                return HttpNotFound();
            }
            ViewBag.Graf = ValoresGraf;
            ViewBag.Perfil = perfil;
            return View(cli);
        }

        public ActionResult DataContact(long? id)
        {
            USUARIOS perfil = (USUARIOS)Session["LoginCredentials"];

            CLIENTES cLIENTES = db.CLIENTES.Where( x=> x.ID_USUARIO == perfil.ID_USUARIO).FirstOrDefault();
            ClientModel clientModel = new ClientModel()
            {
                ID_CLIENTE = cLIENTES.ID_CLIENTE,
                ID_USUARIO = cLIENTES.ID_USUARIO,
                CONTACTO = cLIENTES.CONTACTO,
                CORREO = cLIENTES.CORREO,
                DIRECCION = cLIENTES.DIRECCION,
                FONO1 = cLIENTES.FONO1,
                FONO2 = cLIENTES.FONO2,
                ID_COMUNA = cLIENTES.ID_COMUNA,
                ID_PLAN = cLIENTES.ID_PLAN,
                ID_TIPO = cLIENTES.ID_TIPO,
                NOMBRE_RAZON_SOCIAL = cLIENTES.NOMBRE_RAZON_SOCIAL,
                OBSERVACIONES = cLIENTES.OBSERVACIONES,
                RUT = cLIENTES.RUT,
                STATUS_ACTIVACION = cLIENTES.STATUS_ACTIVACION,
                TIMESTAMP = cLIENTES.TIMESTAMP
            };
            //if (cLIENTES == null)
            //{
            //    return HttpNotFound();
            //}
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(clientModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DataContact([Bind(Include = "ID_CLIENTE,RUT,NOMBRE_RAZON_SOCIAL,ID_TIPO,DIRECCION,CORREO,CONTACTO,FONO1,FONO2,ID_COMUNA,OBSERVACIONES,TIMESTAMP,STATUS_ACTIVACION,ID_PLAN,ID_USUARIO")] ClientModel cLIENTES)
        {
            if (ModelState.IsValid)
            {
                CLIENTES clienInsert = new CLIENTES()
                {
                    ID_CLIENTE = cLIENTES.ID_CLIENTE,
                    ID_USUARIO = cLIENTES.ID_USUARIO,
                    CONTACTO = cLIENTES.CONTACTO,
                    CORREO = cLIENTES.CORREO,
                    DIRECCION = cLIENTES.DIRECCION,
                    FONO1 = cLIENTES.FONO1,
                    FONO2 = cLIENTES.FONO2,
                    ID_COMUNA = cLIENTES.ID_COMUNA,
                    ID_PLAN = cLIENTES.ID_PLAN,
                    ID_TIPO = cLIENTES.ID_TIPO,
                    NOMBRE_RAZON_SOCIAL = cLIENTES.NOMBRE_RAZON_SOCIAL,
                    OBSERVACIONES = cLIENTES.OBSERVACIONES,
                    RUT = cLIENTES.RUT,
                    STATUS_ACTIVACION = cLIENTES.STATUS_ACTIVACION,
                    TIMESTAMP = cLIENTES.TIMESTAMP
                };

                db.Entry(clienInsert).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_COMUNA = new SelectList(db.COMUNAS, "ID_COMUNA", "COMUNA", cLIENTES.ID_COMUNA);
            ViewBag.ID_TIPO = new SelectList(db.TIPO_CLIENTE, "ID_TIPO_CLIENTE", "NOMBRE_TIPO", cLIENTES.ID_TIPO);
            ViewBag.ID_PLAN = new SelectList(db.PLAN_PAGO, "ID_PAGO", "PLAN", cLIENTES.ID_PLAN);
            ViewBag.ID_USUARIO = new SelectList(db.USUARIOS, "ID_USUARIO", "USER", cLIENTES.ID_USUARIO);
            return View(cLIENTES);
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

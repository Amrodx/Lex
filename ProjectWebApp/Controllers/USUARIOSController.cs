using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LexAbogadosWeb.Models;
using System.Security.Cryptography;
using LexAbogadosWeb.Controllers.Servicio;
using System.Configuration;
using System.IO;

namespace LexAbogadosWeb.Controllers
{ 

    public class USUARIOSController : Controller
    {

        private ODAO db = new ODAO();
        
        public ActionResult Login(USUARIOS _login)
        {

            try
            {
                TIPO_ROL Rol = new TIPO_ROL();
                using (ODAO Menu = new ODAO())
                {
                    ViewBag.Rol = new SelectList(Menu.TIPO_ROL.ToList(), "ID_ROL", "NOMBRE_ROL", "CATEGORIA", 1);
                }

                if (ModelState.IsValid) //validating the user inputs 
                {
                    string cualquira = Request["LoginType"];
                    string hash = ConfigurationManager.AppSettings["Encryption"];
                    bool isExist = false;

                    _login.TIMESTAMP = DateTime.Now;

                    _login.IP = System.Web.HttpContext.Current.Request.UserHostAddress;

                    if (cualquira != null)
                    {
                        switch (Request["LoginType"].ToString())
                        {
                            case ("Login"):
                                using (ODAO _entity = new ODAO())  // out Entity name is "SampleMenuMasterDBEntites"  
                                {
                                    string passEncriptado = Encrypt.EncryptString(_login.PASS, hash);
                                    isExist = _entity.USUARIOS.Where(x => x.USER.Trim().ToLower() == _login.USER.Trim().ToLower() && x.PASS.ToString() == passEncriptado.ToString()).Any(); //validating the user name in tblLogin table whether the user name is exist or not  
                                    if (isExist)
                                    {
                                        USUARIOS _loginCredentials = _entity.USUARIOS.Where(x => x.USER.Trim().ToLower() == _login.USER.Trim().ToLower()).FirstOrDefault();  // Get the login user details and bind it to LoginModels class  

                                        FormsAuthentication.SetAuthCookie(_loginCredentials.USER, false); // set the formauthentication cookie  
                                        Session["LoginCredentials"] = _loginCredentials; // Bind the _logincredentials details to "LoginCredentials" session  
                                        Session["MenuMaster"] = db.MENU.Include("MENU_SUB").Where(w => w.ID_ROL == _loginCredentials.ID_ROL).ToList(); //Bind the _menus list to MenuMaster session  
                                        Session["UserName"] = _loginCredentials.USER;
                                        Session["Binary_File"] = _login.BINARY_IMAGE;
                                        ViewBag.USUARIO_LOG = _loginCredentials;


                                        var asd = _entity.ASISTENTES.Where(x => x.ID_USUARIO == _loginCredentials.ID_USUARIO).FirstOrDefault();

                                        if (_entity.CLIENTES.Where(x => x.ID_USUARIO == _loginCredentials.ID_USUARIO).FirstOrDefault() == null && _loginCredentials.ID_ROL == 41 )
                                        {
                                            ViewBag.Message = "Debe Competar Su Perfil de Cliente"; // personas
                                            return RedirectToAction("CompletarPerfil", "CLIENTES");
                                        }
                                        else if (_entity.CLIENTES.Where(x => x.ID_USUARIO == _loginCredentials.ID_USUARIO).FirstOrDefault() == null && _loginCredentials.ID_ROL == 61)
                                        {
                                            ViewBag.Message = "Debe Competar Su Perfil de Cliente";// empresas
                                            return RedirectToAction("CompletarPerfil", "CLIENTES");
                                        }
                                        else if (_entity.ASISTENTES.Where(x => x.ID_USUARIO == _loginCredentials.ID_USUARIO).FirstOrDefault() == null && _loginCredentials.ID_ROL != 41 && _loginCredentials.ID_ROL != 61)
                                        {
                                            ViewBag.Message = "Debe Competar Su Perfil de ASISTENTE";// ASISTENTE
                                            return RedirectToAction("CompletarPerfil", "ASISTENTES");
                                        }
                                        else
                                        {
                                            if (_loginCredentials.ID_ROL == 41 | _loginCredentials.ID_ROL == 61)
                                            {
                                                Session["PerfilCliente"] = _entity.CLIENTES.Where(x => x.ID_USUARIO == _loginCredentials.ID_USUARIO).FirstOrDefault();
                                                return RedirectToAction("Index", "CLIENTES");
                                            }
                                            else
                                            {
                                                return RedirectToAction("Index", "ASISTENTES");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.Message = "Las credenciales no son validas!...";
                                        return View();
                                    }
                                }

                            case ("Register"):
                                using (ODAO _entity = new ODAO())
                                {
                                    isExist = _entity.USUARIOS.Where(x => x.USER.Trim().ToLower() == _login.USER.Trim().ToLower()).Any();
                                    if (isExist)
                                    {
                                        ViewBag.Message = "Este usuario ya existe en nuestros sistemas";
                                        return View();
                                    }
                                    else
                                    {
                                        HttpPostedFileBase File = Request.Files["IMG_PROFILE"];
                                        _login.IMG_PROFILE = File.FileName;
                                        _login.BINARY_IMAGE = ConvertToByte(File);
                                        _login.PASS = Encrypt.EncryptString(_login.PASS.ToString(), hash);
                                        db.USUARIOS.Add(_login);
                                        db.SaveChanges();

                                        return View();
                                    }
                                }
                            default:
                                return View();
                        }
                    }

                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            
        }

        

        public ActionResult Subir(USUARIOS _usuarioSubir)
        {
            HttpPostedFileBase File = Request.Files["IMG_PROFILE"];
            _usuarioSubir.IMG_PROFILE = File.FileName;
            _usuarioSubir.BINARY_IMAGE = ConvertToByte(Request.Files["IMG_PROFILE"]);
            db.USUARIOS.Add(_usuarioSubir);
            db.SaveChanges();

            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL");
            return View();
        }
        public byte[] ConvertToByte(HttpPostedFileBase file)
        {
            byte[] imageByte = null;
            BinaryReader rdr = new BinaryReader(file.InputStream);
            imageByte = rdr.ReadBytes((int)file.ContentLength);
            return imageByte;
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Login", "USUARIOS");
        }



        // GET: USUARIOS
        public ActionResult Index()
        {
            if(Session["LoginCredentials"] == null) {
                return RedirectToAction("Login", "USUARIOS");
            }
                ViewBag.USUARIOS = Session["LoginCredentials"];

            var uSUARIOS = db.USUARIOS.Include(u => u.ASISTENTES).Include(u => u.CLIENTES).Include(u => u.TIPO_ROL);
            return View(uSUARIOS.ToList());
        }

        // GET: USUARIOS/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Create
        public ActionResult Create()
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            ViewBag.Asistentes = new SelectList(db.ASISTENTES, "RUT", "NOMBRES");
            ViewBag.Clientes = new SelectList(db.CLIENTES, "RUT", "NOMBRE_RAZON_SOCIAL");
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL");
            return View();
        }

        // POST: USUARIOS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER,PASS,TIMESTAMP,ID_ROL,RUT,ID_USER")] USUARIOS uSUARIOS)
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            if (ModelState.IsValid)
            {
                db.USUARIOS.Add(uSUARIOS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RUT = new SelectList(db.ASISTENTES, "RUT", "NOMBRES", uSUARIOS.ASISTENTES);
            ViewBag.RUT = new SelectList(db.CLIENTES, "RUT", "NOMBRE_RAZON_SOCIAL", uSUARIOS.CLIENTES);
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Edit/5
        public ActionResult Edit(long? id)
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            ViewBag.RUT = new SelectList(db.ASISTENTES, "RUT", "NOMBRES", uSUARIOS.ASISTENTES);
            ViewBag.RUT = new SelectList(db.CLIENTES, "RUT", "NOMBRE_RAZON_SOCIAL", uSUARIOS.CLIENTES);
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER,PASS,TIMESTAMP,ID_ROL,RUT,ID_USER")] USUARIOS uSUARIOS)
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            if (ModelState.IsValid)
            {
                db.Entry(uSUARIOS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ASISTENTES = new SelectList(db.ASISTENTES, "RUT", "NOMBRES", uSUARIOS.ASISTENTES);
            ViewBag.CLIENTES = new SelectList(db.CLIENTES, "RUT", "NOMBRE_RAZON_SOCIAL", uSUARIOS.CLIENTES);
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", uSUARIOS.ID_ROL);
            return View(uSUARIOS);
        }

        // GET: USUARIOS/Delete/5
        public ActionResult Delete(long? id)
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            if (uSUARIOS == null)
            {
                return HttpNotFound();
            }
            return View(uSUARIOS);
        }

        // POST: USUARIOS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (Session["LoginCredentials"] == null) { Response.Redirect("/USUARIOS/Login"); };
            USUARIOS uSUARIOS = db.USUARIOS.Find(id);
            db.USUARIOS.Remove(uSUARIOS);
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

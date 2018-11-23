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
    public class MENUController : Controller
    {
        private ODAO db = new ODAO();

        // GET: MENU
        public ActionResult Index()
        {
            List<MENU> mENU = new List<MENU>();
            try
            {
                mENU = db.MENU.Include(m => m.TIPO_ROL).ToList();
                return View(mENU);
            }
            catch (Exception)
            {

                return View(mENU);
            }

        }

        // GET: MENU/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // GET: MENU/Create
        public ActionResult Create()
        {
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL");
            return View();
        }

        // POST: MENU/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MENU_ID,TEXTO,REFERENCIA,ID_ROL,ORDEN,CONTROLLER,ACTION")] MENU mENU)
        {
            if (ModelState.IsValid)
            {
                db.MENU.Add(mENU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU.ID_ROL);
            return View(mENU);
        }

        // GET: MENU/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU.ID_ROL);
            return View(mENU);
        }

        // POST: MENU/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MENU_ID,TEXTO,REFERENCIA,ID_ROL,ORDEN,CONTROLLER,ACTION")] MENU mENU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mENU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU.ID_ROL);
            return View(mENU);
        }

        // GET: MENU/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MENU mENU = db.MENU.Find(id);
            if (mENU == null)
            {
                return HttpNotFound();
            }
            return View(mENU);
        }

        // POST: MENU/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            MENU mENU = db.MENU.Find(id);
            db.MENU.Remove(mENU);
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

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
    public class MENU_SUBController : Controller
    {
        private ODAO db = new ODAO();

        // GET: MENU_SUB
        public ActionResult Index()
        {
            List<MENU_SUB> mENU_SUB = new List<MENU_SUB>();
            try
            {
                mENU_SUB = db.MENU_SUB.Include(m => m.MENU).Include(m => m.TIPO_ROL).ToList();
                return View(mENU_SUB);
            }
            catch (Exception)
            {

                return View(mENU_SUB);
            }

        }

        // GET: MENU_SUB/Details/5
        public ActionResult Details(long? id)
        {
            MENU_SUB mENU_SUB = new MENU_SUB();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mENU_SUB = db.MENU_SUB.Find(id);
                if (mENU_SUB == null)
                {
                    return HttpNotFound();
                }
                return View(mENU_SUB);
            }
            catch (Exception)
            {

                return View(mENU_SUB);
            }

        }

        // GET: MENU_SUB/Create
        public ActionResult Create()
        {
            try
            {
                using (ODAO Menu = new ODAO())
                {

                    ViewBag.SUB = new SelectList(Menu.MENU.Include("TIPO_ROL").ToList(), "MENU_ID", "TEXTO", "TIPO_ROL.NOMBRE_ROL", 1);
                }

                ViewBag.MENU_ID_P = new SelectList(db.MENU, "MENU_ID", "TEXTO", "");
                ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL");
                return View();
            }
            catch (Exception)
            {
                ViewBag.SUB = new SelectList(new List<string>());
                ViewBag.MENU_ID_P = new SelectList( new List<string>());
                ViewBag.ID_ROL = new SelectList(new List<string>());
                return View();
            }

        }

        // POST: MENU_SUB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MENU_ID,TEXTO,REFERENCIA,ID_ROL,MENU_ID_P,CRUD,CONTROLLER,ACTION")] MENU_SUB mENU_SUB)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MENU_SUB.Add(mENU_SUB);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MENU_ID_P = new SelectList(db.MENU, "MENU_ID", "TEXTO", mENU_SUB.MENU_ID_P);
                ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU_SUB.ID_ROL);
                return View(mENU_SUB);
            }
            catch (Exception)
            {

                return View(mENU_SUB);
            }

        }

        // GET: MENU_SUB/Edit/5
        public ActionResult Edit(long? id)
        {
            MENU_SUB mENU_SUB = new MENU_SUB();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mENU_SUB = db.MENU_SUB.Find(id);
                if (mENU_SUB == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MENU_ID_P = new SelectList(db.MENU, "MENU_ID", "TEXTO", mENU_SUB.MENU_ID_P);
                ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU_SUB.ID_ROL);
                return View(mENU_SUB);
            }
            catch (Exception)
            {
                ViewBag.MENU_ID_P = new SelectList(new List<string>());
                ViewBag.ID_ROL = new SelectList(new List<string>());
                return View(mENU_SUB);
            }
        }

        // POST: MENU_SUB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MENU_ID,TEXTO,REFERENCIA,ID_ROL,MENU_ID_P,CRUD,CONTROLLER,ACTION")] MENU_SUB mENU_SUB)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mENU_SUB).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MENU_ID_P = new SelectList(db.MENU, "MENU_ID", "TEXTO", mENU_SUB.MENU_ID_P);
                ViewBag.ID_ROL = new SelectList(db.TIPO_ROL, "ID_ROL", "NOMBRE_ROL", mENU_SUB.ID_ROL);
                return View(mENU_SUB);
            }
            catch (Exception)
            {
                ViewBag.MENU_ID_P = new SelectList(new List<string>());
                ViewBag.ID_ROL = new SelectList(new List<string>());
                return View(mENU_SUB);
            }

        }

        // GET: MENU_SUB/Delete/5
        public ActionResult Delete(long? id)
        {
            MENU_SUB mENU_SUB = new MENU_SUB();
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mENU_SUB = db.MENU_SUB.Find(id);
                if (mENU_SUB == null)
                {
                    return HttpNotFound();
                }
                return View(mENU_SUB);
            }
            catch (Exception)
            {

                return View(mENU_SUB);
            }

        }

        // POST: MENU_SUB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                if (id > 0)
                {
                    MENU_SUB mENU_SUB = db.MENU_SUB.Find(id);
                    db.MENU_SUB.Remove(mENU_SUB);
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

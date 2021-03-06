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
    public class PLAN_PAGOController : Controller
    {
        private ODAO db = new ODAO();

        // GET: PLAN_PAGO
        public ActionResult Index()
        {
            return View(db.PLAN_PAGO.ToList());
        }

        // GET: PLAN_PAGO/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN_PAGO pLAN_PAGO = db.PLAN_PAGO.Find(id);
            if (pLAN_PAGO == null)
            {
                return HttpNotFound();
            }
            return View(pLAN_PAGO);
        }

        // GET: PLAN_PAGO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PLAN_PAGO/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PAGO,PLAN,VALOR")] PLAN_PAGO pLAN_PAGO)
        {
            if (ModelState.IsValid)
            {
                db.PLAN_PAGO.Add(pLAN_PAGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pLAN_PAGO);
        }

        // GET: PLAN_PAGO/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN_PAGO pLAN_PAGO = db.PLAN_PAGO.Find(id);
            if (pLAN_PAGO == null)
            {
                return HttpNotFound();
            }
            return View(pLAN_PAGO);
        }

        // POST: PLAN_PAGO/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PAGO,PLAN,VALOR")] PLAN_PAGO pLAN_PAGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pLAN_PAGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pLAN_PAGO);
        }

        // GET: PLAN_PAGO/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PLAN_PAGO pLAN_PAGO = db.PLAN_PAGO.Find(id);
            if (pLAN_PAGO == null)
            {
                return HttpNotFound();
            }
            return View(pLAN_PAGO);
        }

        // POST: PLAN_PAGO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            PLAN_PAGO pLAN_PAGO = db.PLAN_PAGO.Find(id);
            db.PLAN_PAGO.Remove(pLAN_PAGO);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LexAbogadosWeb.Models;

namespace LexAbogadosWeb.Controllers
{
    public class TRIBUNAL_JUSTICIAController : ApiController
    {
        private ODAO db = new ODAO();

        // GET: api/TRIBUNAL_JUSTICIA
        public IQueryable<TRIBUNAL_JUSTICIA> GetTRIBUNAL_JUSTICIA()
        {
            return db.TRIBUNAL_JUSTICIA;
        }

        // GET: api/TRIBUNAL_JUSTICIA/5
        [ResponseType(typeof(TRIBUNAL_JUSTICIA))]
        public IHttpActionResult GetTRIBUNAL_JUSTICIA(long id)
        {
            TRIBUNAL_JUSTICIA tRIBUNAL_JUSTICIA = db.TRIBUNAL_JUSTICIA.Find(id);
            if (tRIBUNAL_JUSTICIA == null)
            {
                return NotFound();
            }

            return Ok(tRIBUNAL_JUSTICIA);
        }

        // PUT: api/TRIBUNAL_JUSTICIA/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTRIBUNAL_JUSTICIA(long id, TRIBUNAL_JUSTICIA tRIBUNAL_JUSTICIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tRIBUNAL_JUSTICIA.ID_REGISTRO)
            {
                return BadRequest();
            }

            db.Entry(tRIBUNAL_JUSTICIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRIBUNAL_JUSTICIAExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TRIBUNAL_JUSTICIA
        [ResponseType(typeof(TRIBUNAL_JUSTICIA))]
        public IHttpActionResult PostTRIBUNAL_JUSTICIA(TRIBUNAL_JUSTICIA tRIBUNAL_JUSTICIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tRIBUNAL_JUSTICIA.ESTADO = "Recepcionado";
            db.TRIBUNAL_JUSTICIA.Add(tRIBUNAL_JUSTICIA);
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TRIBUNAL_JUSTICIAExists(tRIBUNAL_JUSTICIA.ID_REGISTRO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtRoute("DefaultApi", new { id = tRIBUNAL_JUSTICIA.ID_REGISTRO }, tRIBUNAL_JUSTICIA);
        }

        // DELETE: api/TRIBUNAL_JUSTICIA/5
        [ResponseType(typeof(TRIBUNAL_JUSTICIA))]
        public IHttpActionResult DeleteTRIBUNAL_JUSTICIA(long id)
        {
            TRIBUNAL_JUSTICIA tRIBUNAL_JUSTICIA = db.TRIBUNAL_JUSTICIA.Find(id);
            if (tRIBUNAL_JUSTICIA == null)
            {
                return NotFound();
            }

            db.TRIBUNAL_JUSTICIA.Remove(tRIBUNAL_JUSTICIA);
            db.SaveChanges();

            return Ok(tRIBUNAL_JUSTICIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TRIBUNAL_JUSTICIAExists(long id)
        {
            return db.TRIBUNAL_JUSTICIA.Count(e => e.ID_REGISTRO == id) > 0;
        }
    }
}
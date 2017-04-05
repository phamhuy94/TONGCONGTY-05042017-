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
using ERP.Web.Models.Database;

namespace ERP.Web.Api.HeThong
{
    public class Api_ChungTuController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_ChungTu
        public List<DM_CHUNG_TU> GetDM_CHUNG_TU()
        {
            var vData = db.DM_CHUNG_TU;
            var result = vData.ToList().Select(x => new DM_CHUNG_TU()
            {
                MA_CHUNG_TU = x.MA_CHUNG_TU,
                TEN_CHUNG_TU = x.TEN_CHUNG_TU,
                MA_LOAI_CHUNG_TU = x.MA_LOAI_CHUNG_TU,
                MA_CONG_TY = x.MA_CONG_TY,
            }).ToList();
            return result;
        }

        // GET: api/Api_ChungTu/5
        [ResponseType(typeof(DM_CHUNG_TU))]
        public IHttpActionResult GetDM_CHUNG_TU(string id)
        {
            DM_CHUNG_TU dM_CHUNG_TU = db.DM_CHUNG_TU.Find(id);
            if (dM_CHUNG_TU == null)
            {
                return NotFound();
            }

            return Ok(dM_CHUNG_TU);
        }

        // PUT: api/Api_ChungTu/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDM_CHUNG_TU(string id, DM_CHUNG_TU dM_CHUNG_TU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dM_CHUNG_TU.MA_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(dM_CHUNG_TU).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DM_CHUNG_TUExists(id))
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

        // POST: api/Api_ChungTu
        [ResponseType(typeof(DM_CHUNG_TU))]
        public IHttpActionResult PostDM_CHUNG_TU(DM_CHUNG_TU dM_CHUNG_TU)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DM_CHUNG_TU.Add(dM_CHUNG_TU);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DM_CHUNG_TUExists(dM_CHUNG_TU.MA_CHUNG_TU))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dM_CHUNG_TU.MA_CHUNG_TU }, dM_CHUNG_TU);
        }

        // DELETE: api/Api_ChungTu/5
        [ResponseType(typeof(DM_CHUNG_TU))]
        public IHttpActionResult DeleteDM_CHUNG_TU(string id)
        {
            DM_CHUNG_TU dM_CHUNG_TU = db.DM_CHUNG_TU.Find(id);
            if (dM_CHUNG_TU == null)
            {
                return NotFound();
            }

            db.DM_CHUNG_TU.Remove(dM_CHUNG_TU);
            db.SaveChanges();

            return Ok(dM_CHUNG_TU);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DM_CHUNG_TUExists(string id)
        {
            return db.DM_CHUNG_TU.Count(e => e.MA_CHUNG_TU == id) > 0;
        }
    }
}
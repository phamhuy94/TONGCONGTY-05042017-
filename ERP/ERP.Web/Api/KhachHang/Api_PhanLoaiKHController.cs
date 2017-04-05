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

namespace ERP.Web.Api.KhachHang
{
    public class Api_PhanLoaiKHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_PhanLoaiKH
        public IQueryable<KH_PHAN_LOAI_KHACH> GetKH_PHAN_LOAI_KHACH()
        {
            return db.KH_PHAN_LOAI_KHACH;
        }

        // GET: api/Api_PhanLoaiKH/5
        [ResponseType(typeof(KH_PHAN_LOAI_KHACH))]
        public IHttpActionResult GetKH_PHAN_LOAI_KHACH(int id)
        {
            KH_PHAN_LOAI_KHACH kH_PHAN_LOAI_KHACH = db.KH_PHAN_LOAI_KHACH.Find(id);
            if (kH_PHAN_LOAI_KHACH == null)
            {
                return NotFound();
            }

            return Ok(kH_PHAN_LOAI_KHACH);
        }

        // PUT: api/Api_PhanLoaiKH/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKH_PHAN_LOAI_KHACH(int id, KH_PHAN_LOAI_KHACH kH_PHAN_LOAI_KHACH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != kH_PHAN_LOAI_KHACH.ID)
            {
                return BadRequest();
            }

            db.Entry(kH_PHAN_LOAI_KHACH).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KH_PHAN_LOAI_KHACHExists(id))
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

        // POST: api/Api_PhanLoaiKH
        [ResponseType(typeof(KH_PHAN_LOAI_KHACH))]
        public IHttpActionResult PostKH_PHAN_LOAI_KHACH(KH_PHAN_LOAI_KHACH kH_PHAN_LOAI_KHACH)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.KH_PHAN_LOAI_KHACH.Add(kH_PHAN_LOAI_KHACH);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = kH_PHAN_LOAI_KHACH.ID }, kH_PHAN_LOAI_KHACH);
        }

        // DELETE: api/Api_PhanLoaiKH/5
        [ResponseType(typeof(KH_PHAN_LOAI_KHACH))]
        public IHttpActionResult DeleteKH_PHAN_LOAI_KHACH(int id)
        {
            KH_PHAN_LOAI_KHACH kH_PHAN_LOAI_KHACH = db.KH_PHAN_LOAI_KHACH.Find(id);
            if (kH_PHAN_LOAI_KHACH == null)
            {
                return NotFound();
            }

            db.KH_PHAN_LOAI_KHACH.Remove(kH_PHAN_LOAI_KHACH);
            db.SaveChanges();

            return Ok(kH_PHAN_LOAI_KHACH);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KH_PHAN_LOAI_KHACHExists(int id)
        {
            return db.KH_PHAN_LOAI_KHACH.Count(e => e.ID == id) > 0;
        }
    }
}
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

namespace ERP.Web.Api.BaoGia
{
    public class Api_BaoGia_DonHangDuKienController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_BaoGia_DonHangDuKien
        public List<BH_DON_HANG_DU_KIEN> GetBH_DON_HANG_DU_KIEN()
        {
            var vData = db.BH_DON_HANG_DU_KIEN.Where(x => x.TRUC_THUOC == "HOPLONG");
            var result = vData.ToList().Select(x => new BH_DON_HANG_DU_KIEN()
            {
                MA_DU_KIEN = x.MA_DU_KIEN,
                MA_KHACH_HANG = x.MA_KHACH_HANG,
                NGAY_TAO = x.NGAY_TAO,
                THANH_CONG = x.THANH_CONG,
                THAT_BAI = x.THAT_BAI,
                LY_DO_THAT_BAI = x.LY_DO_THAT_BAI,
                TRUC_THUOC = x.TRUC_THUOC,
            }).ToList();
            return result;
        }

        // GET: api/Api_BaoGia_DonHangDuKien/5
        [ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        public IHttpActionResult GetBH_DON_HANG_DU_KIEN(string id)
        {
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return NotFound();
            }

            return Ok(bH_DON_HANG_DU_KIEN);
        }

        // PUT: api/Api_BaoGia_DonHangDuKien/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_DON_HANG_DU_KIEN(string id, BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_DON_HANG_DU_KIEN.MA_DU_KIEN)
            {
                return BadRequest();
            }

            db.Entry(bH_DON_HANG_DU_KIEN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_DON_HANG_DU_KIENExists(id))
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

        // POST: api/Api_BaoGia_DonHangDuKien
        [ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        public IHttpActionResult PostBH_DON_HANG_DU_KIEN(BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.BH_DON_HANG_DU_KIEN.Add(bH_DON_HANG_DU_KIEN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BH_DON_HANG_DU_KIENExists(bH_DON_HANG_DU_KIEN.MA_DU_KIEN))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = bH_DON_HANG_DU_KIEN.MA_DU_KIEN }, bH_DON_HANG_DU_KIEN);
        }

        // DELETE: api/Api_BaoGia_DonHangDuKien/5
        [ResponseType(typeof(BH_DON_HANG_DU_KIEN))]
        public IHttpActionResult DeleteBH_DON_HANG_DU_KIEN(string id)
        {
            BH_DON_HANG_DU_KIEN bH_DON_HANG_DU_KIEN = db.BH_DON_HANG_DU_KIEN.Find(id);
            if (bH_DON_HANG_DU_KIEN == null)
            {
                return NotFound();
            }

            db.BH_DON_HANG_DU_KIEN.Remove(bH_DON_HANG_DU_KIEN);
            db.SaveChanges();

            return Ok(bH_DON_HANG_DU_KIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_DON_HANG_DU_KIENExists(string id)
        {
            return db.BH_DON_HANG_DU_KIEN.Count(e => e.MA_DU_KIEN == id) > 0;
        }
    }
}
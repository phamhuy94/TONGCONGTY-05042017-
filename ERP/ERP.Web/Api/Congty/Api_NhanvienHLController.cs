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
using ERP.Web.Models.NewModels;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Areas.HopLong.Api.HeThong
{
    public class Api_NhanvienHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();

        // GET: api/Api_NhanvienHL
        public List<CCTC_NHAN_VIEN> GetCCTC_NHAN_VIEN(string id)
        {
            var vData = db.CCTC_NHAN_VIEN.Where(x => x.USERNAME == id);
            var result = vData.ToList().Select(x => new CCTC_NHAN_VIEN()
            {
                USERNAME = x.USERNAME,
                GIOI_TINH = x.GIOI_TINH,
                NGAY_SINH = x.NGAY_SINH,
                QUE_QUAN = x.QUE_QUAN,
                THANH_TICH_CONG_TAC = x.THANH_TICH_CONG_TAC,
                TRINH_DO_HOC_VAN = x.TRINH_DO_HOC_VAN,
                MA_PHONG_BAN = x.MA_PHONG_BAN,
            }).ToList();
            return result;
        }

        // GET: api/Api_NhanvienHL/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult GetCCTC_NHAN_VIEN()
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find();
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            return Ok(cCTC_NHAN_VIEN);
        }

        // PUT: api/Api_NhanvienHL/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCCTC_NHAN_VIEN(string id, NHAN_VIEN_MODEL nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nhanvien.USERNAME)
            {
                return BadRequest();
            }
            CCTC_NHAN_VIEN nv = new CCTC_NHAN_VIEN();
            nv.USERNAME = nhanvien.USERNAME;
            nv.GIOI_TINH = nhanvien.GIOI_TINH;
            if (nhanvien.NGAY_SINH != null)
            nv.NGAY_SINH = xlnt.Xulydatetime(nhanvien.NGAY_SINH);
            nv.QUE_QUAN = nhanvien.QUE_QUAN;
            nv.THANH_TICH_CONG_TAC = nhanvien.THANH_TICH_CONG_TAC;
            nv.TRINH_DO_HOC_VAN = nhanvien.TRINH_DO_HOC_VAN;
            nv.MA_PHONG_BAN = nhanvien.MA_PHONG_BAN;
            db.Entry(nv).State = EntityState.Modified;
            


            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CCTC_NHAN_VIENExists(id))
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

        // POST: api/Api_NhanvienHL
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult PostCCTC_NHAN_VIEN(NHAN_VIEN_MODEL nhanvien)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CCTC_NHAN_VIEN nv = new CCTC_NHAN_VIEN();
            nv.USERNAME = nhanvien.USERNAME;
            nv.GIOI_TINH = nhanvien.GIOI_TINH;
            if (nhanvien.NGAY_SINH != null)
                nv.NGAY_SINH = xlnt.Xulydatetime(nhanvien.NGAY_SINH);
            nv.QUE_QUAN = nhanvien.QUE_QUAN;
            nv.THANH_TICH_CONG_TAC = nhanvien.THANH_TICH_CONG_TAC;
            nv.TRINH_DO_HOC_VAN = nhanvien.TRINH_DO_HOC_VAN;
            nv.MA_PHONG_BAN = nhanvien.MA_PHONG_BAN;

            db.CCTC_NHAN_VIEN.Add(nv);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CCTC_NHAN_VIENExists(nv.USERNAME))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = nv.USERNAME }, nv);
        }

        // DELETE: api/Api_NhanvienHL/5
        [ResponseType(typeof(CCTC_NHAN_VIEN))]
        public IHttpActionResult DeleteCCTC_NHAN_VIEN(string id)
        {
            CCTC_NHAN_VIEN cCTC_NHAN_VIEN = db.CCTC_NHAN_VIEN.Find(id);
            if (cCTC_NHAN_VIEN == null)
            {
                return NotFound();
            }

            db.CCTC_NHAN_VIEN.Remove(cCTC_NHAN_VIEN);
            db.SaveChanges();

            return Ok(cCTC_NHAN_VIEN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CCTC_NHAN_VIENExists(string id)
        {
            return db.CCTC_NHAN_VIEN.Count(e => e.USERNAME == id) > 0;
        }
    }
}
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
using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.NewModels;
using System.Web.Http.Results;
using System.Data.SqlClient;

namespace ERP.Web.Api.BaoGia
{
    public class Api_ChiTietBaoGiaController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        // GET: api/Api_ChiTietBaoGia
        [Route("api/Api_ChiTietBaoGia/CT_BAO_GIA/{so_bao_gia}")]
        public List<GetAll_ChiTietBaoGia_Result> CT_BAO_GIA(string so_bao_gia)
        {
            var query = db.Database.SqlQuery<GetAll_ChiTietBaoGia_Result>("GetAll_ChiTietBaoGia  @so_bao_gia, @ma_cong_ty", new SqlParameter("so_bao_gia", so_bao_gia),new SqlParameter("ma_cong_ty", "HOPLONG"));
            var result = query.ToList();
            return result;
        }

        // GET: api/Api_ChiTietBaoGia/5
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult GetBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA bH_CT_BAO_GIA = db.BH_CT_BAO_GIA.Find(id);
            if (bH_CT_BAO_GIA == null)
            {
                return NotFound();
            }

            return Ok(bH_CT_BAO_GIA);
        }

        // PUT: api/Api_ChiTietBaoGia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBH_CT_BAO_GIA(int id, BH_CT_BAO_GIA bH_CT_BAO_GIA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bH_CT_BAO_GIA.ID)
            {
                return BadRequest();
            }

            db.Entry(bH_CT_BAO_GIA).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BH_CT_BAO_GIAExists(id))
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

        // POST: api/Api_ChiTietBaoGia
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public void PostKH_LIEN_HE(List<BH_CT_BAO_GIA> lh)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }
            foreach (var item in lh)
            {
                BH_CT_BAO_GIA lienhe = new BH_CT_BAO_GIA();
                lienhe.SO_BAO_GIA = item.SO_BAO_GIA;
                lienhe.MA_HANG = item.MA_HANG;
                lienhe.SO_LUONG = item.SO_LUONG;
                lienhe.DON_GIA = item.DON_GIA;
                lienhe.CHIET_KHAU = item.CHIET_KHAU;
                lienhe.CACH_TINH_THANH_TIEN = item.CACH_TINH_THANH_TIEN;
                lienhe.THANH_TIEN = item.THANH_TIEN;
                lienhe.CK_VAT = item.CK_VAT;
                lienhe.TIEN_VAT = item.TIEN_VAT;
                lienhe.TINH_TRANG_HANG = item.TINH_TRANG_HANG;
                lienhe.THOI_GIAN_GIAO_HANG = item.THOI_GIAN_GIAO_HANG;
                lienhe.NGAY_GIAO_HANG = xlnt.Xulydatetime(item.NGAY_GIAO_HANG.ToString("dd/MM/yyyy"));
                lienhe.DIA_DIEM_GIAO_HANG = item.DIA_DIEM_GIAO_HANG;
                lienhe.GHI_CHU = item.GHI_CHU;
                db.BH_CT_BAO_GIA.Add(lienhe);
                db.SaveChanges();             
            }
        }

        // DELETE: api/Api_ChiTietBaoGia/5
        [ResponseType(typeof(BH_CT_BAO_GIA))]
        public IHttpActionResult DeleteBH_CT_BAO_GIA(int id)
        {
            BH_CT_BAO_GIA bH_CT_BAO_GIA = db.BH_CT_BAO_GIA.Find(id);
            if (bH_CT_BAO_GIA == null)
            {
                return NotFound();
            }

            db.BH_CT_BAO_GIA.Remove(bH_CT_BAO_GIA);
            db.SaveChanges();

            return Ok(bH_CT_BAO_GIA);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BH_CT_BAO_GIAExists(int id)
        {
            return db.BH_CT_BAO_GIA.Count(e => e.ID == id) > 0;
        }
    }
}
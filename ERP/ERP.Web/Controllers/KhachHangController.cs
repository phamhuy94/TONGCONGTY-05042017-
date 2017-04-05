using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.Web.Models.Database;
using System.IO;
using OfficeOpenXml;
using ERP.Web.Models.BusinessModel;

namespace ERP.Web.Controllers
{
    public class KhachHangController : Controller
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xulydate = new XuLyNgayThang();
        int so_dong_thanh_cong;
        int dong;
        // GET: KhachHang
        public ActionResult Index()
        {
            var kHs = db.KHs.Include(k => k.CCTC_CONG_TY);
            return View(kHs.ToList());
        }

        #region "Import KHÁCH HÀNG"

        public ActionResult Import_KhachHang()
        {
           
            return View();
        }



        


[HttpPost]
        public ActionResult Import_KhachHang(HttpPostedFileBase file)
        {
            try
            {
                if (Request != null)
                {
                    HttpPostedFileBase filetonkho = Request.Files["UploadedFile"];
                    if ((filetonkho != null) && (filetonkho.ContentLength > 0) && !string.IsNullOrEmpty(filetonkho.FileName))
                    {
                        string fileName = filetonkho.FileName;
                        string fileContentType = filetonkho.ContentType;
                        byte[] fileBytes = new byte[filetonkho.ContentLength];
                        var data = filetonkho.InputStream.Read(fileBytes, 0, Convert.ToInt32(filetonkho.ContentLength));
                        //var usersList = new List<Users>();
                        using (var package = new ExcelPackage(filetonkho.InputStream))
                        {
                            var currentSheet = package.Workbook.Worksheets;
                            var workSheet = currentSheet.First();
                            var noOfCol = workSheet.Dimension.End.Column;
                            var noOfRow = workSheet.Dimension.End.Row;
                            for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                            {


                                string makhach = workSheet.Cells[rowIterator, 1].Value.ToString();
                                var query = db.KHs.Where(x => x.MA_KHACH_HANG == makhach).FirstOrDefault();
                                if (query == null)
                                {
                                    KH khachhang = new KH();
                                    khachhang.MA_KHACH_HANG = makhach;
                                    khachhang.TEN_CONG_TY = workSheet.Cells[rowIterator, 2].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 5].Value != null)
                                        khachhang.VAN_PHONG_GIAO_DICH = workSheet.Cells[rowIterator, 5].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 6].Value != null)
                                        khachhang.DIA_CHI_XUAT_HOA_DON = workSheet.Cells[rowIterator, 6].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 7].Value != null)
                                        khachhang.MST = workSheet.Cells[rowIterator, 7].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 8].Value != null)
                                        khachhang.HOTLINE = workSheet.Cells[rowIterator, 8].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 9].Value != null)
                                        khachhang.FAX = workSheet.Cells[rowIterator, 9].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 10].Value != null)
                                        khachhang.EMAIL = workSheet.Cells[rowIterator, 10].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 11].Value != null)
                                        khachhang.LOGO = workSheet.Cells[rowIterator, 11].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 12].Value != null)
                                        khachhang.WEBSITE = workSheet.Cells[rowIterator, 12].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 13].Value != null)
                                        khachhang.TINH = workSheet.Cells[rowIterator, 13].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 14].Value != null)
                                        khachhang.QUOC_GIA = workSheet.Cells[rowIterator, 14].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 15].Value != null)
                                        khachhang.DIEU_KHOAN_THANH_TOAN = workSheet.Cells[rowIterator, 15].Value.ToString();
                                    if (workSheet.Cells[rowIterator, 16].Value != null)
                                        khachhang.SO_NGAY_DUOC_NO = Convert.ToInt32(workSheet.Cells[rowIterator, 16].Value);
                                    if (workSheet.Cells[rowIterator, 17].Value != null)
                                        khachhang.SO_NO_TOI_DA = Convert.ToInt32(workSheet.Cells[rowIterator, 17].Value);
                                    khachhang.TRUC_THUOC = "HOPLONG";
                                    if (workSheet.Cells[rowIterator, 19].Value != null)
                                        khachhang.GHI_CHU = workSheet.Cells[rowIterator, 19].Value.ToString();

                                    db.KHs.Add(khachhang);
                                    db.SaveChanges();

                                    var DATA = db.KHs.Where(x => x.MA_KHACH_HANG == makhach).FirstOrDefault();
                                    if(DATA !=null)
                                    {
                                        KH_PHAN_LOAI_KHACH plkhach = new KH_PHAN_LOAI_KHACH();
                                        plkhach.MA_KHACH_HANG = makhach;
                                        plkhach.MA_LOAI_KHACH = workSheet.Cells[rowIterator, 3].Value.ToString();
                                        if(workSheet.Cells[rowIterator, 4].Value != null)
                                            plkhach.NHOM_NGANH = workSheet.Cells[rowIterator, 4].Value.ToString();
                                        db.KH_PHAN_LOAI_KHACH.Add(plkhach);
                                        db.SaveChanges();
                                    }
                                   
                                    if (workSheet.Cells[rowIterator, 20].Value != null)
                                    {
                                        KH_LIEN_HE lhkhach = new KH_LIEN_HE();
                                        lhkhach.MA_KHACH_HANG = makhach;
                                        lhkhach.NGUOI_LIEN_HE = workSheet.Cells[rowIterator, 20].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                            lhkhach.CHUC_VU = workSheet.Cells[rowIterator, 21].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                            lhkhach.PHONG_BAN = workSheet.Cells[rowIterator, 22].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                            lhkhach.NGAY_SINH = xulydate.Xulydatetime(workSheet.Cells[rowIterator, 23].Value.ToString());
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                            lhkhach.GIOI_TINH = workSheet.Cells[rowIterator, 24].Value.ToString();
                                        lhkhach.SDT1 = workSheet.Cells[rowIterator, 25].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                            lhkhach.SDT2 = workSheet.Cells[rowIterator, 26].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                            lhkhach.EMAIL_CA_NHAN = workSheet.Cells[rowIterator, 27].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                            lhkhach.EMAIL_CONG_TY = workSheet.Cells[rowIterator, 28].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                            lhkhach.SKYPE = workSheet.Cells[rowIterator, 29].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                            lhkhach.FACEBOOK = workSheet.Cells[rowIterator, 30].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                            lhkhach.GHI_CHU = workSheet.Cells[rowIterator, 31].Value.ToString();
                                        db.KH_LIEN_HE.Add(lhkhach);
                                        db.SaveChanges();


                                        string idlh = workSheet.Cells[rowIterator, 25].Value.ToString();

                                        var datalienhe = db.KH_LIEN_HE.Where(x => x.SDT1 == idlh).FirstOrDefault();
                                        if (datalienhe != null)
                                        {
                                            KH_SALES_PHU_TRACH salept = new KH_SALES_PHU_TRACH();
                                            salept.ID_LIEN_HE = datalienhe.ID_LIEN_HE;
                                            salept.SALES_PHU_TRACH = workSheet.Cells[rowIterator, 32].Value.ToString();
                                            salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                                            salept.TRANG_THAI = true;
                                            db.KH_SALES_PHU_TRACH.Add(salept);
                                            db.SaveChanges();
                                        }
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            KH_TK_NGAN_HANG tkkhach = new KH_TK_NGAN_HANG();
                                            tkkhach.MA_KHACH_HANG = makhach;
                                            tkkhach.SO_TAI_KHOAN = workSheet.Cells[rowIterator, 33].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 34].Value != null)
                                                tkkhach.TEN_TAI_KHOAN = workSheet.Cells[rowIterator, 34].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 35].Value != null)
                                                tkkhach.TEN_NGAN_HANG = workSheet.Cells[rowIterator, 35].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 36].Value != null)
                                                tkkhach.CHI_NHANH = workSheet.Cells[rowIterator, 36].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 37].Value != null)
                                                tkkhach.TINH_TP = workSheet.Cells[rowIterator, 37].Value.ToString();

                                            if (workSheet.Cells[rowIterator, 38].Value != null)
                                                tkkhach.LOAI_TAI_KHOAN = workSheet.Cells[rowIterator, 38].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 39].Value != null)
                                                tkkhach.GHI_CHU = workSheet.Cells[rowIterator, 39].Value.ToString();

                                            db.KH_TK_NGAN_HANG.Add(tkkhach);
                                            db.SaveChanges();


                                        }

                                    }
                                }
                                else
                                if (query != null)
                                {
                                    if (workSheet.Cells[rowIterator, 20].Value != null)
                                    {
                                        KH_LIEN_HE lhkhach = new KH_LIEN_HE();
                                        lhkhach.MA_KHACH_HANG = makhach;
                                        lhkhach.NGUOI_LIEN_HE = workSheet.Cells[rowIterator, 20].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 21].Value != null)
                                            lhkhach.CHUC_VU = workSheet.Cells[rowIterator, 21].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 22].Value != null)
                                            lhkhach.PHONG_BAN = workSheet.Cells[rowIterator, 22].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 23].Value != null)
                                            lhkhach.NGAY_SINH = xulydate.Xulydatetime(workSheet.Cells[rowIterator, 23].Value.ToString());
                                        if (workSheet.Cells[rowIterator, 24].Value != null)
                                            lhkhach.GIOI_TINH = workSheet.Cells[rowIterator, 24].Value.ToString();
                                        lhkhach.SDT1 = workSheet.Cells[rowIterator, 25].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 26].Value != null)
                                            lhkhach.SDT2 = workSheet.Cells[rowIterator, 26].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 27].Value != null)
                                            lhkhach.EMAIL_CA_NHAN = workSheet.Cells[rowIterator, 27].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 28].Value != null)
                                            lhkhach.EMAIL_CONG_TY = workSheet.Cells[rowIterator, 28].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 29].Value != null)
                                            lhkhach.SKYPE = workSheet.Cells[rowIterator, 29].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 30].Value != null)
                                            lhkhach.FACEBOOK = workSheet.Cells[rowIterator, 30].Value.ToString();
                                        if (workSheet.Cells[rowIterator, 31].Value != null)
                                            lhkhach.GHI_CHU = workSheet.Cells[rowIterator, 31].Value.ToString();
                                        db.KH_LIEN_HE.Add(lhkhach);
                                        db.SaveChanges();

                                        string idlh = workSheet.Cells[rowIterator, 25].Value.ToString();

                                        var datalienhe = db.KH_LIEN_HE.Where(x => x.SDT1 == idlh).FirstOrDefault();
                                        if (datalienhe != null)
                                        {
                                            KH_SALES_PHU_TRACH salept = new KH_SALES_PHU_TRACH();
                                            salept.ID_LIEN_HE = datalienhe.ID_LIEN_HE;
                                            salept.SALES_PHU_TRACH = workSheet.Cells[rowIterator, 32].Value.ToString();
                                            salept.NGAY_BAT_DAU_PHU_TRACH = DateTime.Today.Date;
                                            salept.TRANG_THAI = true;
                                            db.KH_SALES_PHU_TRACH.Add(salept);
                                            db.SaveChanges();
                                        }
                                        if (workSheet.Cells[rowIterator, 33].Value != null)
                                        {
                                            KH_TK_NGAN_HANG tkkhach = new KH_TK_NGAN_HANG();
                                            tkkhach.MA_KHACH_HANG = makhach;
                                            tkkhach.SO_TAI_KHOAN = workSheet.Cells[rowIterator, 33].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 34].Value != null)
                                                tkkhach.TEN_TAI_KHOAN = workSheet.Cells[rowIterator, 34].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 35].Value != null)
                                                tkkhach.TEN_NGAN_HANG = workSheet.Cells[rowIterator, 35].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 36].Value != null)
                                                tkkhach.CHI_NHANH = workSheet.Cells[rowIterator, 36].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 37].Value != null)
                                                tkkhach.TINH_TP = workSheet.Cells[rowIterator, 37].Value.ToString();

                                            if (workSheet.Cells[rowIterator, 38].Value != null)
                                                tkkhach.LOAI_TAI_KHOAN = workSheet.Cells[rowIterator, 38].Value.ToString();
                                            if (workSheet.Cells[rowIterator, 39].Value != null)
                                                tkkhach.GHI_CHU = workSheet.Cells[rowIterator, 39].Value.ToString();

                                            db.KH_TK_NGAN_HANG.Add(tkkhach);
                                            db.SaveChanges();


                                        }
                                    

                                    
                                    }

                                }

                                so_dong_thanh_cong++;
                                dong = rowIterator;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ViewBag.Error = " Đã xảy ra lỗi, Liên hệ ngay với admin. " + Environment.NewLine + " Thông tin chi tiết về lỗi:" + Environment.NewLine + Ex;
                ViewBag.Information = "Lỗi tại dòng thứ: " + dong;

            }
            finally
            {
                ViewBag.Message = "Đã import thành công " + so_dong_thanh_cong + " dòng";
            }

            return View();
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Index(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    // Verify that the user selected a file
                    if (file != null && file.ContentLength > 0)
                    {
                        // extract only the fielname
                        var fileName = Path.GetFileName(file.FileName);
                        // TODO: need to define destination
                        var path = Path.Combine(Server.MapPath("~/Content/Images/KhachHang"), fileName);
                        file.SaveAs(path);
                    }
                }
            }
        }

    }
}

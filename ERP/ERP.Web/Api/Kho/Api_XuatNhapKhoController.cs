using ERP.Web.Models.BusinessModel;
using ERP.Web.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.Kho
{
    public class Api_XuatNhapKhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        XuLyNgayThang xlnt = new XuLyNgayThang();
        List<Search_SearchByType_Result> result = new List<Search_SearchByType_Result>();
        List<GetChungTuFromDoiTuong_Result> resultDoiTuong = new List<GetChungTuFromDoiTuong_Result>();
        List<GetAllChungTu_Result> resultAllMa = new List<GetAllChungTu_Result>();

        #region "SearchByType"

        [Route("api/Api_XuatNhapKho/SearchByType/{GiaTriChungTu}/{FromTime}/{ToTime}")]
        public List<Search_SearchByType_Result> SearchByType(String GiaTriChungTu, string FromTime, String ToTime)
        {

            if (FromTime == "" && ToTime == "")
            {
                var query = db.Database.SqlQuery<Search_SearchByType_Result>("Search_SearchByType @LoaiChungTu,@macongty", new SqlParameter("LoaiChungTu", GiaTriChungTu), new SqlParameter("macongty", "HOPLONG"));
                result = query.ToList();
            }


            if ((FromTime != "" && ToTime != "") || (FromTime != "" && ToTime != ""))
            {
                DateTime FromDate = xlnt.Xulydatetime(FromTime);
                DateTime ToDate = xlnt.Xulydatetime(ToTime);
                var query = db.Database.SqlQuery<Search_SearchByType_Result>("Search_SearchByTypeWithDate @LoaiChungTu,@FromDate,@ToDate, @macongty", new SqlParameter("LoaiChungTu", GiaTriChungTu), new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("macongty", "HOPLONG"));
                result = query.ToList();
            }

            return result;
        }

        #endregion


        #region "Search by Object"

        [Route("api/Api_XuatNhapKho/SearchByDoiTuong/{doituong}/{FromTime}/{ToTime}")]
        public List<GetChungTuFromDoiTuong_Result> SearchByDoiTuong(String doituong, string FromTime, String ToTime)
        {

            if (FromTime == "" && ToTime == "")
            {
                var query = db.Database.SqlQuery<GetChungTuFromDoiTuong_Result>("GetChungTuFromDoiTuong @MaDoiTuong,@macongty", new SqlParameter("MaDoiTuong", doituong), new SqlParameter("macongty", "HOPLONG"));
                resultDoiTuong = query.ToList();
            }


            if (FromTime != "" && ToTime != "")
            {
                DateTime FromDate = xlnt.Xulydatetime(FromTime);
                DateTime ToDate = xlnt.Xulydatetime(ToTime);
                var query = db.Database.SqlQuery<GetChungTuFromDoiTuong_Result>("GetChungTuFromDoiTuong_WithDate @MaDoiTuong,@FromDate,@ToDate, @macongty", new SqlParameter("MaDoiTuong", doituong), new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("macongty", "HOPLONG"));
                resultDoiTuong = query.ToList();
            }

            return resultDoiTuong;
        }
        #endregion


        #region "Get All Chung Tu"

        [Route("api/Api_XuatNhapKho/SearchAllMa/{FromTime}/{ToTime}")]
        public List<GetAllChungTu_Result> SearchAllMa(string FromTime, String ToTime)
        {

            if (FromTime == "A" && ToTime == "A")
            {
                var query = db.Database.SqlQuery<GetAllChungTu_Result>("GetAllChungTu @macongty", new SqlParameter("macongty", "HOPLONG"));
                resultAllMa = query.ToList();
            }


            if ((FromTime != "A" && ToTime != "A") || (FromTime != "" && ToTime != ""))
            {
                DateTime FromDate = xlnt.Xulydatetime(FromTime);
                DateTime ToDate = xlnt.Xulydatetime(ToTime);
                var query = db.Database.SqlQuery<GetAllChungTu_Result>("GetAllChungTu_WithDate @FromDate,@ToDate, @macongty",new SqlParameter("FromDate", FromDate), new SqlParameter("ToDate", ToDate), new SqlParameter("macongty", "HOPLONG"));
                resultAllMa = query.ToList();
            }

            return resultAllMa;
        }
        #endregion


        #region "Get All Doi Tuong"

        [Route("api/Api_XuatNhapKho/GetAllDoiTuong")]
        public List<GetAllDoiTuong_Result> GetAllDoiTuong()
        {

           
                var query = db.Database.SqlQuery<GetAllDoiTuong_Result>("GetAllDoiTuong @macongty", new SqlParameter("macongty", "HOPLONG"));
            var    resultAllDT = query.ToList();
            
            return resultAllDT;
        }
        #endregion
    }

}
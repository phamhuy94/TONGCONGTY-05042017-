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

namespace ERP.Web.Areas.HopLong.Api.Kho
{
    public class Api_TonKhoHLController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_TonKhoHL
        //public List<tonkhohanghoa> GetHH_TON_KHO(string id)
        //{
        //    var query = db.Database.SqlQuery<tonkhohanghoa>("PROD_HANGHOA");
        //    var result = query.ToList().Select(x => new tonkhohanghoa()
        //    {
        //        mahang = x.mahang,
        //        tonkhohoplong = x.tonkhohoplong,
        //        tonkhoDN = x.tonkhoDN,
        //        tonkhoHCM = x.tonkhoHCM,
        //        tonkhoHP = x.tonkhoHP
        //    }).ToList();

        //    //}).ToList();
        //    return result;
        //    //List<tonkhohanghoa> listtonkho = new List<tonkhohanghoa>();
        //    //#region "comment"
        //    //// var dskho = db.DM_KHO.Where(x => x.TRUC_THUOC == "HOPLONG").ToList();
        //    ////var dskho = db.DM_KHO.ToList();
        //    ////foreach (var item in dskho)
        //    ////{
        //    ////    var vData = db.HH_TON_KHO.Where(x => x.MA_HANG == id && x.MA_KHO == item.MA_KHO);
        //    ////    if(vData.Count() >0)
        //    ////    {
        //    ////        var data = vData.FirstOrDefault();
        //    ////        HH_TON_KHO tonkho = new HH_TON_KHO();
        //    ////        tonkho.MA_HANG = data.MA_HANG;
        //    ////        tonkho.MA_KHO = data.MA_KHO;
        //    ////        tonkho.SL_TON = data.SL_TON;
        //    ////        listtonkho.Add(tonkho); 
        //    ////    }
        //    ////}
        //    ////var tonhang = db.HH_TONKHO_HANG.Where(x => x.MA_HANG == id);
        //    ////if (tonhang.Count() > 0)
        //    ////{
        //    ////    var data1 = tonhang.FirstOrDefault();
        //    ////    HH_TON_KHO tonkhohang = new HH_TON_KHO();
        //    ////    tonkhohang.MA_HANG = data1.MA_HANG;
        //    ////    tonkhohang.MA_KHO = "TỒN TẠI HÃNG";
        //    ////    tonkhohang.SL_TON = data1.SL;
        //    ////    listtonkho.Add(tonkhohang);
        //    ////}
        //    ////var result = listtonkho.ToList().Select(x => new HH_TON_KHO()
        //    ////{
        //    ////    MA_HANG = x.MA_HANG,
        //    ////    MA_KHO = x.MA_KHO,
        //    ////    SL_TON = x.SL_TON
        //    ////}).ToList();
        //    //#endregion
        //    //var data = (from t1 in db.HHs
        //    //            join t2 in db.KHO_TON_HOPLONG on t1.MA_HANG equals t2.MA_HANG
        //    //            join t3 in db.KHO_TON_TAHCM on t1.MA_HANG equals t3.MA_HANG
        //    //            join t4 in db.KHO_TON_TADN on t1.MA_HANG equals t4.MA_HANG
        //    //            join t5 in db.HH_TONKHO_HANG on t1.MA_HANG equals t5.MA_HANG
        //    //            select new { t1.MA_HANG, t1.DON_VI_TINH, t1.MA_NHOM_HANG, t2.TON_KHO_HL, t3.TON_KHO_HCM, t4.TON_KHO_DN, t5.SL_TON }
        //    //            ).ToList();
        //    //var query = data.ToList().Select(x => new tonkhohanghoa
        //    //{
        //    //    mahang = x.MA_HANG,
        //    //    manhomhang = x.MA_NHOM_HANG,
        //    //    donvitinh = x.DON_VI_TINH,
        //    //    tonkhohoplong = x.TON_KHO_HL.ToString(),
        //    //    tonkhoHCM = x.TON_KHO_DN.ToString(),
        //    //    tonkhoDN = x.TON_KHO_DN.ToString(),
        //    //    tonkhohang = x.SL_TON.ToString(),
        //    //}).ToList();
            
        //}



        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
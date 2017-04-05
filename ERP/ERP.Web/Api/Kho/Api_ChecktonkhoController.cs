using ERP.Web.Models.Database;
using ERP.Web.Models.NewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ERP.Web.Api.Kho
{
    public class Api_ChecktonkhoController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();
        // GET: api/Checktonkho
        public List<tonkhohanghoa> Get()
        {
            var query = db.Database.SqlQuery<tonkhohanghoa>("DS_TONKHO_HOPLONG");
            var result = query.ToList().Select(x => new tonkhohanghoa()
            {
                MA_HANG = x.MA_HANG,
                MA_CHUAN = x.MA_CHUAN,
                THONG_SO = x.THONG_SO,
                XUAT_XU = x.XUAT_XU,
                GIA_LIST = x.GIA_LIST,
                DISCONTINUE = x.DISCONTINUE,
                MA_CHUYEN_DOI = x.MA_CHUYEN_DOI,
                SL_HOPLONG = x.SL_HOPLONG,
                SL_GIU = x.SL_GIU,
                SL_KYGUI_DEN = x.SL_KYGUI_DEN,
                SL_KYGUI_DI = x.SL_KYGUI_DI,
                SL_HANG = x.SL_HANG
    }).ToList();
            
            return result;
        }
    

    }
}

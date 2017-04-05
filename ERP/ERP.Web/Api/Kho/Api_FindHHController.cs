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

namespace ERP.Web.Api.Kho
{
    public class Api_FindHHController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_FindHH
        [ResponseType(typeof(HH))]
        public List<HH> GetHH(string id)
        {
            var query = db.Database.SqlQuery<HH>("XL_TimKiemHangHoa", "A22");
            
            return query.ToList();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HHExists(string id)
        {
            return db.HHs.Count(e => e.MA_HANG == id) > 0;
        }
    }
}
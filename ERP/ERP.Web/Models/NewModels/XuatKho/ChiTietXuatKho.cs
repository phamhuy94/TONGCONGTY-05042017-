using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.XuatKho
{
    public class ChiTietXuatKho
    {
        public string MaHang { get; set; }
        public string Kho { get; set; }
        public decimal DonGia { get; set; }
        public int SoLuong { get; set; }
        public string DVT { get; set; }
        public string TKNo { get; set; }
        public string TKCo { get; set; }
        public string TKKho { set; get; }
        public string DonGiaVon { set; get; }
    }
}
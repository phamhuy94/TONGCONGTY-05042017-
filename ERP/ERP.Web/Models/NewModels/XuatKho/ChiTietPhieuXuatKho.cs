﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels.XuatKho
{
    public class ChiTietPhieuXuatKho
    {
        public string MA_HANG { get; set; }
        public string DON_GIA_BAN { get; set; }
        public int SO_LUONG { get; set; }
        public string DVT { get; set; }
        public string TK_NO { get; set; }
        public string TK_CO { get; set; }
        public string TK_KHO { set; get; }
        public string DON_GIA_VON { set; get; }
    }
}
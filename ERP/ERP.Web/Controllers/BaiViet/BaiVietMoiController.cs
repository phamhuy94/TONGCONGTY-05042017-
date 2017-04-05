using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Controllers
{
    public class BaiVietMoiController : Controller
    {
        // GET: BaiVietMoi
        public ActionResult Index()
        {
            return View();
        }

        public string UploadFiles()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    string filename = "";
                    HttpPostedFileBase file = Request.Files[0];
                    filename = file.FileName;
                    filename = filename.Substring(filename.LastIndexOf("\\") + 1);
                    file.SaveAs(Server.MapPath("~/Content/Content/" + filename));
                    return "/Content/Content/" + filename;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Không có file được chọn";
            }
        }

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
                        var path = Path.Combine(Server.MapPath("~/Content/Images/BaiViet"), fileName);
                        file.SaveAs(path);
                    }
                }
            }
        }
    }
}
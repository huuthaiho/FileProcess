using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;

namespace EmployeeListApp.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(IEnumerable<HttpPostedFileBase> PostedFiles)
        {

            var supportedTypes = new[] { "txt"};
            var path = Path.Combine(Server.MapPath("~/UploadFiles"));

            foreach (HttpPostedFileBase file in PostedFiles)
            {
                if (file != null)
                {
                    var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                    //Save TXT file only - We can validate file extension at Client side
                    if (supportedTypes.Contains(fileExt))
                    {
                        var filename = file.FileName;
                        file.SaveAs(Path.Combine(path, filename));
                    }
                }
            }

            string[] filePaths = Directory.GetFiles(path, "*.txt",SearchOption.TopDirectoryOnly);

            TempData["files"] = filePaths;
            return RedirectToAction("Index","Employeers");
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeListApp.Models
{
    public class FileUploadModel
    {
        public IEnumerable<HttpPostedFileBase> PostedFiles { get; set; }
        public string Filename { get; set; }
    }
}
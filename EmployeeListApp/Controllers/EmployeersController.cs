using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using FileProcessor;
using EmployeeListApp.Models;
using System.Threading.Tasks;
using DataModel;

namespace EmployeeListApp.Controllers
{
    public class EmployeersController : Controller
    {
        private IFileProcessor _fileProcessor = null;

        public EmployeersController (IFileProcessor fileProcessor)
        {
            _fileProcessor = fileProcessor;
        }

        // GET: Employeers
        public async Task<ActionResult> Index(string sortBy)
        {
            string[] files = (string[]) TempData["files"] ;
            
            ViewBag.NameSort = String.IsNullOrEmpty(sortBy) ? "CompanyName desc" : "";
            ViewBag.YearSort = sortBy == "YearOfBusiness" ? "YearOfBusiness desc" : "YearOfBusiness";
            ViewBag.ContactorSort = sortBy == "Contactor" ? "Contactor desc" : "Contactor";
            List<Employeer> employeers ;


            if (files != null)
            {
                //Read file asynchronous
                Task<List<Employeer>> filesReadTask = _fileProcessor.ReadFilesAsyn(files.ToList());

                //Read file synchronous
                //List<FileProcessor.Employeer> employeers = _fileProcessor.ReadFiles(files.ToList());

                //Read file Paralel 
                //List<FileProcessor.Employeer> employeers = _fileProcessor.ReadFilesParalel(files.ToList());

                employeers = await filesReadTask;
                TempData["employeers"] = employeers;
                
            }
            else
            {
                employeers = TempData["employeers"] ==null ? new List<Employeer>(): (List<Employeer>)TempData["employeers"];
                TempData.Keep();
            }
                
            switch (sortBy)
            {
                case "CompanyName desc":
                    employeers = employeers.OrderByDescending(x => x.CompanyName).ToList();
                    break;
                case "YearOfBusiness desc":
                    employeers = employeers.OrderByDescending(x => x.YearOfBusiness).ToList();
                    break;

                case "YearOfBusiness":
                    employeers = employeers.OrderBy(x => x.YearOfBusiness).ToList();
                    break;

                case "Contactor desc":
                    employeers = employeers.OrderByDescending(x => x.Contactor).ToList();
                    break;

                case "Contactor":
                    employeers = employeers.OrderBy(x => x.Contactor).ToList();
                    break;

                default:
                    employeers = employeers.OrderBy(x => x.CompanyName).ToList();
                    break;
            }

            return View(employeers);
        }

    }
}

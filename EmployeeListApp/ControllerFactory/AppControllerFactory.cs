using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FileProcessor;

namespace EmployeeListApp.ControllerFactory
{
    public class AppControllerFactory : System.Web.Mvc.DefaultControllerFactory
    {
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            IController controller = null;

            if (controllerName == "FileUpload")
            {
                 controller = new Controllers.FileUploadController();
            }

            if (controllerName == "Employeers")
            {
                 IFileProcessor fileProcessor = new TextFileProcessor();
                 controller = new Controllers.EmployeersController(fileProcessor);
            }

            return controller;
        }

        public override void ReleaseController(IController controller)
        {
            IDisposable dispose = controller as IDisposable;
            if (dispose != null)
            {
                dispose.Dispose();
            }
        }
    }

}

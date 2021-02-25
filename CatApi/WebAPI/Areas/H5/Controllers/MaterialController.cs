using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Areas.H5.Controllers
{
    public class MaterialController : Controller
    {
        //
        // GET: /H5/Material/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GameInfo()
        {
            return View();
        }

    }
}

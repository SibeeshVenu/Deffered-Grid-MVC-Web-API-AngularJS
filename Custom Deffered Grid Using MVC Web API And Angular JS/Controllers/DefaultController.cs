using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Custom_Deffered_Grid_Using_MVC_Web_API_And_Angular_JS.Models;
namespace Custom_Deffered_Grid_Using_MVC_Web_API_And_Angular_JS.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        DataModel dm = new DataModel();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getData(int pageOffset = 0)
        {
            var data = dm.fetchData(pageOffset);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
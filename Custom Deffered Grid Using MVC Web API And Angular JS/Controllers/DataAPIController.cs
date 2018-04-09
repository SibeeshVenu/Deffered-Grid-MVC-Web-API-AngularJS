using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Custom_Deffered_Grid_Using_MVC_Web_API_And_Angular_JS.Models;
namespace Custom_Deffered_Grid_Using_MVC_Web_API_And_Angular_JS.Controllers
{
    public class DataAPIController : ApiController
    {
        DataModel dm = new DataModel();
        public string getData(int id)
        {
            var d = dm.fetchData(id);
            return d;
        }
    }

}

using MVC_Basic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Basic.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Detail/
        WebSiteBanHangEntities objWebsiteBanHangEntities = new WebSiteBanHangEntities();
        public ActionResult Detail(int Id)                                             
        {
           
            var objProduct = objWebsiteBanHangEntities.Product_2119110143.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
	}
}
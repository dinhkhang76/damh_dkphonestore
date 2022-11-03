using MVC_Basic.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Basic.Controllers
{
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities objwebsiteBanHangEntities = new WebSiteBanHangEntities();
        public ActionResult Index()
        {
            var lstCategory = objwebsiteBanHangEntities.Category_2119110143.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id)
        {
            var listProduct = objwebsiteBanHangEntities.Product_2119110143.Where(n => n.CategoryId == Id).ToList();
            return View(listProduct);
        }
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Basic.Context;
using PagedList;

namespace MVC_Basic.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private WebSiteBanHangEntities db = new WebSiteBanHangEntities();

        // GET: Admin/Order
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstOrder = new List<Order_2119110143>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstOrder = db.Order_2119110143.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstOrder = db.Order_2119110143.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 6; //so sp hien tren 1 trang
            int pageNumber = (page ?? 1);
            lstOrder = lstOrder.OrderByDescending(n => n.Id).ToList();
            return View(lstOrder.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_2119110143 order = db.Order_2119110143.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_2119110143 order_2119110158 = db.Order_2119110143.Find(id);
            if (order_2119110158 == null)
            {
                return HttpNotFound();
            }
            return View(order_2119110158);
        }

        // POST: Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_2119110143 order_2119110158 = db.Order_2119110143.Find(id);
            db.Order_2119110143.Remove(order_2119110158);
            db.SaveChanges();
            TempData["message"] = new XMessage("success", "Xóa thành công");

            return RedirectToAction("Index");
        }
    }
}

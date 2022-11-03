using MVC_Basic.Context;
using MVC_Basic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Basic.Controllers
{

    public class PaymentController : Controller
    {
        WebSiteBanHangEntities objWebsiteBanHangEntities = new WebSiteBanHangEntities();

        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                //gan du lieu cho Order
                Order_2119110143 objOrder = new Order_2119110143();
                objOrder.Name = "DonHang" + DateTime.Now.ToString("ddMMyyyyHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objWebsiteBanHangEntities.Order_2119110143.Add(objOrder);
                //luu vao bang Order
                objWebsiteBanHangEntities.SaveChanges();
                //Lay OrderId vua tao luu vao bang OrderDetail
                int intOrderId = objOrder.Id;

                List<OrderDetail_2119110143> lstOrderDetail = new List<OrderDetail_2119110143>();
                foreach (var item in lstCart)
                {
                    OrderDetail_2119110143 obj = new OrderDetail_2119110143();
                    obj.Quantity = item.Quantity;
                    obj.OrderId = intOrderId;
                    obj.ProductId = item.Product.Id;
                    lstOrderDetail.Add(obj);
                }
                objWebsiteBanHangEntities.OrderDetail_2119110143.AddRange(lstOrderDetail);
                objWebsiteBanHangEntities.SaveChanges();
            }
            return View();
        }
    }
}
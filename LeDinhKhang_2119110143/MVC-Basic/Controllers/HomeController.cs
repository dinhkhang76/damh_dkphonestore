using MVC_Basic.Context;
using MVC_Basic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Basic.Controllers
{
    public class HomeController : Controller
    {
        WebSiteBanHangEntities objWebsiteBanHangEntities = new WebSiteBanHangEntities();
        public ActionResult Index(/*string currentFilter,string SearchString, int? page*/)
        {
            //var lstProduct = new List<Product_2119110143>();
            //if (SearchString != null)
            //{
            //    page = 1;
            //}
            //else
            //{
            //    SearchString = currentFilter;
            //}
            //if (!string.IsNullOrEmpty(SearchString))
            //{
            //    lstProduct = objWebsiteBanHangEntities.Product_2119110143.Where(n => n.Name.Contains(SearchString)).ToList();
            //}
            //else
            //{
            //    lstProduct = objWebsiteBanHangEntities.Product_2119110143.ToList();
            //}
            //ViewBag.CurrentFilter = SearchString;

            List<User_2119110143> lstUser = new List<User_2119110143>();
            lstUser = objWebsiteBanHangEntities.User_2119110143.ToList();


            List<Category_2119110143> lstCate = new List<Category_2119110143>();
            lstCate = objWebsiteBanHangEntities.Category_2119110143.ToList();

            List<Product_2119110143> lstPro = new List<Product_2119110143>();
            lstPro = objWebsiteBanHangEntities.Product_2119110143/*.Where(n => n.TypeId == 1)*/.ToList();

            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListCategory = lstCate;
            objHomeModel.ListProduct = lstPro;
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User_2119110143 _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebsiteBanHangEntities.User_2119110143.FirstOrDefault(s => s.Email == _user.Email);
                if(check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.IsAdmin = false; //IsAdmin = false = người dùng
                    objWebsiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebsiteBanHangEntities.User_2119110143.Add(_user);
                    objWebsiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Đăng ký thành công");
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email đã tồn tại";
                    TempData["message"] = new XMessage("danger", "Email đã tồn tại");

                    return View();
                }

                }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebsiteBanHangEntities.User_2119110143.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().LastName + " " + data.FirstOrDefault().FirstName ;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = new XMessage("danger", "Email hoặc mật khẩu không đúng");
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;

        }

    }
}
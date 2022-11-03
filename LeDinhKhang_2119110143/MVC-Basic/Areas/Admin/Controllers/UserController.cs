using MVC_Basic.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static MVC_Basic.Common;

namespace MVC_Basic.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebSiteBanHangEntities objwebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Admin/User
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstUser = new List<User_2119110143>();
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
                lstUser = objwebSiteBanHangEntities.User_2119110143.Where(n => n.FirstName.Contains(SearchString)).ToList();
            }
            else
            {
                lstUser = objwebSiteBanHangEntities.User_2119110143.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstUser = lstUser.OrderByDescending(n => n.Id).ToList();
            return View(lstUser.ToPagedList(pageNumber, pageSize));
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    Common objCommon = new Common();
        //    var lstUser = objwebSiteBanHangEntities.User_2119110143.ToList();
        //    ListtoDataTableConverter converter = new ListtoDataTableConverter();
        //    DataTable dtUser = converter.ToDataTable(lstUser);
        //    ViewBag.ListUser = objCommon.ToSelectList(dtUser, "Id", "IsAdmin");

     
        //    List<UserType> lstUserType = new List<UserType>();
        //    UserType objUserType = new UserType();
        //    objUserType.Id = 1;
        //    objUserType.IsAdmin = "Admin";
        //    lstUserType.Add(objUserType);

        //    objUserType = new UserType();
        //    objUserType.Id = 2;
        //    objUserType.IsAdmin = "Khách hàng";
        //    lstUserType.Add(objUserType);

        //    DataTable dtUserType = converter.ToDataTable(lstUserType);
        //    ViewBag.UserType = objCommon.ToSelectList(dtUserType, "Id", "IsAdmin");
        //    return View();
        //}

        //[ValidateInput(false)]
        //[HttpPost]
        //public ActionResult Create(User_2119110143 User)
        //{
        //    return View(User);
        //}

        [HttpGet]
        public ActionResult Details(int id)
        {
            var User = objwebSiteBanHangEntities.User_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var User = objwebSiteBanHangEntities.User_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
        }
        [HttpPost]
        public ActionResult Delete(User_2119110143 objPro)
        {
            var objUser = objwebSiteBanHangEntities.User_2119110143.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objwebSiteBanHangEntities.User_2119110143.Remove(objUser);
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Common objCommon = new Common();
            var lstUser = objwebSiteBanHangEntities.User_2119110143.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtUser = converter.ToDataTable(lstUser);
            ViewBag.ListUser = objCommon.ToSelectList(dtUser, "Id", "IsAdmin");

            //Quyền thành viên
            List<UserType> lstUserType = new List<UserType>();
            UserType objUserType = new UserType();
            objUserType.IsAdmin = "Admin";
            lstUserType.Add(objUserType);

            objUserType = new UserType();
            objUserType.IsAdmin = "Khách hàng";
            lstUserType.Add(objUserType);

            DataTable dtUserType = converter.ToDataTable(lstUserType);
            ViewBag.UserType = objCommon.ToSelectList(dtUserType, "Id", "IsAdmin");

            var User = objwebSiteBanHangEntities.User_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(User);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, User_2119110143 objUser)
        {
            objwebSiteBanHangEntities.Entry(objUser).State = EntityState.Modified;
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập nhật thành công");
            return RedirectToAction("Index");
        }
    }
}
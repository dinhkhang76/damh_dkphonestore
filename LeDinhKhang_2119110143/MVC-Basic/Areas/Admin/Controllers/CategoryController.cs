using MVC_Basic.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using static MVC_Basic.Common;

namespace MVC_Basic.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebSiteBanHangEntities objwebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Admin/Category
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category_2119110143>();
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
                lstCategory = objwebSiteBanHangEntities.Category_2119110143.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objwebSiteBanHangEntities.Category_2119110143.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.Id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = objwebSiteBanHangEntities.Category_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = objwebSiteBanHangEntities.Category_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(Category_2119110143 objCat)
        {
            var objCategory = objwebSiteBanHangEntities.Category_2119110143.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objwebSiteBanHangEntities.Category_2119110143.Remove(objCategory);
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Xóa thành công");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            this.LoadData();
            return View();


            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category_2119110143 category)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (category.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(category.ImageUpload.FileName);
                        string extension = Path.GetExtension(category.ImageUpload.FileName);
                        fileName = fileName + extension;
                        category.Avatar = fileName;
                        category.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
                    }
                    category.CreatedOnUtc = DateTime.Now;
                    objwebSiteBanHangEntities.Category_2119110143.Add(category);
                    objwebSiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm thành công");

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(category);
        }

        void LoadData()
        {
            Common objCommon = new Common();

            var lstCat = objwebSiteBanHangEntities.Category_2119110143.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");

            List<CategoryType> lstCategoryType = new List<CategoryType>();
            CategoryType objCategoryType = new CategoryType();
            objCategoryType.Id = 1;
            objCategoryType.Name = "Danh mục phổ biến";
            lstCategoryType.Add(objCategoryType);

            DataTable dtCategoryType = converter.ToDataTable(lstCategoryType);
            ViewBag.CategoryType = objCommon.ToSelectList(dtCategoryType, "Id", "Name");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Common objCommon = new Common();

            var lstCat = objwebSiteBanHangEntities.Category_2119110143.ToList();

            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");


            List<CategoryType> lstCategoryType = new List<CategoryType>();
            CategoryType objCategoryType = new CategoryType();
            objCategoryType.Id = 1;
            objCategoryType.Name = "Danh mục phổ biến";
            lstCategoryType.Add(objCategoryType);



            DataTable dtCategoryType = converter.ToDataTable(lstCategoryType);
            ViewBag.CategoryType = objCommon.ToSelectList(dtCategoryType, "Id", "Name");

            var category = objwebSiteBanHangEntities.Category_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Category_2119110143 objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/category"), fileName));
            }
            objCategory.UpdatedOnUtc = DateTime.Now;
            objwebSiteBanHangEntities.Entry(objCategory).State = EntityState.Modified;
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập nhật thành công");

            return RedirectToAction("Index");
        }
    }
}
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
    public class BrandController : Controller
    {
        WebSiteBanHangEntities objwebSiteBanHangEntities = new WebSiteBanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand_2119110143>();
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
                lstBrand = objwebSiteBanHangEntities.Brand_2119110143.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objwebSiteBanHangEntities.Brand_2119110143.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.Id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = objwebSiteBanHangEntities.Brand_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = objwebSiteBanHangEntities.Brand_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(Brand_2119110143 objCat)
        {
            var objBrand = objwebSiteBanHangEntities.Brand_2119110143.Where(n => n.Id == objCat.Id).FirstOrDefault();
            objwebSiteBanHangEntities.Brand_2119110143.Remove(objBrand);
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Xóa thành công");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand_2119110143 brand)
        {
            if (ModelState.IsValid)//Lưu ý
            {
                try
                {
                    if (brand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(brand.ImageUpload.FileName);
                        string extension = Path.GetExtension(brand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        brand.Avatar = fileName;
                        brand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"), fileName));
                    }
                    brand.CreatedOnUtc = DateTime.Now;
                    objwebSiteBanHangEntities.Brand_2119110143.Add(brand);
                    objwebSiteBanHangEntities.SaveChanges();
                    TempData["message"] = new XMessage("success", "Thêm thành công");

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            return View(brand);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Brand = objwebSiteBanHangEntities.Brand_2119110143.Where(n => n.Id == id).FirstOrDefault();
            return View(Brand);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Edit(int id, Brand_2119110143 objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/brand"), fileName));
            }
            objBrand.UpdateOnUtc = DateTime.Now;
            objwebSiteBanHangEntities.Entry(objBrand).State = EntityState.Modified;
            objwebSiteBanHangEntities.SaveChanges();
            TempData["message"] = new XMessage("success", "Cập nhật thành công");

            return RedirectToAction("Index");
        }
    }
}
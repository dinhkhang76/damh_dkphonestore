using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Basic.Models
{
    public partial class ProductMasterData
    {
        public int Id { get; set; }
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]


        public string Name { get; set; }
        //[Display(Name ="Hình ảnh")]
        //[Required(ErrorMessage = "Chưa chọn hình ảnh")]

        public string Avatar { get; set; }
        [Required(ErrorMessage = "Chưa chọn danh mục")]

        [Display(Name = "Danh mục")]
        public Nullable <int> CategoryId { get; set; }

        [Display(Name = "Loại sản phẩm")]
        public Nullable<int> TypeId { get; set; }
        [Required(ErrorMessage = "Chưa chọn thương hiệu")]
        [Display(Name = "Thương hiệu")]
        public Nullable<int> BrandId { get; set; }
        [Required(ErrorMessage = "Mô tả ngắn không được để trống")]
        [Display(Name = "Mô tả ngắn")]
        public string ShortDes { get; set; }
        [Required(ErrorMessage ="Mô tả đầy đủ không được để trống")]
        [Display(Name = "Mô tả đầy đủ")]
        public string FullDescription { get; set; }
        [Required(ErrorMessage = "Giá gốc không được để trống")]
        [Display(Name = "Giá gốc")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Giá khuyến mãi")]
        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]

        public Nullable<double> PriceDiscount { get; set; }
        public string Slug { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<bool> ShowOnHomePage { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }


    }
}
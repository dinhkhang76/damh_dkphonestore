using MVC_Basic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Basic.Context

{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {

    }
    [MetadataType(typeof(ProductMasterData))]
    public partial class Product_2119110143
    {
        [NotMapped]
        //[Display(Name = "Hình ảnh")]
        //[Required(ErrorMessage = "Chưa chọn hình ảnh")]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category_2119110143
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

}
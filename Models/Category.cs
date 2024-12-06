namespace TX2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [Display(Name = "Mã Danh mục")]
        public int Categoryid { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống Tên Danh mục")]
        [StringLength(150)]
        [Display(Name = "Tên Danh mục")]
        public string CategoryName { get; set; }

        [StringLength(50)]
        [Display(Name = "Mô tả")]
        public string MetaTitle { get; set; }
    }
}

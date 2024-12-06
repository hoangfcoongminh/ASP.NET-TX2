namespace TX2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [Display(Name = "Mã Sản phẩm")]
        public int Pid { get; set; }

        [Display(Name = "Mã Danh mục")]
        public int Categoryid { get; set; }

        [Required(ErrorMessage = "Không được bỏ trống Tên sản phẩm")]
        [StringLength(250)]
        [Display(Name = "Tên sản phẩm")]
        public string ProdName { get; set; }

        [StringLength(50)]
        [Display(Name = "Meta Title")]
        [Required(ErrorMessage = "Không được bỏ trống Meta Title")]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        [Required(ErrorMessage = "Không được bỏ trống Mô tả")]
        public string Description { get; set; }

        [StringLength(550)]
        [Display(Name = "Hình ảnh")]
        public string ImagePath { get; set; }

        [Display(Name = "Giá bán")]
        [Required(ErrorMessage = "Không được bỏ trống giá")]
        public decimal Price { get; set; }
    }
}

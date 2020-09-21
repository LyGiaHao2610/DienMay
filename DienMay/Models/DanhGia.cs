using DienMay.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DienMay.Models
{
    public class DanhGia
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDanhGia { get; set; }
        public string MoTaDanhGia { get; set; }
        public int MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public string IDKhachHang { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
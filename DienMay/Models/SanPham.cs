using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DienMay.Models
{
    public class SanPham
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TenSanPham { get; set; }
        public string HinhAnh { get; set; }
        public string Mota { get; set; }
        public string TinhNang { get; set; }
        public int SoLuong { get; set; }
        public int GiaSanPham { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        public int MaHangSanXuat { get; set; }
        public virtual HangSanXuat HangSanXuat { get; set; }

    }
}
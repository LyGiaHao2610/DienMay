using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DienMay.Models
{
    public class HangSanXuat
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHangSanXuat { get; set; }
        public string TenHang { get; set; }
    }
}
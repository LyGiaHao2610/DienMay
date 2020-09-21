using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DienMay.Models
{
    public class TinTuc
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTinTuc { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public string Hinh { get; set; }
    }
}
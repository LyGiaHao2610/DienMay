using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DienMay.Models
{
    public class QuangCao
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TenQuangCao { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }
    }
}
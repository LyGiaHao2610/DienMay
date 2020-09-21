using DienMay.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DienMay.DAL
{
    //public partial class Data: DbContext

    //{
    //    public Data() : base("name=DienMayModel")
    //    { }
    //    public DbSet<Category> categories { get; set; }
    //    public DbSet<DanhGia> danhGias { get; set; }
    //    public DbSet<HangSanXuat> hangSanXuats { get; set; }
    //    public DbSet<QuangCao> quangCaos { get; set; }
    //    public DbSet<SanPham> sanPhams { get; set; }
    //    public DbSet<TinTuc> tinTucs { get; set; }
    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //    }

    //    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    //{
    //    //    base.OnModelCreating(modelBuilder);

    //    //    //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
    //    //    //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
    //    //    //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
    //    //}
    //}
}
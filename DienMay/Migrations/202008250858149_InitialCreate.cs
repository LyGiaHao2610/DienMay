namespace DienMay.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.DanhGias",
                c => new
                    {
                        MaDanhGia = c.Int(nullable: false, identity: true),
                        MoTaDanhGia = c.String(),
                        MaSanPham = c.Int(nullable: false),
                        IDKhachHang = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        SanPham_ID = c.Int(),
                    })
                .PrimaryKey(t => t.MaDanhGia)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.SanPhams", t => t.SanPham_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.SanPham_ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(),
                        HinhAnh = c.String(),
                        Mota = c.String(),
                        TinhNang = c.String(),
                        SoLuong = c.Int(nullable: false),
                        GiaSanPham = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        MaHangSanXuat = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.HangSanXuats", t => t.MaHangSanXuat, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.MaHangSanXuat);
            
            CreateTable(
                "dbo.HangSanXuats",
                c => new
                    {
                        MaHangSanXuat = c.Int(nullable: false, identity: true),
                        TenHang = c.String(),
                    })
                .PrimaryKey(t => t.MaHangSanXuat);
            
            CreateTable(
                "dbo.QuangCaos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenQuangCao = c.String(),
                        Hinh = c.String(),
                        MoTa = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TinTucs",
                c => new
                    {
                        MaTinTuc = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        NoiDung = c.String(),
                        Hinh = c.String(),
                    })
                .PrimaryKey(t => t.MaTinTuc);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DanhGias", "SanPham_ID", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "MaHangSanXuat", "dbo.HangSanXuats");
            DropForeignKey("dbo.SanPhams", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.DanhGias", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SanPhams", new[] { "MaHangSanXuat" });
            DropIndex("dbo.SanPhams", new[] { "CategoryID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.DanhGias", new[] { "SanPham_ID" });
            DropIndex("dbo.DanhGias", new[] { "ApplicationUser_Id" });
            DropTable("dbo.TinTucs");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.QuangCaos");
            DropTable("dbo.HangSanXuats");
            DropTable("dbo.SanPhams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DanhGias");
            DropTable("dbo.Categories");
        }
    }
}

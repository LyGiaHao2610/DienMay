using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Razor;
using System.Web.Security;
using DienMay.Models;

namespace DienMay.Controllers
{
    public class SanPhamsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SanPhams
        public ActionResult Index(string searchString)
        {
            var Loc = from p in db.sanPhams
                      select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                
                Loc = Loc.Where(s => s.TenSanPham.Contains(searchString));
                

            }
            return View(Loc);
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        [Authorize]
        public ActionResult AddToCart(int? id, string url)
        {
            if(Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = db.sanPhams.Find(id);
                cart.Add(new Item()
                {
                    SanPham = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.sanPhams.Find(id);
                var count = cart.Count();
                for(int i = 0; i<count;i++)
                {
                    if(cart[i].SanPham.ID == id)
                    {
                        int PrevQ = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            SanPham = product,
                            Quantity = PrevQ + 1
                        });
                        break;
                    }
                    else
                    {
                        var product2 = cart.Where(x => x.SanPham.ID == id).SingleOrDefault();
                        if(product2==null)
                        {
                            cart.Add(new Item()
                            {
                                SanPham = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Checkout");
        }
        public ActionResult Remove(int? id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            //var product = db.sanPhams.Find(id);
            foreach(var item in cart)
            {
                if(item.SanPham.ID == id)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult CheckoutDetails()
        {
            return View();
        }
        public ActionResult DecreaseQ(int? id)
        {
            if(Session["cart"] !=null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = db.sanPhams.Find(id);
                foreach (var item in cart)
                {
                    if(item.SanPham.ID == id)
                    {
                        int PrevQ = item.Quantity;
                        if(PrevQ > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                SanPham = product,
                                Quantity = PrevQ - 1
                            });
                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Checkout");
        }
        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName");
            ViewBag.MaHangSanXuat = new SelectList(db.hangSanXuats, "MaHangSanXuat", "TenHang");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenSanPham,HinhAnh,Mota,TinhNang,SoLuong,GiaSanPham,CategoryID,MaHangSanXuat")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    sanPham.HinhAnh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.sanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName", sanPham.CategoryID);
            ViewBag.MaHangSanXuat = new SelectList(db.hangSanXuats, "MaHangSanXuat", "TenHang", sanPham.MaHangSanXuat);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName", sanPham.CategoryID);
            ViewBag.MaHangSanXuat = new SelectList(db.hangSanXuats, "MaHangSanXuat", "TenHang", sanPham.MaHangSanXuat);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenSanPham,HinhAnh,Mota,TinhNang,SoLuong,GiaSanPham,CategoryID,MaHangSanXuat")] SanPham sanPham, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    sanPham.HinhAnh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.categories, "CategoryID", "CategoryName", sanPham.CategoryID);
            ViewBag.MaHangSanXuat = new SelectList(db.hangSanXuats, "MaHangSanXuat", "TenHang", sanPham.MaHangSanXuat);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.sanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.sanPhams.Find(id);
            db.sanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

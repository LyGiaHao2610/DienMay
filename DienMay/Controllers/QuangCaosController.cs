using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMay.Models;

namespace DienMay.Controllers
{
    public class QuangCaosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QuangCaos
        public ActionResult Index()
        {
            return View(db.quangCaos.ToList());
        }

        // GET: QuangCaos/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.quangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // GET: QuangCaos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuangCaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenQuangCao,Hinh,MoTa")] QuangCao quangCao, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    string _file = Path.GetFileName(img.FileName);
                    quangCao.Hinh = _file;
                    string _path = Path.Combine(Server.MapPath("~/HinhAnh"), _file);
                    img.SaveAs(_path);
                }
                db.quangCaos.Add(quangCao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quangCao);
        }

        // GET: QuangCaos/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.quangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: QuangCaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenQuangCao,Hinh,MoTa")] QuangCao quangCao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quangCao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quangCao);
        }

        // GET: QuangCaos/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuangCao quangCao = db.quangCaos.Find(id);
            if (quangCao == null)
            {
                return HttpNotFound();
            }
            return View(quangCao);
        }

        // POST: QuangCaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuangCao quangCao = db.quangCaos.Find(id);
            db.quangCaos.Remove(quangCao);
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

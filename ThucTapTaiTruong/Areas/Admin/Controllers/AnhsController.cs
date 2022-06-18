using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ThucTapTaiTruong.Models.Entities;

namespace ThucTapTaiTruong.Areas.Admin.Controllers
{
    public class AnhsController : Controller
    {
        private MyDBContext db = new MyDBContext();

        // GET: Admin/Anhs
        public ActionResult Index()
        {
            var anhs = db.Anhs.Include(a => a.SanPham);
            return View(anhs.ToList());
        }

        // GET: Admin/Anhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anh anh = db.Anhs.Find(id);
            if (anh == null)
            {
                return HttpNotFound();
            }
            return View(anh);
        }

        // GET: Admin/Anhs/Create
        public ActionResult Create()
        {
            ViewBag.MaSanPham = new SelectList(db.SanPhams.Where(p => p.Xoa != false), "MaSanPham", "TenSP");
            return View();
        }

        // POST: Admin/Anhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Anh anh, HttpPostedFileBase URL)
        {
            if (ModelState.IsValid)
            {
                db.Anhs.Add(anh);
                db.SaveChanges();
                if (URL != null && URL.ContentLength > 0)
                {
                    int id = anh.MaAnh;
                    string _FileName = "";
                    _FileName = URL.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Content/images/home2"), _FileName);
                    URL.SaveAs(_path);

                    Anh uAnh = db.Anhs.FirstOrDefault(x => x.MaAnh == id);
                    uAnh.URL = _FileName;
                    db.SaveChanges();
                }
                    return RedirectToAction("Index");
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams.Where(p => p.Xoa != false), "MaSanPham", "TenSP", anh.MaSanPham);
            return View(anh);
        }

        // GET: Admin/Anhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anh anh = db.Anhs.Find(id);
            if (anh == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams.Where(p => p.Xoa != false), "MaSanPham", "TenSP", anh.MaSanPham);
            return View(anh);
        }

        // POST: Admin/Anhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Anh anh, HttpPostedFileBase URL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anh).State = EntityState.Modified;
                db.SaveChanges();
                if (URL != null && URL.ContentLength > 0)
                {
                    int id = anh.MaAnh;
                    string _FileName = "";
                    _FileName = URL.FileName;
                    string _path = Path.Combine(Server.MapPath("~/Content/images/home2"), _FileName);
                    URL.SaveAs(_path);

                    Anh uAnh = db.Anhs.FirstOrDefault(x => x.MaAnh == id);
                    uAnh.URL = _FileName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams.Where(p => p.Xoa != false), "MaSanPham", "TenSP", anh.MaSanPham);
            return View(anh);
        }

        // GET: Admin/Anhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anh anh = db.Anhs.Find(id);
            if (anh == null)
            {
                return HttpNotFound();
            }
            return View(anh);
        }

        // POST: Admin/Anhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anh anh = db.Anhs.Find(id);
            db.Anhs.Remove(anh);
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

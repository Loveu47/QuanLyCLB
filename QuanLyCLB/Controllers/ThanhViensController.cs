using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCLB.Models;
using EntityState = System.Data.Entity.EntityState;

namespace QuanLyCLB.Controllers
{
    public class ThanhViensController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;
        // GET: ThanhViens
        public ActionResult Index()
        {
            var thanhViens = db.ThanhViens.Where(j=>j.ToChucId == acc.ToChucId);
            return View(thanhViens.ToList());
        }
        // GET: ThanhViens/Create
        public ActionResult Create()
        {
            ViewBag.BanId = new SelectList(db.Bans.Where(j=>j.ToChucId == acc.ToChucId), "Id", "TenBan");
            return View();
        }

        // POST: ThanhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HoTen,MSSV,SDT,NganhHoc,NgaySinh,ToChucId,BanId,ChucVu")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {   if(thanhVien.ChucVu == null)
                {
                    thanhVien.ChucVu = "Thành viên";
                }
                db.ThanhViens.Add(thanhVien);
                db.SaveChanges();
                ThongBao("Thêm mới thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ThongBao("Có lỗi xảy ra, vui lòng thử lại!!!", "error");
            return RedirectToAction("Index");
        }

        // GET: ThanhViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.BanId = new SelectList(db.Bans.Where(j=>j.ToChucId == acc.ToChucId), "Id", "TenBan", thanhVien.BanId);
            return View(thanhVien);
        }

        // POST: ThanhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HoTen,MSSV,SDT,NganhHoc,NgaySinh,ToChucId,BanId,ChucVu")] ThanhVien thanhVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thanhVien).State = EntityState.Modified;
                db.SaveChanges();
                ThongBao("Chỉnh sửa thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ThongBao("Có lỗi xảy ra, vui lòng thử lại!!!", "error");
            return RedirectToAction("Index");
        }

        // GET: ThanhViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanhVien thanhVien = db.ThanhViens.Find(id);
            if (thanhVien == null)
            {
                return HttpNotFound();
            }
            db.ThanhViens.Remove(thanhVien);
            db.SaveChanges();
            ThongBao("Xoá thành công!!!", "success");
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

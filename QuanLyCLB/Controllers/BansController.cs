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
    public class BansController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;
        // GET: Bans
        public ActionResult Index()
        {
            var bans = db.Bans.Where(i => i.ToChucId == acc.ToChucId);
            return View(bans.ToList());
        }

        // POST: Bans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenBan,MoTa,ToChucId")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Bans.Add(ban);
                db.SaveChanges();
                ThongBao("Thêm mới thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ThongBao("Có lỗi xảy ra, vui lòng thử lại!!!", "error");
            return RedirectToAction("Index");
        }

        // GET: Bans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bans.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban);
        }

        // POST: Bans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenBan,MoTa,ToChucId")] Ban ban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ban).State = EntityState.Modified;
                db.SaveChanges();
                ThongBao("Chỉnh sửa thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ThongBao("Có lỗi xảy ra, vui lòng thử lại!!!", "error");
            return View(ban);
        }

        // GET: Bans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ban ban = db.Bans.Find(id);
            if (ban == null)
            {
                return HttpNotFound();
            }
            List<ThanhVien> tv = (List<ThanhVien>) db.ThanhViens.Where(x => x.BanId == id).ToList();
            foreach(ThanhVien item in tv)
            {
                db.ThanhViens.Remove(item);
            }
            db.Bans.Remove(ban);
            ThongBao("Xoá thành công!!", "success");
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

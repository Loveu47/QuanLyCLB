using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCLB.Models;

namespace QuanLyCLB.Controllers
{
    public class TaiKhoansController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;

        // GET: TaiKhoans
        public ActionResult Index()
        {   if(acc.QLCapCao == true)
            {
                ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten");
                var taiKhoans = db.TaiKhoans.Include(t => t.ToChuc);
                return View(taiKhoans.ToList());
            }
            else
            {
                return View("Error");
            }
        }

        // GET: TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ToChucId,TaiKhoan1,MatKhau,HoTen,ChucVu,SDT,Email,QLCapCao")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                int dem = db.TaiKhoans.Where(j => j.TaiKhoan1 == taiKhoan.TaiKhoan1).Count();
                int mail = db.TaiKhoans.Where(i=>i.Email == taiKhoan.Email).Count();
                if (dem == 0 || mail ==0)
                {
                    db.TaiKhoans.Add(taiKhoan);
                    db.SaveChanges();
                    ThongBao("Thêm mới tài khoản thành công!!!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ThongBao("Tên tài khoản hoặc email đã tồn tại!!!", "error");
                    return RedirectToAction("Index");
                }  
            }
            ThongBao("Có lỗi xảy ra, vui lòng thử lại!!!", "error");
            return RedirectToAction("Index");
        }

        public ActionResult SuaQuyen(int id)
        {
            var item = db.TaiKhoans.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            if(item.QLCapCao == null)
            {
                item.QLCapCao = true;
            } else
            {
                item.QLCapCao = !item.QLCapCao;
            }
            db.SaveChanges();
            return Json(new
            {
                status = item.QLCapCao
            }) ;
        }
        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
                if (taiKhoan == null)
                {
                    return HttpNotFound();
                }
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
                ThongBao("Xoá thành công!!!!", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                ThongBao("Xoá không thành công, vui lòng thử lại!!!", "error");
                return RedirectToAction("Index");
            }
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

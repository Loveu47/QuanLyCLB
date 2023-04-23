using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyCLB.Models;
using EntityState = System.Data.Entity.EntityState;

namespace QuanLyCLB.Controllers
{
    public class HoatDongsController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;
        // GET: HoatDongs
        public ActionResult Index()
        {
            if (acc.ToChucId == null)
            {
                var hoatDongs = db.HoatDongs.Include(h => h.ToChuc).OrderByDescending(j=>j.ThoiGian);
                return View("Index", hoatDongs.ToList());
            }
            else
            {
                var hoatDongs = db.HoatDongs.Include(h => h.ToChuc).Where(j => j.ToChucId == acc.ToChucId);
                return View("QuanLy", hoatDongs.ToList());
            }
        }
        public ActionResult ChiTiet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hd = db.HoatDongs.Find(id);
            if (hd == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(hd);
            }
        }

        public ActionResult Create()
        {
            return View();
        }
        // POST: HoatDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Id,ToChucId,NoiDung,ThoiGian,TieuDe")] HoatDong hoatDong, HttpPostedFileBase AnhChinh)
        {
            if (AnhChinh != null && AnhChinh.ContentLength > 0)
            {
                string _fn = Path.GetFileName(AnhChinh.FileName);
                string path = Path.Combine(Server.MapPath("/Content/upload/"), _fn);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    AnhChinh.SaveAs(path);
                }
                else
                {
                    AnhChinh.SaveAs(path);
                }
                hoatDong.AnhChinh = "/Content/upload/" + _fn;
            }
            if (ModelState.IsValid)
            {
                db.HoatDongs.Add(hoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoatDong);
        }

        // GET: HoatDongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hoatDong = db.HoatDongs.Find(id);
            if (hoatDong == null)
            {
                return HttpNotFound();
            }
            return View(hoatDong);
        }

        // POST: HoatDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,ToChucId,NoiDung,ThoiGian,TieuDe")] HoatDong hoatDong, HttpPostedFileBase Logo)
        {
            if (Logo != null && Logo.ContentLength > 0)
            {
                string _fn = Path.GetFileName(Logo.FileName);
                string path = Path.Combine(Server.MapPath("/Content/upload/"), _fn);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    Logo.SaveAs(path);
                }
                else
                {
                    Logo.SaveAs(path);
                }
                hoatDong.AnhChinh = "/Content/upload/" + _fn;
            } else
            {
                HoatDong hd = db.HoatDongs.Find(hoatDong.Id);
                hoatDong.AnhChinh = hd.AnhChinh;
            }
            if (ModelState.IsValid)
            {
                db.Set<HoatDong>().AddOrUpdate(hoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hoatDong);
        }

        // GET: HoatDongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hoatDong = db.HoatDongs.Find(id);
            if (hoatDong == null)
            {
                return HttpNotFound();
            }
            db.HoatDongs.Remove(hoatDong);
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

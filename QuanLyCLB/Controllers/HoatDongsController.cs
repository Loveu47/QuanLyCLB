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
    public class HoatDongsController : Controller
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;
        // GET: HoatDongs
        public ActionResult Index()
        {   if(acc.ToChucId == null)
            {
                var hoatDongs = db.HoatDongs.Include(h => h.ToChuc);
                return View("Index", hoatDongs.ToList());
                
                
            } else
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
        // GET: HoatDongs/Details/5
        public ActionResult Details(int? id)
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

        // GET: HoatDongs/Create
        public ActionResult Create()
        {
            ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten");
            return View();
        }

        // POST: HoatDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ToChucId,NoiDung,ThoiGian,TieuDe,AnhChinh")] HoatDong hoatDong)
        {
            if (ModelState.IsValid)
            {
                db.HoatDongs.Add(hoatDong);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten", hoatDong.ToChucId);
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
            ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten", hoatDong.ToChucId);
            return View(hoatDong);
        }

        // POST: HoatDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ToChucId,NoiDung,ThoiGian,TieuDe,AnhChinh")] HoatDong hoatDong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoatDong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ToChucId = new SelectList(db.ToChucs, "Id", "Ten", hoatDong.ToChucId);
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
            return View(hoatDong);
        }

        // POST: HoatDongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HoatDong hoatDong = db.HoatDongs.Find(id);
            db.HoatDongs.Remove(hoatDong);
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

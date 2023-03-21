﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using QuanLyCLB.Models;
using EntityState = System.Data.Entity.EntityState;

namespace QuanLyCLB.Controllers
{
    public class ToChucsController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();

        // GET: ToChucs
        public ActionResult Index()
        {
            return View(db.ToChucs.ToList());
        }

        // GET: ToChucs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToChucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ten,MoTa,NgayThanhLap,NgayGiaiThe")] ToChuc toChuc, HttpPostedFileBase Logo)
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
                    toChuc.Logo = "/Content/upload/" + _fn;
                }
                if (ModelState.IsValid)
                {
                    db.ToChucs.Add(toChuc);
                    db.SaveChanges();
                    ThongBao("Thêm mới thành công!!!", "success");
                    return RedirectToAction("Index");
                }
                ThongBao("Thêm thất bại!!!", "error");
                return View(toChuc);
        }

        // GET: ToChucs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToChuc toChuc = db.ToChucs.Find(id);
            if (toChuc == null)
            {
                return HttpNotFound();
            }
            return View(toChuc);
        }

        // POST: ToChucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ten,MoTa,NgayThanhLap")] ToChuc toChuc, HttpPostedFileBase Logo)
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
                toChuc.Logo = "/Content/upload/" + _fn;
            }
            if (ModelState.IsValid)
            {
                db.Entry(toChuc).State = EntityState.Modified;
                db.SaveChanges();
                ThongBao("Sửa thành công!!!", "success");
                return RedirectToAction("Index");
            }
            ThongBao("Sửa thất bại!!!", "error");
            return View(toChuc);
        }

        // GET: ToChucs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToChuc toChuc = db.ToChucs.Find(id);
            if (toChuc == null)
            {
                return HttpNotFound();
            } else
            {
                toChuc.NgayGiaiThe = DateTime.Now;
                db.ToChucs.AddOrUpdate();
                db.SaveChanges();
                ThongBao("Giải thể thành công!!!", "success");
            }
            return Redirect("~/ToChucs/Index");
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
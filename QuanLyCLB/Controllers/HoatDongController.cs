using QuanLyCLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCLB.Controllers
{
    public class HoatDongController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        // GET: HoatDong
        public ActionResult Index()
        {
            return View(db.HoatDongs.ToList());
        }

        public ActionResult ChiTiet(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoatDong hd = db.HoatDongs.Find(id);
            if(hd == null)
            {
                return HttpNotFound();
            } else
            {
                return View(hd);
            }
        }
    }
}
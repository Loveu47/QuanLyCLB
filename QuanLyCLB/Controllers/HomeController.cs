using QuanLyCLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCLB.Controllers
{
    public class HomeController : ParentsController
    {
        private QuanLyCLBEntities db = new QuanLyCLBEntities();
        public ActionResult Index()
        {
            TempData["TongSo"] = db.ToChucs.Count();
            TempData["ThanhVien"] = db.ThanhViens.Count();
            TempData["HoatDong"] = db.HoatDongs.Count();
            TempData["CanBo"] = db.TaiKhoans.Count();
            return View();
        }
        public ActionResult TrangCaNhan()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
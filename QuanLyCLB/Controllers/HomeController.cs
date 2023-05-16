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
        TaiKhoan acc = System.Web.HttpContext.Current.Session["Login"] as TaiKhoan;
        public ActionResult Index()
        {   if(acc.QLCapCao == true)
            {
                TempData["TongSo"] = db.ToChucs.Count();
                TempData["ThanhVien"] = db.ThanhViens.Count();
                TempData["HoatDong"] = db.HoatDongs.Count();
                TempData["CanBo"] = db.TaiKhoans.Count();
                return View("Index");
            } else
            {
                TempData["QuanLy"] = db.TaiKhoans.Where(j => j.ToChucId == acc.ToChucId).Count();
                TempData["ThanhVien"] = db.ThanhViens.Where(j=>j.ToChucId == acc.ToChucId).Count();
                TempData["HoatDong"] = db.HoatDongs.Where(j => j.ToChucId == acc.ToChucId).Count();
                TempData["Ban"] = db.Bans.Where(j => j.ToChucId == acc.ToChucId).Count();
                return View("Dashboard");
            }
        }
        public ActionResult TrangCaNhan()
        {
            return View();
        }
    }
}
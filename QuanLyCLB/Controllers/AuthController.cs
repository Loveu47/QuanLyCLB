using QuanLyCLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCLB.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        QuanLyCLBEntities myObj = new QuanLyCLBEntities();
        public TaiKhoan acc;
        // GET: Admin/Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["account"];
            var mk = collection["pass"];
            //mk = GetMD5(mk);
            if (string.IsNullOrEmpty(tendn))
            {
                ViewBag.ThongBao = "Vui lòng nhập tên đăng nhập !!!";
            }
            else if (string.IsNullOrEmpty(mk))
            {
                ViewBag.ThongBao = "Nhập mật khẩu !!!";
            }
            else
            {   

                acc = myObj.TaiKhoans.SingleOrDefault(i => i.TaiKhoan1 == tendn && i.MatKhau == mk);
                if (acc != null)
                {
                    Session["Login"] = acc;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.ThongBao = "Tài khoản hoặc mật khẩu không chính xác!!!";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Login"] = " ";
            return Redirect("~/Auth/Login");
        }
    }
}
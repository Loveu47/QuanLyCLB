using QuanLyCLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCLB.Controllers
{
    public class AuthController : Controller
    {   
        QuanLyCLBEntities db = new QuanLyCLBEntities();
        // GET: Auth
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

        public ActionResult QuenMatKhau()
        {
            Session["Login"] = " ";
            return View();
        }
        [HttpPost]
        public ActionResult QuenMatKhau(string Email)
        {
            // Kiểm tra xem email có tồn tại trong CSDL không
            var user = db.TaiKhoans.FirstOrDefault(u => u.Email == Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Email không tồn tại trong hệ thống");
                return View();
            }
            // Tạo mã xác nhận ngẫu nhiên
            var code = new Random().Next(100000, 999999);

            // Gửi email chứa mã xác nhận đến địa chỉ email của người dùng
            var message = new MailMessage();
            message.To.Add(Email);
            message.Subject = "Cấp lại mật khẩu";
            message.Body = "Tài khoản: "+ user.TaiKhoan1+ " .Mã xác nhận của bạn là: " + code;

            var smtp = new SmtpClient();
            smtp.Send(message); 
            user.Code = code;
            db.SaveChanges();
            return RedirectToAction("ResetPassword");
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(int? code, string newPassword)
        {
            // Kiểm tra xem mã xác nhận có đúng không
            var user = db.TaiKhoans.FirstOrDefault(u => u.Code == code);
            if (user == null)
            {
                ModelState.AddModelError("code", "Mã xác nhận không đúng");
                return View();
            }

            // Cập nhật mật khẩu mới cho người dùng
            user.MatKhau = newPassword;
            user.Code = null;
            db.SaveChanges();
            TempData["Tb"] = "Đặt lại mật khẩu thành công!!!";
            return RedirectToAction("Login");
        }

    }
}
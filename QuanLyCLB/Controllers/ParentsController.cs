using QuanLyCLB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QuanLyCLB.Controllers
{
    public class ParentsController : Controller
    {
        QuanLyCLBEntities db = new QuanLyCLBEntities();
        // GET: Parents
        public ParentsController()
        {
            if (System.Web.HttpContext.Current.Session["Login"].Equals(" "))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Auth/Login");
            }
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        protected void ThongBao(string message, string type)
        {
            TempData["ThongBao"] = message;
            if (type == "success")
            {
                TempData["Type"] = "alert-inv-success";
            }
            else if (type == "warning")
            {
                TempData["Type"] = "alert-inv-waring";
            }
            else if (type == "error")
            {
                TempData["Type"] = "alert-inv-danger";
            }
        }
        public bool QL(int id)
        {
            TaiKhoan ql = db.TaiKhoans.Find(id);
            if (ql.QLCapCao == true)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
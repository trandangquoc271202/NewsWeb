using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class RegisterController : Controller

    {
        // GET: Register
        private Model1 db = new Model1();
        public ActionResult Register()
        {
            Users user = Session["userRe"] as Users;
            if (user != null)
            {
                Session.Remove("userRe");
                Session.Remove("code");
            }
            return View();
        }
        [HttpPost]

        public ActionResult RegisterUser(FormCollection form)
        {
            string email = form["email"];
            string password = form["password"];
            string rePassword = form["re_password"];

            if (password.Equals(rePassword))
            {
                // Check if the user already exists in the database
                bool userExists = db.Users.Any(u => u.email == email);

                if (userExists)
                {
                    ViewBag.ErrorMessage = "User with this email already exists.";
                }
                else
                {

                    int atIndex = email.IndexOf('@');
                    string username = atIndex != -1 ? email.Substring(0, atIndex) : email;
                    Users newUser = new Users
                    {
                        name = username,
                        email = email,
                        password = password,
                        permission = "user",
                        status = true,

                    };

                    String xacNhan = RandomNumber();
                    Session["userRe"] = newUser;
                    Session["code"] = xacNhan;
                    Session["index"] = 5;
                    EmailSender e = new EmailSender();
                    e.SendEmail(email, "Xác nhận đăng ký", "Mã xác nhận của bạn là: " + xacNhan);
                    return View("ConfirmCode");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Passwords do not match.";
            }

            return View("Register");
        }
        public string RandomNumber()
        {
            Random random = new Random();
            string result = "";

            for (int i = 0; i < 6; i++)
            {
                result += random.Next(0, 10).ToString();
            }

            return result;
        }
        public ActionResult ConfirmCode()
        {
            
            return View();
        }

        public ActionResult SendAgainCode()
        {
            Users userExists = Session["userRe"] as Users;
            String code = RandomNumber();
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(userExists.email, "Mã xác nhận lấy lại mật khẩu", "Mã của bạn là: " + code);
            Session["code"] = code;
            Session["index"] = 5;
            return View("ConfirmCode");
        }

        [HttpPost]

        public ActionResult RegisterConfirm(FormCollection form)
        {
            String codeConfrim = form["codeConfirm"];
            String code = Session["code"] as String;
            Users user = Session["userRe"] as Users;
            if (codeConfrim.Equals(code))
            {
                db.Users.Add(user);
                db.SaveChanges();
                Session.Remove("code");
                Session.Remove("userRe");
                Session.Remove("index");
                Session["LoggedInUser"] = user;
                return RedirectToAction("Index", "Home");
            }
            int index = (int)Session["index"];
            if(index == 0)
            {
                Session.Remove("code");
                Session.Remove("userRe");
                Session.Remove("index");
                ViewBag.ErrorMessage = "Bạn đã nhập quá số lần cho phép.";
                return View("Register");
            }
            index--;
            Session["index"] = index;
            ViewBag.ErrorMessage = "Số lần nhập sai còn lại của bạn: " + index;
            return View("ConfirmCode");
        }
    }
}
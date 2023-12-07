using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace NewsWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Model1 db = new Model1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string email = form["email"];
            string password = form["password"];
        
            Users user = db.Users.FirstOrDefault(c => c.email == email && c.password == password);

            if (user != null)
            {

               

                Session["LoggedInUser"] = user;
                if (user.permission.Trim() == "admin")
                {

                    return RedirectToAction("HomeAdmin", "HomeAdmin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Email hoặc mật khẩu không đúng";
                return View();
            }
        }
        public ActionResult LogOut()
        {
            var loggedInUser = Session["LoggedInUser"] as NewsWeb.Models.Users;
            if (loggedInUser !=null)
            {
                Session.Remove("LoggedInUser");
                Session.Abandon();

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost] 
        public ActionResult CheckEmailFG(FormCollection form)
        {
            String email = form["emailForgot"];
            Users userExists = db.Users.FirstOrDefault(u => u.email.Equals(email));

            if (userExists != null)
            {   
                String code = RandomNumber();
                EmailSender emailSender = new EmailSender();
                emailSender.SendEmail(userExists.email, "Mã xác nhận lấy lại mật khẩu", "Mã của bạn là: " + code);
                Session["userFG"] = userExists;
                Session["codeFG"] = code;
                Session["indexFG"] = 5;
                return View("ConfirmPassFG");
            }
            ViewBag.ErrorMessage = "Email không tồn tại";
            return View("ForgotPassword");
        }

        public ActionResult SendAgainCode()
        {
            Users userExists = Session["userFG"] as Users;
            String code = RandomNumber();
            EmailSender emailSender = new EmailSender();
            emailSender.SendEmail(userExists.email, "Mã xác nhận lấy lại mật khẩu", "Mã của bạn là: " + code);
            Session["codeFG"] = code;
            Session["indexFG"] = 5;
            return View("ConfirmPassFG");
        }

        public ActionResult ConfirmPassFG()
        {
            Users user = Session["userFG"] as Users;
            if(user != null)
            {
                return View();
            } else
            {
                return View("Login");
            }
            
        }

        public ActionResult ChangePasswrodFG()
        {
            Users user = Session["userFG"] as Users;
            if (user != null)
            {
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Người dùng không hợp lệ";
                return View("Login");
            }
        }

        [HttpPost]

        public ActionResult ConfirmChangePasswordFG(FormCollection form)
        {
            Users user = Session["userFG"] as Users;

            if (user != null)
            {
                string newPassword = form["passwordCP"];
                string confirmPassword = form["re_passwordCP"];

                if (!string.IsNullOrEmpty(newPassword) && newPassword.Equals(confirmPassword, StringComparison.Ordinal))
                {          
                    using (var dbContext = new Model1()) 
                    {
                        Users userToUpdate = dbContext.Users.Find(user.id);
                        if (userToUpdate != null)
                        {
                            userToUpdate.password = newPassword.Trim();
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Không tìm thấy người dùng trong cơ sở dữ liệu";
                            return View("Login");
                        }
                    }
                    Session.Remove("userFG");
                    return View("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "Mật khẩu không hợp lệ";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Người dùng không hợp lệ";
            }
            return View("Login");
        }


        [HttpPost]
        public ActionResult ConfirmCodePassFG(FormCollection form)
        {
            String codeConfrim = form["codeConfirmFG"];
            String code = Session["codeFG"] as String;

            if (codeConfrim.Equals(code))
            {
                Session.Remove("codeFG");
                Session.Remove("indexFG");
                return View("ChangePasswrodFG");
            }
            else
            {
                int index = (int)Session["indexFG"];
                if (index == 0)
                {
                    Session.Remove("codeFG");
                    Session.Remove("userFG");
                    Session.Remove("indexFG");
                    ViewBag.ErrorMessage = "Bạn đã nhập quá số lần cho phép.";
                    return View("Login");
                }
                index--;
                Session["indexFG"] = index;
                ViewBag.ErrorMessage = "Số lần nhập sai còn lại của bạn: " + index;
                return View("ConfirmPassFG");
            }
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
        
    }

}
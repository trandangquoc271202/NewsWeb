using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsWeb.Models;

namespace NewsWeb.Controllers
{
    public class InfoUserController : Controller
    {
        // GET: InfoUser
        Model1 db = new Model1();
        public ActionResult InfoUser()
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            if (loggedInUser != null)
            {

                return View();
            }
            return RedirectToAction("News", "News");
        }

        public ActionResult ChangeInfo()
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            if (loggedInUser != null)
            {

                return View();
            }
            return RedirectToAction("News", "News");
        }
        [HttpPost]
        public ActionResult SubmitChangeInfo(FormCollection form)
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            String name = form["name"];
            String email = form["email"];
            String phone = form["phone"];
            String address = form["address"];
            String birthdate = form["birthdate"];
            if (loggedInUser != null)
            {
                Users userToUpdate = db.Users.Find(loggedInUser.id);
                if (userToUpdate != null)
                {
                    userToUpdate.name = name;
                    userToUpdate.email = email;
                    userToUpdate.phone = phone;
                    userToUpdate.address = address;
                    userToUpdate.birthDay = birthdate;
                    db.SaveChanges();
                    loggedInUser.name = name;
                    loggedInUser.email = email;
                    loggedInUser.phone = phone;
                    loggedInUser.address = address;
                    loggedInUser.birthDay = birthdate;
                    Session["LoggedInUser"] = loggedInUser;
                    return View("InfoUser");
                }
                else
                {
                    ViewBag.ErrorMessage = "Không tìm thấy người dùng trong cơ sở dữ liệu";
                    return View("ChangeInfo");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

        }
        public ActionResult ChangePassword()
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            if (loggedInUser != null)
            {

                return View();
            }
            return RedirectToAction("News", "News");
        }

        public ActionResult ConfirmChangePasswordFG(FormCollection form)
        {
            Users user = Session["LoggedInUser"] as Users;

            if (user != null)
            {
                string newPassword = form["password"];
                string confirmPassword = form["re_password"];

                if (!string.IsNullOrEmpty(newPassword) && newPassword.Equals(confirmPassword, StringComparison.Ordinal))
                {
                    using (var dbContext = new Model1())
                    {
                        Users userToUpdate = dbContext.Users.Find(user.id);
                        if (userToUpdate != null)
                        {
                            userToUpdate.password = newPassword.Trim();
                            Session["LoggedInUser"] = userToUpdate;
                            dbContext.SaveChanges();
                            ViewBag.ErrorMessage = "Thay đổi thành công";
                            return View("ChangePassword");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "Không tìm thấy người dùng trong cơ sở dữ liệu";
                            return View("ChangePassword");
                        }
                    }
                  
                  
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
            return View("ChangePassword");
        }
        public ActionResult History()
        {
            return View();

        }
        public ActionResult Hobby()
        {
            return View();
        }

    }

}
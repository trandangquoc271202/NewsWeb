﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
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
        public ActionResult History()
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            List<HistoryNews> list = db.HistoryNews
            .Where(item => item.UserID == loggedInUser.id)
            .OrderBy(item => item.Time)
            .ToList();
            list = list.OrderByDescending(item => item.Time).ToList();

            foreach (var item in list)
            {
                string htmlContent;
                using (WebClient webClient = new WebClient())
                {
                    htmlContent = webClient.DownloadString(item.Link);
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                // Sử dụng XPath để chọn phần tử h1 có class là 'title-detail'
                HtmlNode detailNode = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'title-detail')]");

                // Lấy nội dung của phần tử h1 (innerText)
                string detail = detailNode?.InnerText;

                // Gán nội dung đã lấy vào thuộc tính Link của đối tượng HistoryNews
                item.Link = detail;
            }

            return View(list);
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
                Users userToUpdate = db.Users.FirstOrDefault(c => c.id == loggedInUser.id);

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
      

    }
   

}
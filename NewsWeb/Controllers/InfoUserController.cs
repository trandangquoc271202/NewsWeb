using System;
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

            List<HistoryInfomation> result = new List<HistoryInfomation>(); // Corrected list initialization

            foreach (var item in list)
            {
                string htmlContent;
                using (WebClient webClient = new WebClient())
                {
                    htmlContent = webClient.DownloadString(item.Link);
                }

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                HtmlNode detailNode = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'title-detail')]");
                string title = detailNode?.InnerText;

                HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'fig-picture')]//img[contains(@class, 'lazy') or contains(@class, 'lazied')]");
                string imageUrl = imageNode?.GetAttributeValue("data-src", "");
                HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'description')]");
                string description = descriptionNode?.InnerText;

                // Create a new HistoryInfomation object and add it to the result list
                HistoryInfomation historyInfo = new HistoryInfomation
                {
                    title = title,
                    image = imageUrl,
                    description = description,
                    Time = item.Time
                };

                result.Add(historyInfo);
            }

            // Pass the result list to the view
            return View(result);
        }        
        public ActionResult Favorite(String id)
        {
            var loggedInUser = Session["LoggedInUser"] as NewsWeb.Models.Users;
            if (loggedInUser != null)
            {
                Favorite favorite = new Favorite
                {
                    UserID = loggedInUser.id,
                    Link = id,
                    Time = DateTime.Now,
                };
                db.Favorite.Add(favorite);
                db.SaveChanges();
            }

            return Redirect("/Detail/Detail");
        }


        public ActionResult ListFavorite()
        {
            Users loggedInUser = Session["LoggedInUser"] as Users;
            List<Favorite> list = db.Favorite
                .Where(item => item.UserID == loggedInUser.id)
                .OrderBy(item => item.Time)
                .ToList();

            List<ListFavorite> result = new List<ListFavorite>(); // Use a ViewModel

            foreach (var item in list)
            {
                string htmlContent = "";
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        htmlContent = webClient.DownloadString(item.Link);
                    }

                    HtmlDocument doc = new HtmlDocument();
                    doc.LoadHtml(htmlContent);

                    HtmlNode detailNode = doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'title-detail')]");
                    string title = detailNode?.InnerText;

                    HtmlNode imageNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'fig-picture')]/img");
                    string imageUrl = imageNode?.GetAttributeValue("src", "");

                    HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'description')]");
                    string description = descriptionNode?.InnerText;

                    ListFavorite listFavorite = new ListFavorite
                    {
                        Title = title,
                        Image = imageUrl,
                        Description = description,
                        Time = item.Time
                    };

                    result.Add(listFavorite);
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Error downloading content for {item.Link}: {ex.Message}");
                }
            }

            return View(result);
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
using HtmlAgilityPack;
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class DetailController : Controller
    {
        Model1 db = new Model1();
        // GET: Detail
        public ActionResult Detail()
        {
           
            string content = "";
            string url = Request["id"];

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            HtmlNode sidebarNode = doc.DocumentNode.SelectSingleNode("//div[@class='sidebar-1']");

            if (sidebarNode != null)
            {
                content = sidebarNode.InnerHtml.Replace("data-src", "src");
            }
            var loggedInUser = Session["LoggedInUser"] as NewsWeb.Models.Users;
            if(loggedInUser != null)
            {
                HistoryNews his = new HistoryNews
                {
                    UserID = loggedInUser.id,
                    Link = url,
                    Time = DateTime.Now,
                  
                };
                db.HistoryNews.Add(his);
                db.SaveChanges();
            }
            var query = from comment in db.Comments
                        join user in db.Users on comment.idUser equals user.id
                        where comment.link == url && comment.allow == true
                        orderby comment.dateTimeComment descending
                        select new DetailComment
                        {
                            id = comment.id,
                            nameUser = user.name,
                            idUser = comment.idUser,
                            message = comment.message
                        };

            // Thực hiện truy vấn và lấy kết quả
            var result = query.ToList();

            ViewBag.ContentObject = (object)content;
            ViewBag.link = url;
            ViewBag.ItemList = result;
            ViewBag.ContentText = getContentText(content);
            return View();
        }

        public string getContentText(string content)
        {
            string result = "";
            string htmlContent = @"" + content;

            // Sử dụng HtmlAgilityPack để phân tích cú pháp HTML
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlContent);

            // Lấy tất cả các thẻ <p>
            HtmlNodeCollection paragraphs = htmlDocument.DocumentNode.SelectNodes("//h1");
            if (paragraphs != null)
            {
                // Lấy nội dung của thẻ <h1>
                foreach (HtmlNode paragraph in paragraphs)
                {
                    string paragraphContent = paragraph.InnerText;
                    result += paragraphContent;

                }
            }
            paragraphs = htmlDocument.DocumentNode.SelectNodes("//p");
            // Kiểm tra xem có thẻ <p> hay không
            if (paragraphs != null)
            {
                // Lặp qua từng thẻ và in ra nội dung
                foreach (HtmlNode paragraph in paragraphs)
                {
                    string paragraphContent = paragraph.InnerText;
                    result += paragraphContent;
                   
                }
            }
            return result;
        } 
        [HttpPost]
        public ActionResult Comment(FormCollection form)
        {
            string comment = form["comment"];
            string url = form["link"];
            Users user = Session["LoggedInUser"] as Users;
            if (user != null)
            {
                Comment commentNews = new Comment
                 {
                idUser = user.id,
                link = url,
                 message = comment,
                    dateTimeComment = DateTime.Now,
                allow = true
                };
                db.Comments.Add(commentNews);
                db.SaveChanges();
                return Redirect("/Detail/Detail?id="+url);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult RemoveComment(FormCollection form)
        {
            string id = form["id"];
            string url = form["link"];

            var teacher = db.Comments.Find(int.Parse(id));
            if (teacher != null)
            {
                db.Comments.Remove(teacher);
                db.SaveChanges();
            }

            return Redirect("/Detail/Detail?id=" + url);
        }
    }
}
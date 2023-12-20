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
            ViewBag.ContentObject = (object)content;
            ViewBag.link = url;

            return View();
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
                    dateTimeComment = DateTime.Now
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
    }
}
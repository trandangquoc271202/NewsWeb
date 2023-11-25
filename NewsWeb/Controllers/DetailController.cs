using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Detail()
        {
           
            string content = "";
            string url = "https://vnexpress.net/khoi-nghiep-tu-long-ga-4681261.html";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);

            HtmlNode sidebarNode = doc.DocumentNode.SelectSingleNode("//div[@class='sidebar-1']");

            if (sidebarNode != null)
            {
                content = sidebarNode.InnerHtml;
               
            }
            
            return View((object)content);
        }
    }
}
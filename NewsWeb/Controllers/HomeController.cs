using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/tin-moi-nhat.rss");

            return View(list);
        }

        
    }
}
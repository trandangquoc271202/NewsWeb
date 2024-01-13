using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult News()
        {
            List<RssItem> list = new List<RssItem>();
            ConvertRssToList convertRssToList = new ConvertRssToList();
            list = convertRssToList.GetRssItems("https://vnexpress.net/rss/thoi-su.rss");

            return View(list);
        }
      
    }
}
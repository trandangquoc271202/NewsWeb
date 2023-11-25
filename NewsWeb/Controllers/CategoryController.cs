using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult World()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/the-gioi.rss");

            return View(list);
        }
        public ActionResult News()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/thoi-su.rss");

            return View(list);
        }
        public ActionResult Business()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/kinh-doanh.rss");

            return View(list);
        }
        public ActionResult StartUp()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/the-gioi.rss");

            return View(list);
        }
        public ActionResult Entertainment()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/giai-tri.rss");

            return View(list);
        }
        public ActionResult Sport()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/the-thao.rss");

            return View(list);
        }
        public ActionResult Law()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/phap-luat.rss");

            return View(list);
        }
        public ActionResult Education()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/giao-duc.rss");

            return View(list);
        }
        public ActionResult Health()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/suc-khoe.rss");

            return View(list);
        }
        public ActionResult Life()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/gia-dinh.rss");

            return View(list);
        }
        public ActionResult Travel()
        {
            ConvertRssToList convertRssToList = new ConvertRssToList();

            List<RssItem> list = convertRssToList.GetRssItems("https://vnexpress.net/rss/du-lich.rss");

            return View(list);
        }
    }
}
using NewsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NewsWeb.Controllers
{
    public class SearchController : Controller
    {
        [HttpPost]
        public ActionResult Search(string title)
        {
            List<RssItem> result = new List<RssItem>();
            ConvertRssToList convertRssToList = new ConvertRssToList();
            HashSet<RssItem> mySet = new HashSet<RssItem>();

            string[] rss =
            {
                "https://vnexpress.net/rss/the-gioi.rss",
                "https://vnexpress.net/rss/tin-moi-nhat.rss",
                "https://vnexpress.net/rss/thoi-su.rss",
                "https://vnexpress.net/rss/kinh-doanh.rss",
                "https://vnexpress.net/rss/giai-tri.rss",
                "https://vnexpress.net/rss/the-thao.rss",
                "https://vnexpress.net/rss/phap-luat.rss",
                "https://vnexpress.net/rss/giao-duc.rss",
                "https://vnexpress.net/rss/suc-khoe.rss",
                "https://vnexpress.net/rss/gia-dinh.rss",
                "https://vnexpress.net/rss/du-lich.rss"
            };
            foreach (string rssItem in rss)
            {
                List<RssItem> list = convertRssToList.GetRssItems(rssItem);

                // Lọc danh sách RssItem dựa trên tiêu đề chứa từ khóa
                var filteredList = list.Where(item => item.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) != -1).ToList();

                // Thêm các phần tử từ danh sách đã lọc vào mySet
                foreach (RssItem item in filteredList)
                {
                    mySet.Add(item);
                }
            }

            // Bây giờ mySet chứa tất cả các RssItem có Title chứa từ khóa
            result.AddRange(mySet); // Chuyển đổi HashSet thành List và thêm vào danh sách kết quả

            return View(result);
        }
    }
}
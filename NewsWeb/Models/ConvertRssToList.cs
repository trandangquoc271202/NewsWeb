using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Xml;

namespace NewsWeb.Models
{
    public class ConvertRssToList
    {
        public List<RssItem> GetRssItems(string rssFeedUrl)
        {
            List<RssItem> rssItems = new List<RssItem>();

            // Sử dụng XmlReader để đọc RSS feed
            using (XmlReader reader = XmlReader.Create(rssFeedUrl))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                if (feed != null)
                {
                    // Lặp qua mỗi mục trong RSS feed
                    foreach (SyndicationItem item in feed.Items)
                    {
                        RssItem rssItem = new RssItem
                        {
                            Title = item.Title?.Text,
                            Description = convertDescription(item.Summary?.Text),
                            Link = item.Links.FirstOrDefault()?.Uri?.ToString(),
                            PublishDate = item.PublishDate.DateTime,
                            Image = convertImage(item.Summary?.Text)
                        };

                        rssItems.Add(rssItem);
                    }
                }
            }

            return rssItems;
        }
        public static string convertImage(string description)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(description);
            HtmlNode imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img");
            string srcValue = imgNode.GetAttributeValue("src", "");
            return srcValue;
        }
        public static string convertDescription(string description)
        {
            int index = description.IndexOf("</br>") + 5;
            string result = description.Substring(index);
            return result;
        }
    }
}
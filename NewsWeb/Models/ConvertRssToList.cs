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
        static List<RssItem> GetRssItems(string rssFeedUrl)
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
                            Description = item.Summary?.Text,
                            Link = item.Links.FirstOrDefault()?.Uri?.ToString(),
                            PublishDate = item.PublishDate.DateTime
                        };

                        rssItems.Add(rssItem);
                    }
                }
            }

            return rssItems;
        }
    }
}
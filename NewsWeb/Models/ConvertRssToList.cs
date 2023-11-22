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

            try
            {
                using (XmlReader reader = XmlReader.Create(rssFeedUrl))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);

                    if (feed != null)
                    {
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
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed
                Console.WriteLine($"Error processing RSS feed: {ex.Message}");
            }

            return rssItems;
        }

        public static string convertImage(string description)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(description);

            HtmlNode imgNode = htmlDocument.DocumentNode.SelectSingleNode("//img");

            if (imgNode != null)
            {
                string srcValue = imgNode.GetAttributeValue("src", "");
                return srcValue;
            }
            else
            {
                // Return a default value when no imgNode is found
                return "https://s1.vnecdn.net/vnexpress/restruct/i/v830/default/thumb_1000.jpg";
            }
        }


        public static string convertDescription(string description)
        {
            int index = description.IndexOf("</br>") + 5;
            string result = description.Substring(index);
            return result;
        }
    }
}
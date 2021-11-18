using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.BackgroundService
{
    public static class TopAppLinkManager
    {
        public static async Task<List<HtmlNode>> GetTopAppsFromPlayStoreUrl(string url)
        {
            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();

            var html = await httpClient.GetStringAsync(url);

            htmlDocument.LoadHtml(html);

            var appHtml = htmlDocument.DocumentNode.Descendants("div").Where(node =>
                node.GetAttributeValue("class", "").Equals("ZmHEEd ")).ToList();

            //Take Top 10 Apps
            var appListHtmlNode = appHtml[0].Descendants("div").Where(n =>
                n.GetAttributeValue("class", "").Contains("ImZGtf ")).Take(10).ToList();

            return appListHtmlNode;
        }

        public static Task<List<string>> GetAppsUrlFromHtml(List<HtmlNode> appHtmlListLinks)
        {
            var list = new List<string>();

            foreach (var appHtmlLink in appHtmlListLinks)
            {
                var appLinkList = appHtmlLink.Descendants("a").Where(link =>
                        link.GetAttributeValue("href", "").Contains("/store/apps/details?id"));

                foreach (var appLink in appLinkList)
                {
                    if (!list.Contains(appLink.GetAttributeValue("href", "")))
                    {
                        list.Add(appLink.GetAttributeValue("href", ""));
                    }
                }
            }

            return Task.FromResult(list);
        }
    }
}

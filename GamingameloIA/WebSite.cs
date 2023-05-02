using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Collections.Specialized.BitVector32;

namespace GamingameloIA
{
    internal class WebSite
    {
        public void GetInformationsSite(string url)
        {
            var lastThree = GetLatestNews(url);
            if(lastThree.Count < 3)
            {
                //Error Code..
            }

            foreach(string link in lastThree)
            {
                var response = GetStructureSite(link);

                int start = response.IndexOf("<section class=\"content-wrapper\"");
                int end = response.IndexOf("</section>", start);
                string articleHtml = response.Substring(start, end - start);

                string article = "";
                string startFilter = "<p>";
                string endFilter = "</p>";
                while (true)
                {
                    if (!articleHtml.Contains(startFilter))
                        break;

                    int startIndex = articleHtml.IndexOf(startFilter) + startFilter.Length;
                    int endIndex = articleHtml.IndexOf(endFilter, startIndex);
                    article += articleHtml.Substring(startIndex, endIndex - startIndex);

                    articleHtml = articleHtml.Remove(startIndex - startFilter.Length, endIndex - startIndex + endFilter.Length);
                }

                article = CleanUpArticle(article);
            }
        }

        private string CleanUpArticle(string article)
        {
            string startFilter = "<a";
            string endFilter = "</a>";

            while (true)
            {
                if (!article.Contains(startFilter))
                    break;

                int startIndex = article.IndexOf(startFilter);
                int endIndex = article.IndexOf(endFilter, startIndex);
                string a = article.Substring(startIndex, endIndex - startIndex + endFilter.Length);

                int startA = article.IndexOf("<u>") + "<u>".Length;
                int endA = article.IndexOf("</u>", startIndex);
                string val = article.Substring(startA, endA - startA);

                article = article.Replace(a, val);

                if (article.Contains("<span"))
                {
                    int startSpan = article.IndexOf("<span");
                    int endSpan = article.IndexOf("</span>", startSpan);

                    article = article.Remove(startSpan, endSpan - startSpan + 7);

                    article = article.Replace("<em>", "").Replace("</em>", "");
                }
            }

            return article;
        }
        private List<string> GetLatestNews(string url)
        {
            var response = GetStructureSite(url);
            string section = response.Substring(response.IndexOf("<section data-next=\"latest\""), response.IndexOf("<div class=\"static-lightbox3 ad-unit\">") - response.IndexOf("<section data-next=\"latest\""));

            List<string> lastThree = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                int startIndex = section.IndexOf("href=\"") + 6;
                int endIndex = section.IndexOf("\"", startIndex + 1);
                string link = section.Substring(startIndex, endIndex - startIndex);

                lastThree.Add(link);

                section = section.Remove(startIndex - 7, endIndex - startIndex + 7);
            }

            return lastThree;
        }

        private string GetStructureSite(string url)
        {
            using (var webClient = new WebClient())
            {
                var response = webClient.DownloadString(url);
                Console.WriteLine(response);

                return response;
            }
        }
    }
}

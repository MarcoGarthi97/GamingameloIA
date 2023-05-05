using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingameloIA
{
    public class Article
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string WebSite { get; set; }
        public string Text { get; set; }
        public string Summarize { get; set; }
        public DateTime Date { get; set; }
        public bool Published { get; set; }

        public Article(string webSite, string text, string summarize, DateTime date, bool published)
        {
            WebSite = webSite;
            Text = text;
            Summarize = summarize;
            Date = date;
            Published = published;
        }
    }

    public class Sites
    {
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string NewsPage { get; set; }

        public Sites (string name, string link, string newsPage)
        {
            Name = name;
            Link = link;
            NewsPage = newsPage;
        }
    }
}

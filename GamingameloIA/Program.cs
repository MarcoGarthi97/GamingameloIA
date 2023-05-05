using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingameloIA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Meta meta = new Meta();
            WebSite site = new WebSite();
            MongoDB mongo = new MongoDB();
            //string id = meta.CreateContainer("https://www.lapaginaonline.it/giornale/wp-content/uploads/2018/12/Attachment-1.jpeg", "My mum <3");
            //string insert = meta.PublicContainer(id);

            List<Article> latesArticlesGamesRadar = site.GetInformationsSiteGamesRadar("https://www.gamesradar.com/news/games/");

            List<Article> articles = mongo.GetArticles();
            foreach (var articleGamesRadar in latesArticlesGamesRadar)
            {
                if(articles.Find(x => x.Text == articleGamesRadar.Text)  == null)
                    mongo.InsertArticle(articleGamesRadar);
            }

            string bestArticle = site.GetBestArticle(latesArticlesGamesRadar);
        }
    }
}

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
            WebSite webSite = new WebSite();
            MongoDB mongo = new MongoDB();
            OpenIA openIA = new OpenIA();

            var sites = mongo.GetSites();

            foreach(var site in sites)
            {
                List<Article> latesArticlesGamesRadar = webSite.GetInformationsSite(site);

                //Sistema rating o per scegliere l'articolo migliore così dà troppi problemi
                //string bestArticle = site.GetBestArticle(latesArticlesGamesRadar);
                string bestArticle = latesArticlesGamesRadar.OrderByDescending(x => x.Summarize.Length).First().Summarize;

                string url = openIA.Image(bestArticle);

                string id = meta.CreateContainer(url, bestArticle);
                string insert = meta.PublicContainer(id);
            }

            
        }
    }
}

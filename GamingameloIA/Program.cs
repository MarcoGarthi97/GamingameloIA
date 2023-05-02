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
            //Meta meta = new Meta();
            //string id = meta.CreateContainer("https://www.lapaginaonline.it/giornale/wp-content/uploads/2018/12/Attachment-1.jpeg", "My mum <3");
            //string insert = meta.PublicContainer(id);

            WebSite site = new WebSite();
            site.GetInformationsSite("https://www.gamesradar.com/news/games/");
        }
    }
}

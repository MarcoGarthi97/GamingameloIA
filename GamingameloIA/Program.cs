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
            string id = meta.CreateContainer("https%3A%2F%2Fhips.hearstapps.com%2Fhmg-prod%2Fimages%2Fhow-to-keep-ducks-call-ducks-1615457181.jpg%3Fcrop%3D0.669xw%3A1.00xh%3B0.166xw%2C0", "My mum <3");
            string insert = meta.PublicContainer(id);
        }
    }
}

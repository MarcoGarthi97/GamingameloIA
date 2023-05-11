using Newtonsoft.Json;
using RestSharp;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GamingameloIA
{
    internal class Meta
    {
        private string _link = "https://graph.facebook.com/v16.0/";
        private string _token = File.ReadAllLines(Properties.Settings.Default.MetaToken)[0];
        private string _profileID = File.ReadAllLines(Properties.Settings.Default.ProfileID)[0];

        public string CreateContainer(string link, string caption)
        {
            try 
            {
                Uri uri = new Uri(link);
                var client = new RestClient("https://graph.facebook.com/v16.0/" + _profileID + "/media");
                var request = new RestRequest();
                request.AddParameter("caption", caption);
                request.AddParameter("resize", "1200%3A*");
                request.AddParameter("access_token", _token);
                request.AddParameter("image_url", link);
                request.Method = Method.Post;
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                var val = JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Content);

                return val.First().Value;
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public string PublicContainer(string id)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest(_link + _profileID + "/media_publish?creation_id=" + id + "&access_token=" + _token, Method.Post);
                RestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

                return response.Content;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}

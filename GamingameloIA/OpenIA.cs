using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingameloIA
{
    internal class OpenIA
    {
        private string _bearer = File.ReadAllText(Properties.Settings.Default.OpenIA);
        public string Summarize(string command, string text)
        {
            string result = "";
            try
            {
                Summarized summarize = new Summarized("text-davinci-003", command + " " + text, 0.7, 1100, 1, 0, 0);

                string json = JsonConvert.SerializeObject(summarize);

                var client = new RestClient();
                var request = new RestRequest("https://api.openai.com/v1/completions", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + _bearer);
                var body = json;
                request.AddStringBody(body, DataFormat.Json);
                RestResponse response = client.Execute(request);

                var resultOpenIA = JsonConvert.DeserializeObject<ResponseSummarized>(response.Content);
                result = resultOpenIA.choices[0].text;

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }

    class Summarized
    {
        public string model { get; set; }
        public string prompt { get; set; }
        public double temperature { get; set; }
        public int max_tokens { get; set; }
        public int top_p { get; set; }
        public int frequency_penalty { get; set; }
        public int presence_penalty { get; set; }


        public Summarized(string model, string prompt, double temperature, int max_tokens, int top_p, int frequency_penalty, int presence_penalty)
        {
            this.model = model;
            this.prompt = prompt;
            this.temperature = temperature;
            this.max_tokens = max_tokens;
            this.top_p = top_p;
            this.frequency_penalty = frequency_penalty;
            this.presence_penalty = presence_penalty;
        }
    }

    class ResponseSummarized
    {
        public string id { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public List<ResponseSummarized_Choices> choices { get; set; }
        public ResponseSummarized_Usage usage { get; set; }
    }

    class ResponseSummarized_Choices
    {
        public string text { get; set; }
        public int index { get; set; }
        public string logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    class ResponseSummarized_Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}

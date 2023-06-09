﻿using GamingameloIA.Properties;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingameloIA
{
    internal class MongoDB
    {
        private static string connectionString = File.ReadAllText(Settings.Default.MongoDB);
        private IMongoDatabase GetDatabase()
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var mongoClient = new MongoClient(settings);

            return mongoClient.GetDatabase("GamingameloDB");
        }

        public List<Sites> GetSites()
        {
            List<Sites> sites = new List<Sites>();

            try
            {
                var intelligenziameloDB = GetDatabase();

                IMongoCollection<Sites> sitesAtlas = intelligenziameloDB.GetCollection<Sites>("Sites");

                sites = sitesAtlas.Aggregate().ToList();

                foreach (var site in sites)
                {
                    sites.Find(x => x.Name == site.Name).NewsScraping = GetNewsScraping(site.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return sites;
        }

        public NewsScraping GetNewsScraping(string name)
        {
            NewsScraping newsScraping = new NewsScraping();
            try
            {
                var intelligenziameloDB = GetDatabase();

                IMongoCollection<NewsScraping> newsScrapingAtlas = intelligenziameloDB.GetCollection<NewsScraping>("NewsScraping");

                newsScraping = newsScrapingAtlas.Find(x => x.Name == name).First();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return newsScraping;
        }

        public List<Article> GetArticles(string filter = null)
        {
            List<Article> articles = new List<Article>();

            try
            {
                var intelligenziameloDB = GetDatabase();

                IMongoCollection<Article> articlesAtlas = intelligenziameloDB.GetCollection<Article>("Articles");
                //List<Article> art = null;

                //if(filter != null)
                //    art = articlesAtlas.Find(x => x.Text != "").ToList();
                //else
                    //art = articlesAtlas.Find(x => x.Text == filter).ToList();

                List<Article> art = articlesAtlas.Find(x => x.Text != "").ToList();

                foreach (Article article in art)
                    articles.Add(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return articles;
        }

        public void InsertArticle(Article article)
        {
            try
            {
                IMongoDatabase intelligenziameloDB = GetDatabase();
                IMongoCollection<Article> articles = intelligenziameloDB.GetCollection<Article>("Articles");

                articles.InsertOne(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertArticle(List<Article> article)
        {
            try
            {
                IMongoDatabase intelligenziameloDB = GetDatabase();
                IMongoCollection<Article> articles = intelligenziameloDB.GetCollection<Article>("Articles");

                articles.InsertMany(article);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateArticles(Article article)
        {
            try
            {
                IMongoDatabase intelligenziameloDB = GetDatabase();
                IMongoCollection<Article> articles = intelligenziameloDB.GetCollection<Article>("Articles");

                var filter = Builders<Article>.Filter.Eq("Summarize", article.Summarize);

                var update = Builders<Article>.Update.Set("Published", true);
                var updateResult = articles.UpdateOne(filter, update);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}

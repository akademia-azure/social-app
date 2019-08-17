using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Configuration;
using SocialApp.MVC.Contracts;
using SocialApp.MVC.Models;

namespace SocialApp.MVC.Services
{
    public class SearchService : ISearchService
    {
        private readonly SearchServiceClient searchClient;
        private readonly ISearchIndexClient indexClient;
        private readonly IConfiguration configuration;

        public SearchService(IConfiguration configuration)
        {
            this.configuration = configuration;
            var serviceName = configuration.GetValue<string>("ConnectionStrings:SearchServiceName");
            var apiKey = configuration.GetValue<string>("ConnectionStrings:SearchServiceApiKey");

            var indexName = configuration.GetValue<string>("ConnectionStrings:SearchIndexName");

            searchClient = new SearchServiceClient(serviceName, new SearchCredentials(apiKey));
            indexClient = searchClient.Indexes.GetClient(indexName);
        }

        public SearchViewModel Search(SearchModel searchModel)
        {
            throw new NotImplementedException();
        }

        private string GenerateFilters(SearchModel searchModel)
        {
            throw new NotImplementedException();
        }

        private string GenerateLikeMoreFilter(int likeMore)
        {
            throw new NotImplementedException();
        }

        private string GenerateLikeLessFilter(int likeLess)
        {
            throw new NotImplementedException();
        }

        private string GenerateUserNameFilter(string userName)
        {
            throw new NotImplementedException();
        }

        private string PreparePhraseToSearch(string phrase)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Post> DeserializeResults(DocumentSearchResult<Document> response)
        {
            throw new NotImplementedException();
        }

        #region AUTOMATION

        public void GenerateSearch()
        {
            CreateDataSource();
            CreateIndex();
            CreateIndexer();
        }

        private void CreateDataSource()
        {
            string dataSourceName = "posts-source";
            string dbConnectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            var dataSource = DataSource.AzureSql(
                name: dataSourceName,
                sqlConnectionString: dbConnectionString,
                tableOrViewName: "Post");

            try
            {
                searchClient.DataSources.CreateOrUpdate(dataSource);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateIndex()
        {
            string indexName = "index";

            var indexExists = searchClient.Indexes.Exists(indexName);

            if (indexExists)
                searchClient.Indexes.Delete(indexName);

            var index = new Index(
                name: indexName,
                fields: FieldBuilder.BuildForType<PostIndex>());

            try
            {
                searchClient.Indexes.Create(index);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CreateIndexer()
        {
            string indexerName = "posts-indexer";
            string dataSourceName = "posts-source";
            string indexName = "index";

            var indexerExists = searchClient.Indexers.Exists(indexerName);

            if (indexerExists)
                searchClient.Indexers.Delete(indexerName);

            var indexer = new Indexer(
                name: indexerName,
                dataSourceName: dataSourceName,
                targetIndexName: indexName,
                schedule: new IndexingSchedule(TimeSpan.FromMinutes(5)));

            try
            {
                searchClient.Indexers.Create(indexer);
                searchClient.Indexers.Run(indexerName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        class PostIndex
        {
            [Key]
            [IsRetrievable(true)]
            public string Id { get; set; }

            [IsRetrievable(true)]
            public string UserId { get; set; }

            [IsFilterable, IsSearchable, IsRetrievable(true)]
            public string UserFullName { get; set; }

            [IsFilterable, IsSearchable, IsRetrievable(true)]
            public string PreviewImageUrl { get; set; }

            [IsFilterable, IsSearchable, IsRetrievable(true)]
            public string Message { get; set; }

            [IsFilterable, IsRetrievable(true)]
            public int LikeCounter { get; set; }

            [IsRetrievable(true)]
            public DateTimeOffset Date { get; set; }
        }
        #endregion
    }
}

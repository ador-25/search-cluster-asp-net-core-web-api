using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var cloudId = "e61df9493b6d478ea6bb207b9ef9b91a:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvOjQ0MyQ0ZjViMGM4N2MyMTE0NmE2OGEwNjlkZDIxYmZkYmY1MiRhOWM5MTcxNDU3MDE0YjRkODQzMjFkNGJkMjM5YWY1Zg==";
            var username = "elastic";
            var password = "0MHKMlWLJrHmYpptvYR7yk9e";
            var indexName = "my_index";

            // Extract hostname and port from the Cloud ID
            var cloudIdParts = cloudId.Split(':');
            var hostnameAndPort = cloudIdParts[cloudIdParts.Length - 2].Split('@');
            var hostname = hostnameAndPort[hostnameAndPort.Length - 1];
            var port = cloudIdParts[cloudIdParts.Length - 1];

            // Construct the URI using UriBuilder
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostname,
                Port = int.Parse(port)
            };
            var uri = uriBuilder.Uri;

            var settings = new ConnectionSettings(uri)
                .BasicAuthentication(username, password);

            var client = new ElasticClient(settings);




            var searchResponse = SearchInIndex(client, indexName, query);

            var searchResults = new List<object>();

            foreach (var hit in searchResponse.Hits)
            {
                var source = hit.Source as Dictionary<string, object>;
                if (source != null && source.ContainsKey("content"))
                {
                    var content = source["content"].ToString();

                    var res = new
                    {
                        source = source["url"].ToString(),
                        content = ProcessResult(query, content)
                    };

                    searchResults.Add(res);
                }
            }

            return Ok(searchResults);
        }

        static ISearchResponse<object> SearchInIndex(IElasticClient client, string indexName, string searchTerm)
        {
            var searchRequest = new SearchRequest<object>(indexName)
            {
                Query = new MatchQuery
                {
                    Field = "content", // Replace with the actual field name you want to search in
                    Query = searchTerm
                }
            };

            var searchResponse = client.Search<object>(searchRequest);
            return searchResponse;
        }

        static string ProcessResult(string str, string word)
        {
            word = word.Replace("\n", "");
            if (word.Contains(str))
            {
                var index = word.IndexOf(str);
                return word.Substring(index, Math.Min(200, word.Length - index));
            }
            else
            {
                return str + " found here";
            }
        }
    }
}

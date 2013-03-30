using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;


namespace Isha.WindowsApp.Server
{
    public static class JSONFeeds
    {   
        /// <summary>
        /// Generic method to get JSON data for any known objects, the object definition should match with result.
        /// e.g. if you pass "List<string>" then the result from JSON should be a simple array of strings.
        /// </summary>
        /// <typeparam name="T">Type of the object you expect the result to be</typeparam>
        /// <param name="uri">Pass the JSON URI to get data from</param>
        /// <returns></returns>
        public static async Task<T> GetJSONDataAsync<T>(string uri)
        {
            T result = default(T);
            if (String.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(uri);
            }

            string data = string.Empty;

            var request = HttpWebRequest.Create(new Uri(uri)) as HttpWebRequest;
            request.Accept = "application/json;odata=verbose";
            // Use the Task Parallel Library pattern
            var factory = new TaskFactory();
            var task = factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            try
            {
                var response = await task;
             
                // Read the response into a Stream object.
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
                
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception e)
            {
                var we = e.InnerException as WebException;
                if (we != null)
                {
                    var resp = we.Response as HttpWebResponse;
                    var code = resp.StatusCode;
                    //MessageBox.Show("RespCallback Exception raised! Message:" + we.Message +
                    //                  "HTTP Status: " + we.Status);
                }
                else
                    throw e;
            }
            return result;
        }

        /// <summary>
        /// Specific to get program list, the result has a certain foramt, it requires a custom formatting.
        /// </summary>
        /// <param name="uri">Pass the JSON URI to get data from</param>
        /// <returns></returns>
        public static async Task<ProgramList> GetProgramListAsync(string uri)
        {
            ProgramList result = default(ProgramList);
            if (String.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(uri);
            }

            string data = string.Empty;

            var request = HttpWebRequest.Create(new Uri(uri)) as HttpWebRequest;
            request.Accept = "application/json;odata=verbose";
            // Use the Task Parallel Library pattern
            var factory = new TaskFactory();
            var task = factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

            try
            {
                var response = await task;

                // Read the response into a Stream object.
                using (System.IO.Stream responseStream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(responseStream))
                    {
                        data = reader.ReadToEnd();
                    }
                }
                
                JObject obj = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(data);

                JArray array = obj["results"].ToObject<JArray>();

                result = new ProgramList();
                result.resultcount = obj["resultcount"].ToObject<int>();

                int upperbound = result.resultcount;
                int lowerbound = result.resultcount - (array.Count - 1);

                for (int i = 0; i < array.Count; i++)
                {
                    JObject jobj = array[i].Value<JObject>();

                    ProgramOverview pov = jobj[lowerbound.ToString()].ToObject(typeof(ProgramOverview)) as ProgramOverview;

                    Program p = new Program();
                    p.overallIndex = lowerbound;
                    p.overview = pov;
                    result.results.Add(p);

                    lowerbound++;
                }
            }
            catch (Exception e)
            {
                var we = e.InnerException as WebException;
                if (we != null)
                {
                    var resp = we.Response as HttpWebResponse;
                    var code = resp.StatusCode;
                    //MessageBox.Show("RespCallback Exception raised! Message:" + we.Message +
                    //                  "HTTP Status: " + we.Status);
                }
                else
                    throw e;
            }
            return result;
        }

        /// <summary>
        /// Retrieves the RSS document containing Isha blog posts and returns 
        /// the RSS items.
        /// </summary>
        /// <returns>RSS item, one for each post</returns>
        public static async Task<RSSEntry[]> GetIshaBlogPosts()
        {
            string feedUrl = @"http://feeds.ishafoundation.org/IshaBlog";

            HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(new Uri(feedUrl));

            var task = Task.Factory.FromAsync<WebResponse>(webRequest.BeginGetResponse, webRequest.EndGetResponse,
                null);
            await task;

            WebResponse response = task.Result;
            XDocument rssXDocument = XDocument.Load(response.GetResponseStream());

            // To do: Pala 3/29/2013
            // note, in the parsing below, the string pubDate contains timezone
            // as UTC, and yet the parsing creates it as local time. For now, 
            // explicitly forcing it to be UTC. Not sure if this is a hack
            // Need to follow up
            var rssItems = from item in rssXDocument.Descendants("item")
                           let title = item.Element("title").Value
                           let link = item.Element("link").Value
                           let pubDate = DateTime.SpecifyKind(DateTime.Parse(item.Element("pubDate").Value), DateTimeKind.Utc)
                           let description = item.Element("description").Value
                           select new RSSEntry(title, description, link, pubDate);

            return rssItems.ToArray();
        }
    }
}



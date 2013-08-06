using System;
using System.Net;
using System.Windows;

using System.Windows.Input;

using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Runtime.Serialization.Json;
//using Windows.Foundation.Collections;
using System.Collections;
using System.Threading.Tasks;


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
                        data = await reader.ReadToEndAsync();
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
                        data = await reader.ReadToEndAsync();
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
    }
}



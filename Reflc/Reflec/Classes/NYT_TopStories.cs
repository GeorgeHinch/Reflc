using Reflec.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    public class NYT_TopStories
    {
        private static string _apiKey = "a5fdde8f2d6d6ef2ff58ae1d2eeffa3c:4:74641225";
        public static void getTopStories()
        {
            Frame buildFrame = new Frame();

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.nytimes.com/svc/topstories/v1/home.json?api-key={_apiKey}");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    try
                    {
                        var serializer = new DataContractJsonSerializer(typeof(NYT_TopStoriesData));
                        var topStories = (NYT_TopStoriesData)serializer.ReadObject(stream);

                        buildFrame.Navigate(typeof(NYT_TopStories_Card), topStories);
                        MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
                    }
                    catch
                    {
                        MainPage.buildError(true);
                    }
                }
            }
            else
            {
                MainPage.buildError(false);
            }
        } 
    }

    #region Serialization for NYT top stories
    [DataContract]
    public class NYT_TopStoriesData
    {
        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string copyright { get; set; }

        [DataMember]
        public string section { get; set; }

        [DataMember]
        public string last_updated { get; set; }

        [DataMember]
        public int num_results { get; set; }

        [DataMember]
        public IList<NYT_Result> results { get; set; }
    }

    [DataContract]
    public class NYT_Result
    {
        [DataMember]
        public string section { get; set; }

        [DataMember]
        public string subsection { get; set; }

        [DataMember]
        public string title { get; set; }

        [DataMember(Name = "abstract")]
        public string story_abstract { get; set; }

        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string byline { get; set; }

        [DataMember]
        public string item_type { get; set; }

        [DataMember]
        public string updated_date { get; set; }

        [DataMember]
        public string created_date { get; set; }

        [DataMember]
        public string published_date { get; set; }

        [DataMember]
        public string material_type_facet { get; set; }

        [DataMember]
        public string kicker { get; set; }

        [DataMember]
        public object des_facet { get; set; }

        [DataMember]
        public object org_facet { get; set; }

        [DataMember]
        public object per_facet { get; set; }

        [DataMember]
        public object geo_facet { get; set; }

        [DataMember]
        public IList<Multimedia> multimedia { get; set; }
    }

    [DataContract]
    public class Multimedia
    {
        [DataMember]
        public string url { get; set; }

        [DataMember]
        public string format { get; set; }

        [DataMember]
        public int height { get; set; }

        [DataMember]
        public int width { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string subtype { get; set; }

        [DataMember]
        public string caption { get; set; }

        [DataMember]
        public string copyright { get; set; }
    }
    #endregion
}

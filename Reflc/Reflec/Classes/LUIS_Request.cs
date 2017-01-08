using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reflec.Classes
{
    public class LUIS_Request
    {
        public static async Task getEntityFromLUIS(string Query)
        {
            Query = Uri.EscapeDataString(Query);
            LUIS_JSON Data = new LUIS_JSON();
            HttpClient client = new HttpClient();
            using (client)
            {
                string RequestURI = "https://api.projectoxford.ai/luis/v2.0/apps/8da6fd83-89d5-4f54-a900-046366f08f85?subscription-key=f0eb997559da47ecabf134c0f6961f2e&q=" + Query;
                HttpResponseMessage msg = await client.GetAsync(RequestURI);

                if (msg.IsSuccessStatusCode)
                {
                    var JsonDataResponse = await msg.Content.ReadAsStringAsync();
                    Data = JsonConvert.DeserializeObject<LUIS_JSON>(JsonDataResponse);

                    switch (Data.topScoringIntent.intent)
                    {
                        case "Clear":
                            await Reflec_Tasks.clear();
                            break;
                        case "GetForecast":
                            await Reflec_Tasks.getForecast(Data.topScoringIntent);
                            break;
                        case "GetWeather":
                            await Reflec_Tasks.getWeather(Data.topScoringIntent);
                            break;
                        case "GetBusData":
                            await Reflec_Tasks.getBusData(Data.topScoringIntent);
                            break;
                        case "GetStopData":
                            await Reflec_Tasks.getStopData(Data.topScoringIntent);
                            break;
                        case "GetTopStories":
                            await Reflec_Tasks.getTopStories(Data.topScoringIntent);
                            break;
                        case "GetStockData":
                            await Reflec_Tasks.getStockData(Data.topScoringIntent);
                            break;
                        case "GetHoroscope":
                            await Reflec_Tasks.getTopStories(Data.topScoringIntent);
                            break;
                    }
                }
            }
        }
    }

    #region Class for serializing LUIS requests
    public class Resolution
    {
        public string date { get; set; }
    }

    public class Value
    {
        public string entity { get; set; }
        public string type { get; set; }
        public Resolution resolution { get; set; }
    }

    public class Parameter
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public IList<Value> value { get; set; }
    }

    public class Action
    {
        public bool triggered { get; set; }
        public string name { get; set; }
        public IList<Parameter> parameters { get; set; }
    }

    public class TopScoringIntent
    {
        public string intent { get; set; }
        public double score { get; set; }
        public IList<Action> actions { get; set; }
    }

    public class Intent
    {
        public string intent { get; set; }
        public double score { get; set; }
        public IList<Action> actions { get; set; }
    }

    public class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public Resolution resolution { get; set; }
        public double? score { get; set; }
    }

    public class Dialog
    {
        public string contextId { get; set; }
        public string status { get; set; }
    }

    public class LUIS_JSON
    {
        public string query { get; set; }
        public TopScoringIntent topScoringIntent { get; set; }
        public IList<Intent> intents { get; set; }
        public IList<Entity> entities { get; set; }
        public Dialog dialog { get; set; }
    }
    #endregion
}

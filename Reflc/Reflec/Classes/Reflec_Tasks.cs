using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflec.Classes
{
    public class Reflec_Tasks
    {
        public static async Task clear()
        {
            MainPage.mainPage.Main_StackPanel.Children.Clear();
        }

        public static async Task getForecast(TopScoringIntent intent)
        {
            string location = "";
            string date = "";

            /*foreach(Action a in intent.actions)
            {
                foreach(Parameter p in a.parameters)
                {
                    if(p.name == "Location")
                    {
                        location = DataBuilder.geoCityBuilder(p.value[0].entity);
                    }

                    if(p.name == "Date")
                    {
                        date = p.value[0].entity;
                    }
                }
            }*/

            if (location != "" && date != "")
            {
                OWM_GetWeather.getForecast(null);
            }

            else if (location != "")
            {
                OWM_GetWeather.getForecast(location);
            }

            else if (date != "")
            {
                OWM_GetWeather.getForecast(null);
            }

            else
            {
                OWM_GetWeather.getForecast(null);
            }
        }

        public static async Task getWeather(TopScoringIntent intent)
        {
            string location = "";
            string date = "";

            /*foreach (Action a in intent.actions)
            {
                foreach (Parameter p in a.parameters)
                {
                    if (p.name == "Location")
                    {
                        location = DataBuilder.geoCityBuilder(p.value[0].entity);
                    }
                }
            }*/

            if (location != "" && date != "")
            {
                OWM_GetWeather.getCurrent(null);
            }

            else if (location != "")
            {
                OWM_GetWeather.getCurrent(location);
            }

            else if (date != "")
            {
                OWM_GetWeather.getCurrent(null);
            }

            else
            {
                OWM_GetWeather.getCurrent(null);
            }
        }

        public static async Task getBusData(TopScoringIntent intent)
        {
            OBA_Request.getStopAD();
        }

        public static async Task getStopData(TopScoringIntent intent)
        {
            OBA_Request.getNearbyStop();
        }

        public static async Task getTopStories(TopScoringIntent intent)
        {
            NYT_TopStories.getTopStories();
        }

        public static async Task getStockData(TopScoringIntent intent)
        {
            YDN_GetStocks.getStockData(intent.actions[0].parameters[0].value[0].entity);
        }

        public static async Task getSportsData(TopScoringIntent intent)
        {
            YDN_GetStocks.getStockData(intent.actions[0].parameters[0].value[0].entity);
        }

        public static async Task getHoroscope(TopScoringIntent intent)
        {
            YDN_GetStocks.getStockData(intent.actions[0].parameters[0].value[0].entity);
        }

        public static async Task getFlight(TopScoringIntent intent)
        {
            PF_FlightData.getFlightData(intent.actions[0].parameters[0].value[0].entity);
        }
    }
}

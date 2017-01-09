using Reflec.Cards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Reflec.Classes
{
    class ESPN_SportsData
    {
        #region Gets current sports data
        private static HashSet<string> Teams_MLB = new HashSet<string> { "braves", "marlins", "mets", "phillies", "nationals", "cubs", "reds", "brewers", "pirates", "saint louis cardinals", "diamondbacks", "rockies", "dodgers", "padres", "giants", "orioles", "red sox", "yankees", "rays", "blue jays", "white sox", "indians", "tigers", "royals", "twins", "astros", "angels", "athletics", "mariners", "texas rangers" };
        private static HashSet<string> Teams_MLS = new HashSet<string> { "fire", "rapids", "columbus crew sc", "dc united", "fc dallas", "dynamo", "impact", "galaxy", "revolution", "new york city fc", "red bulls", "orlando city sc", "philadelphia union", "timbers", "real salt lake", "earthquakes", "sounders", "sporting kansas city", "toronto fc", "whitecaps"};
        private static HashSet<string> Teams_NBA = new HashSet<string> { "celtics", "nets", "knicks", "76ers", "raptors", "bulls", "cavaliers", "pistons", "pacers", "bucks", "hawks", "bobcats", "heat", "magic", "wizards", "nuggets", "timberwolves", "thunder", "trail blazers", "jazz", "warriors", "clippers", "lakers", "suns", "kings", "mavericks", "rockets", "grizzlies", "hornets", "spurs"};
        private static HashSet<string> Teams_NFL = new HashSet<string> { "bills", "dolphins", "patriots", "jets", "ravens", "bengals", "browns", "steelers", "texans", "colts", "jaguars", "titans", "broncos", "chiefs", "raiders", "chargers", "cowboys", "giants", "new york giants", "ny giants", "eagles", "redskins", "bears", "lions", "packers", "vikings", "falcons", "panthers", "saints", "buccaneers", "cardinals", "rams", "49ers", "seahawks" };
        private static HashSet<string> Teams_NHL = new HashSet<string> { "devils", "islanders", "rangers", "flyers", "penguins", "bruins", "sabres", "canadiens", "senators", "maple leafs", "hurricanes", "panthers", "lightning", "capitals", "winnipeg jets", "blackhawks", "blue jackets", "red wings", "predators", "blues", "flames", "avalanche", "oilers", "wild", "canucks", "ducks", "stars", "la kings", "coyotes", "sharks" };

        public static void getSportData(string team)
        {
            Frame buildFrame = new Frame();

            string sport;
            string league;

            if (Teams_MLB.Contains(team))
            {
                sport = "baseball";
                league = "mlb";
            }
            else if (Teams_NBA.Contains(team))
            {
                sport = "basketball";
                league = "nba";
            }
            else if (Teams_NFL.Contains(team))
            {
                sport = "football";
                league = "nfl";
            }
            else if (Teams_MLS.Contains(team))
            {
                sport = "soccer";
                league = "mls";
            }
            else if (Teams_NHL.Contains(team))
            {
                sport = "hockey";
                league = "nhl";
            }
            else
            {
                sport = "";
                league = "";
            }

            // http://site.api.espn.com/apis/site/v2/sports/football/nfl/scoreboard?lang=en&region=us&calendartype=blacklist&limit=100&dates=2016&seasontype=2

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://site.api.espn.com/apis/v2/scoreboard/header?sport={sport}&league={league}");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SportsData));
                        var sportDetails = (SportsData)serializer.ReadObject(stream);

                        Event sportsEvent = null;
                        int eventInt = 0;

                        foreach (Event e in sportDetails.sports[0].leagues[0].events)
                        {
                            if (e.competitors[0].name.ToLower() == team)
                            {
                                sportsEvent = sportDetails.sports[0].leagues[0].events[eventInt];
                            }

                            else if (e.competitors[1].name.ToLower() == team)
                            {
                                sportsEvent = sportDetails.sports[0].leagues[0].events[eventInt];
                            }

                            else
                            {
                                eventInt++;
                            }
                        }

                        buildFrame.Navigate(typeof(Sport_Card), sportsEvent);
                        MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
                    }

                }
                catch
                {
                    TextBlock tb = new TextBlock();
                    tb.Width = 500;
                    tb.FontSize = 48;
                    tb.FontStyle = FontStyle.Italic;
                    tb.FontWeight = FontWeights.Thin;
                    tb.Margin = new Thickness(0, 0, 0, 25);
                    tb.TextAlignment = TextAlignment.Center;
                    tb.TextWrapping = TextWrapping.WrapWholeWords;
                    tb.Foreground = new SolidColorBrush(Colors.White);
                    tb.Text = "Reflec could not process this request.";
                    
                    MainPage.mainPage.Main_StackPanel.Children.Add(tb);
                }
            }
            else
            {
                buildFrame.Navigate(typeof(Sport_Card), null);
                MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
            }
        }
        #endregion
    }

    #region Serialization for ESPN data
    public class Link
    {
        public IList<string> rel { get; set; }
        public string href { get; set; }
        public string text { get; set; }
    }

    public class Type
    {
        public int id { get; set; }
        public string description { get; set; }
        public string detail { get; set; }
        public string shortDetail { get; set; }
        public string state { get; set; }
    }

    public class FullStatus
    {
        public string clock { get; set; }
        public int period { get; set; }
        public Type type { get; set; }
    }

    public class Broadcast
    {
        public string type { get; set; }
        public int typeId { get; set; }
        public bool isNational { get; set; }
        public int broadcasterId { get; set; }
        public int broadcastId { get; set; }
        public int priority { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string callLetters { get; set; }
        public string region { get; set; }
        public string lang { get; set; }
    }

    public class Competitor
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string type { get; set; }
        public string color { get; set; }
        public string group { get; set; }
        public string homeAway { get; set; }
        public string abbreviation { get; set; }
        public string displayName { get; set; }
        public string score { get; set; }
        public bool winner { get; set; }
        public string record { get; set; }
        public string competitionIdPrevious { get; set; }
        public string name { get; set; }
        public string logo { get; set; }
        public string logoDark { get; set; }
    }

    public class AppLink
    {
        public IList<string> rel { get; set; }
        public string href { get; set; }
    }

    public class Event
    {
        public string id { get; set; }
        public string competitionId { get; set; }
        public string uid { get; set; }
        public string date { get; set; }
        public bool timeValid { get; set; }
        public string location { get; set; }
        public int season { get; set; }
        public int seasonType { get; set; }
        public int week { get; set; }
        public string weekText { get; set; }
        public int period { get; set; }
        public string clock { get; set; }
        public IList<Link> links { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
        public FullStatus fullStatus { get; set; }
        public string link { get; set; }
        public IList<Broadcast> broadcasts { get; set; }
        public string broadcast { get; set; }
        public bool onWatch { get; set; }
        public IList<Competitor> competitors { get; set; }
        public IList<AppLink> appLinks { get; set; }
    }

    public class Smartdate
    {
        public string label { get; set; }
        public int seasontype { get; set; }
        public int week { get; set; }
        public int season { get; set; }
    }

    public class League
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string abbreviation { get; set; }
        public string slug { get; set; }
        public IList<Event> events { get; set; }
        public IList<Smartdate> smartdates { get; set; }
    }

    public class Sport
    {
        public string id { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public IList<League> leagues { get; set; }
    }

    public class SportsData
    {
        public IList<Sport> sports { get; set; }
    }
    #endregion
}

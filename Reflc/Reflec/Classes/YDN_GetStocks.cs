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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Reflec.Classes
{
    class YDN_GetStocks
    {
        #region Gets current stock data
        public static void getStockData(string symbol)
        {
            Frame buildFrame = new Frame();

            // %22YHOO%22,%22AAPL%22
            // %22AAPL%22

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20IN%20(%22{symbol}%22)&format=json&env=http://datatables.org/alltables.env");
            HttpClient client = new HttpClient();
            var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                var bytes = Encoding.Unicode.GetBytes(result);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var serializer = new DataContractJsonSerializer(typeof(StockData));
                    var stockDetails = (StockData)serializer.ReadObject(stream);

                    buildFrame.Navigate(typeof(Stock_Card), stockDetails);
                    MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
                }
            }
            else
            {
                buildFrame.Navigate(typeof(Stock_Card), null);
                MainPage.mainPage.Main_StackPanel.Children.Add(buildFrame);
            }
        }
        #endregion
    }

    #region Class for stock data serialization
    [DataContract]
    public class Quote
    {
        [DataMember]
        public string symbol { get; set; }

        [DataMember]
        public string Ask { get; set; }

        [DataMember]
        public string AverageDailyVolume { get; set; }

        [DataMember]
        public string Bid { get; set; }

        [DataMember]
        public object AskRealtime { get; set; }

        [DataMember]
        public object BidRealtime { get; set; }

        [DataMember]
        public string BookValue { get; set; }

        [DataMember]
        public string Change_PercentChange { get; set; }

        [DataMember]
        public string Change { get; set; }

        [DataMember]
        public object Commission { get; set; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public object ChangeRealtime { get; set; }

        [DataMember]
        public object AfterHoursChangeRealtime { get; set; }

        [DataMember]
        public string DividendShare { get; set; }

        [DataMember]
        public string LastTradeDate { get; set; }

        [DataMember]
        public object TradeDate { get; set; }

        [DataMember]
        public string EarningsShare { get; set; }

        [DataMember]
        public object ErrorIndicationreturnedforsymbolchangedinvalid { get; set; }

        [DataMember]
        public string EPSEstimateCurrentYear { get; set; }

        [DataMember]
        public string EPSEstimateNextYear { get; set; }

        [DataMember]
        public string EPSEstimateNextQuarter { get; set; }

        [DataMember]
        public string DaysLow { get; set; }

        [DataMember]
        public string DaysHigh { get; set; }

        [DataMember]
        public string YearLow { get; set; }

        [DataMember]
        public string YearHigh { get; set; }

        [DataMember]
        public object HoldingsGainPercent { get; set; }

        [DataMember]
        public object AnnualizedGain { get; set; }

        [DataMember]
        public object HoldingsGain { get; set; }

        [DataMember]
        public object HoldingsGainPercentRealtime { get; set; }

        [DataMember]
        public object HoldingsGainRealtime { get; set; }

        [DataMember]
        public object MoreInfo { get; set; }

        [DataMember]
        public object OrderBookRealtime { get; set; }

        [DataMember]
        public string MarketCapitalization { get; set; }

        [DataMember]
        public object MarketCapRealtime { get; set; }

        [DataMember]
        public string EBITDA { get; set; }

        [DataMember]
        public string ChangeFromYearLow { get; set; }

        [DataMember]
        public string PercentChangeFromYearLow { get; set; }

        [DataMember]
        public object LastTradeRealtimeWithTime { get; set; }

        [DataMember]
        public object ChangePercentRealtime { get; set; }

        [DataMember]
        public string ChangeFromYearHigh { get; set; }

        [DataMember]
        public string PercebtChangeFromYearHigh { get; set; }

        [DataMember]
        public string LastTradeWithTime { get; set; }

        [DataMember]
        public string LastTradePriceOnly { get; set; }

        [DataMember]
        public object HighLimit { get; set; }

        [DataMember]
        public object LowLimit { get; set; }

        [DataMember]
        public string DaysRange { get; set; }

        [DataMember]
        public object DaysRangeRealtime { get; set; }

        [DataMember]
        public string FiftydayMovingAverage { get; set; }

        [DataMember]
        public string TwoHundreddayMovingAverage { get; set; }

        [DataMember]
        public string ChangeFromTwoHundreddayMovingAverage { get; set; }

        [DataMember]
        public string PercentChangeFromTwoHundreddayMovingAverage { get; set; }

        [DataMember]
        public string ChangeFromFiftydayMovingAverage { get; set; }

        [DataMember]
        public string PercentChangeFromFiftydayMovingAverage { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public object Notes { get; set; }

        [DataMember]
        public string Open { get; set; }

        [DataMember]
        public string PreviousClose { get; set; }

        [DataMember]
        public object PricePaid { get; set; }

        [DataMember]
        public string ChangeinPercent { get; set; }

        [DataMember]
        public string PriceSales { get; set; }

        [DataMember]
        public string PriceBook { get; set; }

        [DataMember]
        public string ExDividendDate { get; set; }

        [DataMember]
        public string PERatio { get; set; }

        [DataMember]
        public string DividendPayDate { get; set; }

        [DataMember]
        public object PERatioRealtime { get; set; }

        [DataMember]
        public string PEGRatio { get; set; }

        [DataMember]
        public string PriceEPSEstimateCurrentYear { get; set; }

        [DataMember]
        public string PriceEPSEstimateNextYear { get; set; }

        [DataMember]
        public string Symbol { get; set; }

        [DataMember]
        public object SharesOwned { get; set; }

        [DataMember]
        public string ShortRatio { get; set; }

        [DataMember]
        public string LastTradeTime { get; set; }

        [DataMember]
        public object TickerTrend { get; set; }

        [DataMember]
        public string OneyrTargetPrice { get; set; }

        [DataMember]
        public string Volume { get; set; }

        [DataMember]
        public object HoldingsValue { get; set; }

        [DataMember]
        public object HoldingsValueRealtime { get; set; }

        [DataMember]
        public string YearRange { get; set; }

        [DataMember]
        public object DaysValueChange { get; set; }

        [DataMember]
        public object DaysValueChangeRealtime { get; set; }

        [DataMember]
        public string StockExchange { get; set; }

        [DataMember]
        public string DividendYield { get; set; }

        [DataMember]
        public string PercentChange { get; set; }
    }

    [DataContract]
    public class Results
    {
        [DataMember]
        public Quote quote { get; set; }
    }

    [DataContract]
    public class Query
    {
        [DataMember]
        public int count { get; set; }

        [DataMember]
        public string created { get; set; }

        [DataMember]
        public string lang { get; set; }

        [DataMember]
        public Results results { get; set; }
    }

    [DataContract]
    public class StockData
    {
        [DataMember]
        public Query query { get; set; }
    }
    #endregion
}

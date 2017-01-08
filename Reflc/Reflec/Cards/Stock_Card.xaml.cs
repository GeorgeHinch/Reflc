using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Reflec.Cards
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Stock_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StockData symbol = e.Parameter as StockData;

            double changeValue = double.Parse(symbol.query.results.quote.Change);

            currentValue_Textblock.Text = symbol.query.results.quote.Bid;
            currentSymbol_Textblock.Text = symbol.query.results.quote.Name;

            if(changeValue < 0)
            {
                changeColor_Grid.Background = new SolidColorBrush(Color.FromArgb(255, 255, 108, 92));
            }
            else if (changeValue > 0)
            {
                changeColor_Grid.Background = new SolidColorBrush(Color.FromArgb(255, 62, 220, 129));
            }
            else
            {
                changeColor_Grid.Background = new SolidColorBrush(Color.FromArgb(255,144,144,144));
            }

            dayLowValue_Textblock.Text = symbol.query.results.quote.DaysLow;
            changeValue_Textblock.Text = changeValue.ToString();
            dayHighValue_Textblock.Text = symbol.query.results.quote.DaysHigh;

            stockAdditionalInfo_TextBlock.Text = symbol.query.results.quote.symbol;
        }

        public Stock_Card()
        {
            this.InitializeComponent();
        }
    }
}

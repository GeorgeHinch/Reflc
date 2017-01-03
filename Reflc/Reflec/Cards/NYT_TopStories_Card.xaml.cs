using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class NYT_TopStories_Card : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NYT_TopStoriesData stories = e.Parameter as NYT_TopStoriesData;

            #region Set NYT section
            NYTAdditionalInfo_TextBlock.Text = "NYT Home";
            //NYTAdditionalInfo_TextBlock.Text = DataBuilder.buildNYTSection(stories.section);
            #endregion

            #region Set headlines
            NYT_TopStory1_TextBlock.Text = stories.results[0].title;
            NYT_TopStory2_TextBlock.Text = stories.results[1].title;
            NYT_TopStory3_TextBlock.Text = stories.results[2].title;
            NYT_TopStory4_TextBlock.Text = stories.results[3].title;
            NYT_TopStory5_TextBlock.Text = stories.results[4].title;
            NYT_TopStory6_TextBlock.Text = stories.results[5].title;
            NYT_TopStory7_TextBlock.Text = stories.results[6].title;
            #endregion
        }

        public NYT_TopStories_Card()
        {
            this.InitializeComponent();
        }
    }
}

using Reflec.Assets.Weather;
using Reflec.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization;
using Windows.Media.Capture;
using Windows.Media.SpeechRecognition;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Reflec
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage mainPage;

        private SpeechRecognizer speechRecognizer;
        public CoreDispatcher dispatcher;
        private StringBuilder dictatedTextBuilder;
        private bool isListening;
        private bool isReflec;

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            bool permissionGained = await checkMicrophonePermissions();

            if (permissionGained)
            {
                //btnContinuousRecognize.IsEnabled = true;

                //PopulateLanguageDropdown();
                await InitializeRecognizer(SpeechRecognizer.SystemSpeechLanguage);
            }
            else
            {
                Debug.WriteLine("No microphone access");
                //this.dictationTextBox.Text = "Permission to access capture resources was not given by the user, reset the application setting in Settings->Privacy->Microphone.";
                //btnContinuousRecognize.IsEnabled = false;
                //cbLanguageSelection.IsEnabled = false;
            }

            if (isListening == false)
            {
                // The recognizer can only start listening in a continuous fashion if the recognizer is currently idle.
                // This prevents an exception from occurring.
                if (speechRecognizer.State == SpeechRecognizerState.Idle)
                {
                    try
                    {
                        isListening = true;
                        await speechRecognizer.ContinuousRecognitionSession.StartAsync();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("No Privacy.");
                        await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-speechtyping"));

                        isListening = false;

                    }
                }
            }
            else
            {
                isListening = false;

                if (speechRecognizer.State != SpeechRecognizerState.Idle)
                {
                    // Cancelling recognition prevents any currently recognized speech from
                    // generating a ResultGenerated event. StopAsync() will allow the final session to 
                    // complete.
                    try
                    {
                        await speechRecognizer.ContinuousRecognitionSession.StopAsync();

                        // Ensure we don't leave any hypothesis text behind
                        Debug.WriteLine("Ensure we don't leave any hypothesis text behind: " + dictatedTextBuilder.ToString());
                    }
                    catch (Exception exception)
                    {
                        var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                        await messageDialog.ShowAsync();
                    }
                }
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            mainPage = this;

            isListening = false;
            dictatedTextBuilder = new StringBuilder();

            DataBuilder.setWeatherAnimation();
        }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (this.speechRecognizer != null)
            {
                if (isListening)
                {
                    await this.speechRecognizer.ContinuousRecognitionSession.CancelAsync();
                    isListening = false;
                }

                speechRecognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
                speechRecognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;
                speechRecognizer.HypothesisGenerated -= SpeechRecognizer_HypothesisGenerated;
                speechRecognizer.StateChanged -= SpeechRecognizer_StateChanged;

                this.speechRecognizer.Dispose();
                this.speechRecognizer = null;
            }
        }

        private async Task InitializeRecognizer(Language recognizerLanguage)
        {
            if (speechRecognizer != null)
            {
                // cleanup prior to re-initializing this scenario.
                speechRecognizer.StateChanged -= SpeechRecognizer_StateChanged;
                speechRecognizer.ContinuousRecognitionSession.Completed -= ContinuousRecognitionSession_Completed;
                speechRecognizer.ContinuousRecognitionSession.ResultGenerated -= ContinuousRecognitionSession_ResultGenerated;
                speechRecognizer.HypothesisGenerated -= SpeechRecognizer_HypothesisGenerated;

                this.speechRecognizer.Dispose();
                this.speechRecognizer = null;
            }

            this.speechRecognizer = new SpeechRecognizer(recognizerLanguage);

            // Provide feedback to the user about the state of the recognizer. This can be used to provide visual feedback in the form
            // of an audio indicator to help the user understand whether they're being heard.
            speechRecognizer.StateChanged += SpeechRecognizer_StateChanged;

            // Apply the dictation topic constraint to optimize for dictated freeform speech.
            var dictationConstraint = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "dictation");
            speechRecognizer.Constraints.Add(dictationConstraint);
            SpeechRecognitionCompilationResult result = await speechRecognizer.CompileConstraintsAsync();
            if (result.Status != SpeechRecognitionResultStatus.Success)
            {
                Debug.WriteLine("Result Status: " + result.Status.ToString());
                //rootPage.NotifyUser("Grammar Compilation Failed: " + result.Status.ToString(), NotifyType.ErrorMessage);
            }

            // Handle continuous recognition events. Completed fires when various error states occur. ResultGenerated fires when
            // some recognized phrases occur, or the garbage rule is hit. HypothesisGenerated fires during recognition, and
            // allows us to provide incremental feedback based on what the user's currently saying.
            speechRecognizer.ContinuousRecognitionSession.Completed += ContinuousRecognitionSession_Completed;
            speechRecognizer.ContinuousRecognitionSession.ResultGenerated += ContinuousRecognitionSession_ResultGenerated;
            speechRecognizer.HypothesisGenerated += SpeechRecognizer_HypothesisGenerated;
        }

        private async void ContinuousRecognitionSession_Completed(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionCompletedEventArgs args)
        {
            if (args.Status != SpeechRecognitionResultStatus.Success)
            {
                // If TimeoutExceeded occurs, the user has been silent for too long. We can use this to 
                // cancel recognition if the user in dictation mode and walks away from their device, etc.
                // In a global-command type scenario, this timeout won't apply automatically.
                // With dictation (no grammar in place) modes, the default timeout is 20 seconds.
                if (args.Status == SpeechRecognitionResultStatus.TimeoutExceeded)
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Debug.WriteLine("Automatic Time Out of Dictation");
                        isListening = false;
                    });
                }
                else
                {
                    await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        Debug.WriteLine("Continuous Recognition Completed: " + args.Status.ToString());
                        isListening = false;
                    });
                }
            }

            await speechRecognizer.ContinuousRecognitionSession.StartAsync();
            isListening = true;
        }

        private async void SpeechRecognizer_HypothesisGenerated(SpeechRecognizer sender, SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            string hypothesis = args.Hypothesis.Text;

            // Update the textbox with the currently confirmed text, and the hypothesis combined.
            string textboxContent = dictatedTextBuilder.ToString() + " " + hypothesis + " ...";
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (hypothesis == "reflec" || hypothesis == "reflex" || hypothesis == "reflect")
                {
                    isReflec = true;
                }
                Debug.WriteLine("Hypothesis: " + textboxContent);
            });
        }

        private async void ContinuousRecognitionSession_ResultGenerated(SpeechContinuousRecognitionSession sender, SpeechContinuousRecognitionResultGeneratedEventArgs args)
        {
            // We may choose to discard content that has low confidence, as that could indicate that we're picking up
            // noise via the microphone, or someone could be talking out of earshot.
            if (args.Result.Confidence == SpeechRecognitionConfidence.Medium || args.Result.Confidence == SpeechRecognitionConfidence.High)
            {
                dictatedTextBuilder.Append(args.Result.Text + " ");

                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    if (isReflec)
                    {
                        Debug.WriteLine("Reflec can do things!");
                        string requestQuery = dictatedTextBuilder.ToString();
                        LUIS_Request.getEntityFromLUIS(requestQuery);

                        
                        isReflec = false;
                    }
                    Debug.WriteLine("Result: " + dictatedTextBuilder.ToString());

                    dictatedTextBuilder.Clear();
                });
            }
            else
            {
                // In some scenarios, a developer may choose to ignore giving the user feedback in this case, if speech
                // is not the primary input mechanism for the application.
                // Here, just remove any hypothesis text by resetting it to the last known good.
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    Debug.WriteLine("Result Failed: " + dictatedTextBuilder.ToString());

                    string discardedText = args.Result.Text;
                    if (!string.IsNullOrEmpty(discardedText))
                    {
                        discardedText = discardedText.Length <= 25 ? discardedText : (discardedText.Substring(0, 25) + "...");

                        Debug.WriteLine("Discarded due to low/rejected Confidence: " + discardedText);
                    }
                });
            }
        }

        private async void SpeechRecognizer_StateChanged(SpeechRecognizer sender, SpeechRecognizerStateChangedEventArgs args)
        {
            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                Debug.WriteLine("State Changed: " + args.State.ToString());
            });
        }

        public static async Task<bool> checkMicrophonePermissions()
        {
            try
            {
                MediaCaptureInitializationSettings settings = new MediaCaptureInitializationSettings();
                settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
                settings.MediaCategory = MediaCategory.Speech;
                MediaCapture capture = new MediaCapture();

                await capture.InitializeAsync();
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            return true;
        }

        private void Rain_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Rain_Animation), false);
        }

        private void Snow_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Snow_Animation), false);
        }

        private void Sun_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Sun_Animation), false);
        }

        private void Sun_Cloud_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Sun_Cloud_Animation), false);
        }

        private void Moon_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Moon_Animation), false);
        }

        private void Moon_Cloud_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Moon_Cloud_Animation), false);
        }

        private void Clouds_Click(object sender, RoutedEventArgs e)
        {
            WeartherAnimation_Frame.Navigate(typeof(Cloudy_Animation), false);
        }

        private void LoadWeather_Click(object sender, RoutedEventArgs e)
        {
            OWM_GetWeather.getForecast(DataBuilder.geoCityBuilder("San Francisco, CA, USA"));
        }

        private void LoadTransit_Click(object sender, RoutedEventArgs e)
        {
            OBA_Request.getStopAD();
        }

        private void LoadTransitStop_Click(object sender, RoutedEventArgs e)
        {
            OBA_Request.getNearbyStop();
        }

        private void LoadTopStory_Click(object sender, RoutedEventArgs e)
        {
            NYT_TopStories.getTopStories();
        }

        private void LoadStocks_Click(object sender, RoutedEventArgs e)
        {
            YDN_GetStocks.getStockData("GOOG");
        }

        private void LoadSports_Click(object sender, RoutedEventArgs e)
        {
            ESPN_SportsData.getSportData("seahawks");
        }

        private void LoadFlight_Click(object sender, RoutedEventArgs e)
        {
            PF_FlightData.getFlightData("UA128");
        }
    }
}

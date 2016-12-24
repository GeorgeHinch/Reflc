using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Reflec.Classes
{
    class SlackSender
    {
        private static readonly Uri _uri = new Uri("https://hooks.slack.com/services/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");
        private static SlackPayload payload = new SlackPayload();

        #region Builds generic Slack Message
        public static void slackMessageSender(string username, string text)
        {
            payload.Channel = "#reflec";
            payload.Username = username;
            payload.Text = text;

            slackSender(payload);
        }
        #endregion

        #region Builds Slack exception message
        public static void slackExceptionSender(Exception e)
        {
            payload.Channel = "#reflec";
            payload.Username = "Mirror Exception";

            SlackAttachmentsFields[] fieldsArray = new SlackAttachmentsFields[1];
            fieldsArray[0] = new SlackAttachmentsFields
            {
                Title = "Stack Trace",
                Value = e.StackTrace,
                Short = false
            };

            SlackAttachments[] attatchArray = new SlackAttachments[1];
            attatchArray[0] = new SlackAttachments
            {
                Fallback = "New Exception: " + e.Message,
                Pretext = "New Exception: <" + e.HelpLink + "|" + e.Message + ">",
                Color = "#D00000",
                AttachmentsFields = fieldsArray
            };

            payload.Attachments = attatchArray;

            slackSender(payload);
        }
        #endregion

        #region Sends Slack payload
        private static async void slackSender(SlackPayload payload)
        {
            string json = JsonConvert.SerializeObject(payload);

            HttpClient client = new HttpClient();
            HttpResponseMessage respon = await client.PostAsync(_uri, new StringContent(json, Encoding.UTF8, "application/json"));
        }
        #endregion
    }


    #region Classes to serializes Json payload required by Slack Incoming WebHooks
    public class SlackPayload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("unfurl_links")]
        public bool UnfurlLinks { get; set; }

        [JsonProperty("unfurl_media")]
        public bool UnfurlMedia { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("attachments")]
        public SlackAttachments[] Attachments { get; set; }
    }

    public class SlackAttachments
    {
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_link")]
        public string AuthorLink { get; set; }

        [JsonProperty("author_icon")]
        public string AuthorIcon { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_link")]
        public string TitleLink { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("fallback")]
        public string Fallback { get; set; }

        [JsonProperty("pretext")]
        public string Pretext { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fields")]
        public SlackAttachmentsFields[] AttachmentsFields { get; set; }

        [JsonProperty("actions")]
        public SlackAttachmentsActions[] AttachmentsActions { get; set; }

        [JsonProperty("image_url")]
        public string ImageURL { get; set; }

        [JsonProperty("thumb_url")]
        public string ThumbURL { get; set; }

        [JsonProperty("footer")]
        public string Footer { get; set; }

        [JsonProperty("footer_icon")]
        public string FooterIcon { get; set; }

        [JsonProperty("ts")]
        public string TimeStamp { get; set; }
    }

    public class SlackAttachmentsFields
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("short")]
        public bool Short { get; set; }
    }

    public class SlackAttachmentsActions
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public bool Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("style")]
        public string Style { get; set; }

        [JsonProperty("confirm")]
        public SlackAttachmentsActionsConfirm Confirm { get; set; }
    }

    public class SlackAttachmentsActionsConfirm
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public bool Text { get; set; }

        [JsonProperty("ok_text")]
        public string OkText { get; set; }

        [JsonProperty("dismiss_text")]
        public string DismissText { get; set; }
    }
    #endregion
}

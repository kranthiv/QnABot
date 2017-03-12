using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QnAMakerDialog;
using System.Threading.Tasks;
using DpsBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using DpsBot.Helpers;
using System.Net;

namespace DpsBot.QnADialog
{
    public class QnAService
    {
        private const string knowledgebaseId = @"";
        private const string qnamakerSubscriptionKey = "";

        public async Task<QnAResponse> GetAnswerAsync(string question)
        {
            string responseString = string.Empty;


            //Build the URI
            Uri qnamakerUriBase = new Uri("https://westus.api.cognitive.microsoft.com/qnamaker/v1.0");
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgebaseId}/generateAnswer");

            //Add the question as part of the body
            var postBody = $"{{\"question\": \"{question}\"}}";

            //Send the POST request
            using (WebClient client = new WebClient())
            {
                //Add the subscription key header
                client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                responseString = await client.UploadStringTaskAsync(builder.Uri, postBody);
            }

            QnAResponse response = await JSONHelper.DeSerializeJSON<QnAResponse>(responseString);
            return response;
        }


    }
}
using DpsBot.Models;
using DpsBot.QnADialog;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DpsBot.Dialogs
{
    [Serializable]
    [LuisModel("38515128-9320-4a63-ade6-8666cf8ecda1", "e9e0d02c56bd46c4be04dad90aa45dfb")]
    public class RootLuisDialog :LuisDialog<object>
    {
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("ConversationStarter")]
        public async Task ConversationStarter(IDialogContext context, LuisResult result)
        {
            string message = "Hi,Welcome to DPS.\n How can we help?";
            await context.PostAsync(message);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("QnA")]
        public async Task QnA(IDialogContext context,LuisResult result)
        {
            QnAService qNa = new QnAService();
            QnAResponse reponse = await qNa.GetAnswerAsync(result.Query);
            await context.PostAsync(reponse.Answer);
            context.Wait(this.MessageReceived);
        }

        [LuisIntent("AccountCreation")]
        public async Task CreateAccount(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Account creation invoked");
            context.Wait(this.MessageReceived);
        }
    }
}
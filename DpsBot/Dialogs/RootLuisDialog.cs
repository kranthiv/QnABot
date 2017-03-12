using DpsBot.Forms;
using DpsBot.Models;
using DpsBot.QnADialog;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DpsBot.Dialogs
{
    [Serializable]
    [LuisModel("", "")]
    public class RootLuisDialog :LuisDialog<object>
    {
        internal static IDialog<AccountCreationForm> MakeAccountCreationDialog()
        {
            return Chain.From(() => FormDialog.FromForm(AccountCreationForm.BuildForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;
                        // Actually process the sandwich order...
                        await context.PostAsync("Processed your Account!");
                    }
                    catch (FormCanceledException<AccountCreationForm> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = $"You quit on {e.Last}--maybe you can finish next time!";
                        }
                        else
                        {
                            reply = "Sorry, I've had a short circuit.  Please try again.";
                        }
                        await context.PostAsync(reply);
                    }
                });
        }

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
        public async Task CreateAccount(IDialogContext context,LuisResult result)
        {
            //await context.Call(MakeAccountCreationDialog, ResumeAfterFormDialog);
            var query = new Account();
            var formDialog = new FormDialog<Account>(query, this.BuildForm, FormOptions.PromptInStart, result.Entities);
            context.Call<Account>(formDialog,ResumeAfterFormDialog);
            //await context.PostAsync("Account creation invoked");
            //context.Wait(this.MessageReceived);
        }

        private Task ResumeAfterFormDialog(IDialogContext context, IAwaitable<Account> result)
        {
            return context.PostAsync("done");
        }

        private IForm<Account> BuildForm()
        {
            IForm<Account> form;
            OnCompletionAsyncDelegate<Account> processLoanSuspension = async (context, state) =>
            {
                

                await context.PostAsync("account created");
            };

            var builder = new FormBuilder<Account>()
                .Field(nameof(Account.Email), (state) => string.IsNullOrEmpty(state.Email))
                .Field(nameof(Account.ConfirmEmail))
                .Field(nameof(Account.Password))
                .Field(nameof(Account.ConfirmPassword))
                .Field(nameof(Account.SecurityQuestion))
                .Field(nameof(Account.Answer))
                .Field(nameof(Account.title))
                .Field(nameof(Account.FirstName))
                .Field(nameof(Account.LastName))
                .Field(nameof(Account.Address))
                .Field(nameof(Account.MobileNumber))
                .Confirm("Do you want to proceed with account creation")
                .OnCompletion(processLoanSuspension);
            builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;
            form = builder.Build();
            return form;

        }


        private async Task ResumeAfterFormDialog(IDialogContext context, IAwaitable<AccountCreationForm> result)
        {
            try
            {
                var searchQuery = await result;

                //Database call to suspend loan payments


                await context.PostAsync($"Suspending payment to the loan initilized...");

                await context.PostAsync("payment to the loan suspended!!");
            }
            catch (FormCanceledException ex)
            {
                string reply;

                if (ex.InnerException == null)
                {
                    reply = "You have canceled the operation.";
                }
                else
                {
                    reply = $"Oops! Something went wrong :( Technical Details: {ex.InnerException.Message}";
                }

                await context.PostAsync(reply);
            }
            finally
            {
                context.Done<object>(null);
            }
        }
    }
}
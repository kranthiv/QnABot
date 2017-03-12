using DpsBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpsBot.Forms
{

    [Serializable]
    public class AccountCreationForm
    {

        [Prompt("Please enter your {&}")]
        public string Email { get; set; }

        [Prompt("Please confirm your {&}")]
        public string ConfirmEmail { get; set; }

        [Prompt("Please enter your {&}")]
        public string Password { get; set; }

        [Prompt("Please confirm your {&}")]
        public string ConfirmPassword { get; set; }

        [Prompt("Please enter your {&}")]
        public string SecurityQuestion { get; set; }

        [Prompt("Please {&} your Security question")]
        public string Answer { get; set; }

        [Prompt("Please choose {&}")]
        public Title title { get; set; }

        [Prompt("Please enter your {&}")]
        public string FirstName { get; set; }

        [Prompt("Please enter your {&}")]
        public string SecondName { get; set; }

        [Prompt("Please enter your {&}")]
        public string Address { get; set; }

        [Prompt("Please enter your {&}")]
        [Pattern(@"(\(\d{3}\))?\s*\d{3}(-|\s*)\d{4}")]
        public string MobileNumber { get; set; }

        public static IForm<AccountCreationForm> BuildForm()
        {
            OnCompletionAsyncDelegate<AccountCreationForm> processOrder = async (context, state) =>
            {
                await context.PostAsync(@"We are currently processing your sandwich. We will message you the status.");
            };

            return new FormBuilder<AccountCreationForm>()
                .Message("Thanks for choosing DPS")
                .Field(nameof(Email))
                .OnCompletion(processOrder)
                .Build();
        }
    }
}
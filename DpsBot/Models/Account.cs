using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpsBot.Models
{
    [Serializable]
    public class Account
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
        public string LastName { get; set; }

        [Prompt("Please enter your {&}")]
        public string Address { get; set; }

        [Prompt("Please enter your {&}")]
        [Pattern(@"(\(\d{3}\))?\s*\d{3}(-|\s*)\d{4}")]
        public string MobileNumber { get; set; }

    }

    [Serializable]
    public enum Title
    {
        Mr=1,
        Mrs=2,
        Miss=3,
        Ms=4,
        Dr=5
    }
}
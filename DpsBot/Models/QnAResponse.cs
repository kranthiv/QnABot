using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DpsBot.Models
{
    public class QnAResponse
    {
        [JsonProperty(PropertyName = "answer")]
        public string Answer { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}
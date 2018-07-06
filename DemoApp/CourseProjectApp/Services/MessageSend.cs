
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectApp.Services
{
    public class MessageSend : ISmsSend
    {
        public MessageSenderOptions Options { get; set; }
        public MessageSend(IOptions<MessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        public async Task SendSmsAsync(string number, string message)
        {
            using (var client = new HttpClient { BaseAddress = new Uri("https://api.twilio.com/") })
            {
                client.DefaultRequestHeaders.Authorization
                    = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String((Encoding.ASCII.GetBytes
                    ($"{Options.Sid}:{Options.AuthoToken}")
                    )));

                var contentSms = new FormUrlEncodedContent(
                    new[]
                    {
                        new KeyValuePair<string,string>("To",$"+{number}"),
                        new KeyValuePair<string, string>("From",$"+8699030082"),
                        new KeyValuePair<string, string>("Body",message)
                    });
                var results = await client
                    .PostAsync($"/2010-04-01/Accounts/{Options.Sid}/Message.json", contentSms)
                    .ConfigureAwait(false);

                     
            }
        }
    }
}

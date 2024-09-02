using Newtonsoft.Json;

namespace Mvc_Web_Application.Models
{
    public class LogonRequestViewModel
    {
        [JsonRequired]

        public string Username { get; set; }

        public string psw { get; set; }

    }
}

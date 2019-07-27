using Newtonsoft.Json;

namespace AuthToken.ViewModels.Models.Error
{
    public class ErrorResponseView
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}

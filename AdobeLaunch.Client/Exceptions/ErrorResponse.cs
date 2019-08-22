using Newtonsoft.Json;

namespace AdobeLaunch.Client.Exceptions
{
    public class ErrorResponse
    {
        [JsonProperty("error_description")] public string ErrorDescription { get; set; }
        [JsonProperty("error")] public string Error { get; set; }
    }
}
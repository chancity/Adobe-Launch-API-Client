using Newtonsoft.Json;

namespace AdobeShared.Exceptions
{
    public class ErrorResponse
    {
        [JsonProperty("error_description")] public string ErrorDescription { get; set; }
        [JsonProperty("error")] public string Error { get; set; }
    }
}
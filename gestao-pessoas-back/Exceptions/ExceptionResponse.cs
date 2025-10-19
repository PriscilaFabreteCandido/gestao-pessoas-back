using Newtonsoft.Json;

namespace gestao_pessoas_back.Exceptions
{
    public class ExceptionResponse
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

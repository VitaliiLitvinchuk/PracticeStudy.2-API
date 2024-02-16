using System.Text.Json.Serialization;

namespace Middleware.Handler
{
    public class BadRequestResponse
    {
        [JsonPropertyName("errors")]
        public required object Errors { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace ResourceHandler.Resources.ChatGPT
{
    public class CompletionRequest
    {
        [JsonPropertyName("model")]
        public string? Model
        {
            get;
            set;
        }
        [JsonPropertyName("prompt")]
        public string? Prompt
        {
            get;
            set;
        }
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens
        {
            get;
            set;
        }
    }
}

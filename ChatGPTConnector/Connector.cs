using System.Text.Json;
using System.Text;
using ResourceHandler.Resources.ChatGPT;

namespace ChatGPTConnector
{
    public static class Connector
    {

        public static async Task DoRequest(string text)
        {
            CompletionRequest completionRequest = new CompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = text,
                MaxTokens = 120
            };

            CompletionResponse completionResponse = new CompletionResponse();

            using (HttpClient httpClient = new HttpClient())
            {
                using (var httpReq = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/completions"))
                {
                    httpReq.Headers.Add("Authorization", $"Bearer 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");
                    string requestString = JsonSerializer.Serialize(completionRequest);
                    httpReq.Content = new StringContent(requestString, Encoding.UTF8, "application/json");
                    using (HttpResponseMessage? httpResponse = await httpClient.SendAsync(httpReq))
                    {
                        if (httpResponse is not null)
                        {
                            if (httpResponse.IsSuccessStatusCode)
                            {
                                string responseString = await httpResponse.Content.ReadAsStringAsync();
                                {
                                    if (!string.IsNullOrWhiteSpace(responseString))
                                    {
                                        completionResponse = JsonSerializer.Deserialize<CompletionResponse>(responseString);
                                    }
                                }
                            }
                        }
                        if (completionResponse is not null)
                        {
                            string? completionText = completionResponse.Choices?[0]?.Text;
                            Console.WriteLine(completionText);
                        }
                    }

                }
            }
        }
    }
}
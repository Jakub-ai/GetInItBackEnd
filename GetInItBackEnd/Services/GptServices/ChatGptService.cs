using System.Text;
using GetInItBackEnd.Models.chatGptDto;
using Newtonsoft.Json;

namespace GetInItBackEnd.Services.GptServices;

public class ChatGptService : IChatGptService
{
    private readonly IHttpClientFactory _clientFactory;

    public ChatGptService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string> GetResponseFromOpenAI(ChatPromptDto prompt)
    {
        var client = _clientFactory.CreateClient();
        client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "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");

        var content = new StringContent(JsonConvert.SerializeObject(new { prompt = prompt.Input }), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", content);

        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
        else
        {
            throw new Exception("Error connecting to OpenAI API");
        }
    }
}
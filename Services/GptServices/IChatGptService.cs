using GetInItBackEnd.Models.chatGptDto;

namespace GetInItBackEnd.Services.GptServices;

public interface IChatGptService
{
    Task<string> GetResponseFromOpenAI(ChatPromptDto prompt);
}
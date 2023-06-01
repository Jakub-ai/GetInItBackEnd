using GetInItBackEnd.Models.chatGptDto;
using GetInItBackEnd.Services.GptServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GetInItBackEnd.Controllers;
[Route("api/chatGpt")]
[ApiController]
public class ChatGptController : ControllerBase
{
    public IChatGptService GptService { get; }

    public ChatGptController(IChatGptService gptService)
    {
        GptService = gptService;
    }
    
    [HttpPost("sendPrompt")]
    public async Task<IActionResult> Post([FromBody] ChatPromptDto prompt)
    {
        try
        {
            var result = await GptService.GetResponseFromOpenAI(prompt);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
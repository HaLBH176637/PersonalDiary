using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotnetGeminiSDK.Client.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using DotnetGeminiSDK.Model.Response;

namespace PersonalDiaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AIController : ControllerBase
    {
        private readonly IGeminiClient _geminiClient;

        public AIController(IGeminiClient geminiClient)
        {
            _geminiClient = geminiClient;
        }
        [HttpPost("prompttext")]
        public async Task<ActionResult<GeminiMessageResponse>> PromptText(string text)
        {
            var response = await _geminiClient.TextPrompt("Gợi ý viết nhật ký" + text);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);
        }
    }
}

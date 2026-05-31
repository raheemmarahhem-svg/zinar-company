using Microsoft.AspNetCore.Mvc;
using ZinarCompany.Services;

namespace ZinarCompany.Controllers
{
    [ApiController]
    [Route("api/ai")]
    public class AiChatController : ControllerBase
    {
        private readonly GeminiService _gemini;

        public AiChatController(GeminiService gemini)
        {
            _gemini = gemini;
        }

        public class ChatReq
        {
            public string? Message { get; set; }
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] ChatReq req)
        {
            var msg = (req.Message ?? "").Trim();
            if (string.IsNullOrWhiteSpace(msg))
                return BadRequest(new { reply = "Write a message first." });

            if (msg.Length > 500) msg = msg[..500];

            var reply = await _gemini.AskAsync(msg);
            return Ok(new { reply });
        }
    }
}
using Messenger.IServices;
using Messenger.Models;
using Messenger.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChat(Guid id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            return Ok(chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var chats = await _chatService.GetAllAsync();
            return Ok(chats);
        }
            

        [HttpPost]
        public async Task<IActionResult> AddChat([FromBody] ChatDto chatDto )
        {
            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                Name = chatDto.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _chatService.AddAsync(chat);

            return CreatedAtAction(nameof(GetChat), new { id = chat.Id }, chat);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateChat(Guid id, [FromBody] Chat updateChat)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
            {
                return BadRequest();
            }
            chat.Name = updateChat.Name;
            chat.Id = updateChat.Id;
            await _chatService.UpdateAsync(chat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChat(Guid id)
        {
            var chat = await _chatService.GetByIdAsync(id);
            if (chat == null)
            {
                return NotFound();
            }
            await _chatService.DeleteAsync(id);
            return NoContent();
        }


    }
}

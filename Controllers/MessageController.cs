using Messenger.IServices;
using Messenger.Models;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Messages = await _messageService.GetAllAsync();
            return Ok(Messages);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(Guid id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(Guid id, Message message)
        {
            if (id != message.Id)
                return BadRequest();

            await _messageService.UpdateAsync(message);
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> AddMessage(Message message)
        {
            await _messageService.AddAsync(message);
            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            var messages = await _messageService.GetByIdAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            await _messageService.DeleteAsync(id);
            return NoContent();
        }


    }
}

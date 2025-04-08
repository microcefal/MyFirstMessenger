using Microsoft.AspNetCore.SignalR;

namespace Messenger.Data.Hubs
{
    public class ChatHub : Hub
    {
       
        
            public async Task SendMessage(Guid chatId, string user, string message)
            {
                await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", user, message);
            }

            public async Task JoinChat(Guid chatId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chatId.ToString());
            }

            public async Task LeaveChat(Guid chatId)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId.ToString());
            }
        }

    }


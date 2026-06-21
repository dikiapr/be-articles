using System.Security.Claims;
using ArtikelKu.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ArtikelKu.Api.Hubs;

[Authorize]
public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        var username = Context.User?.FindFirstValue(ClaimTypes.Name) ?? "Anonim";

        var payload = new ChatMessageDto(username, message.Trim(), DateTime.UtcNow);

        await Clients.All.SendAsync("ReceiveMessage", payload);
    }
}

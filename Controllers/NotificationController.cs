using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRServer.Controllers
{
[Route("api/notification")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private CancellationTokenSource _streamingTokenSource;

    public NotificationController(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationDto notification)
    {

        await _hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Sender, notification.Message);
        await Task.Delay(TimeSpan.FromSeconds(5));
      
        return Ok();
    }

    // public async Task<IActionResult> SendNotification()
    // {
    //       await Clients.Caller.SendAsync("StreamStarted");

    //     // Start a background task to send streaming data
       
    //         while (true)
    //         {
    //             // Generate or fetch streaming data
    //             var streamingData = GenerateStreamingData();

    //             // Send the data to the connected client
    //             await Clients.Caller.SendAsync("ReceiveStreamingData", streamingData);

    //             // Adjust the streaming interval as needed
    //             await Task.Delay(TimeSpan.FromSeconds(1));
    //         }
      
    //     return Ok();
    // }



}

public class NotificationDto
{
    public string Sender { get; set; }
    public string Message { get; set; }
}
}
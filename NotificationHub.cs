using Microsoft.AspNetCore.SignalR;
using System.Threading.Channels;
using System.Threading.Tasks;

public class NotificationHub : Hub
{

    private CancellationTokenSource _cancellationTokenSource;
        private Random _random = new Random();


    public async Task StartStreaming()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Console.WriteLine("StreamStarted");


        await Clients.All.SendAsync("StreamStarted");
        // Start a background task to send streaming data
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                Console.WriteLine("ReceiveStreamingData");

                // Generate or fetch streaming data
                var streamingData = GenerateStreamingData();

                // Send the data to the connected client
                await Clients.Caller.SendAsync("ReceiveStreamingData", streamingData);

                // Adjust the streaming interval as needed
                await Task.Delay(TimeSpan.FromSeconds(2));
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine("StreamStartedBreak");

                    break;
                }
            }
        
    }

    public void StopStreaming()
    {

          Console.WriteLine("StopStreaming");

        _cancellationTokenSource?.Cancel();
    }


    private string GenerateStreamingData()
   {
     // Simulate LTP fluctuations within a certain range (e.g., 100 to 200)
    double randomLtp = 100 + (_random.NextDouble() * 100);

    // Format the LTP as a string with a fixed number of decimal places
    string ltpFormatted = randomLtp.ToString("0.00");

    return $"LTP: {ltpFormatted}";
   }
    public async Task SendNotification(string message)
    {
      
        await Clients.All.SendAsync("ReceiveNotification", "message");

        
    }
}


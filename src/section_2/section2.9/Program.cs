using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();

// TODO for Coding Challenge Start here on starting-point branch
var subscriber = muxer.GetSubscriber();
var cancellationTokenSource = new CancellationTokenSource();
var token = cancellationTokenSource.Token;

var channel = await subscriber.SubscribeAsync("test-channel");

channel.OnMessage(msg =>
{
    Console.WriteLine($"Sequentially received: {msg.Message} on channel: {msg.Channel}");
});

await subscriber.SubscribeAsync("test-channel", (channel, value) =>
{
    Console.WriteLine($"Received: {value} on channel: {channel}");
});

var basicSendTask = Task.Run(async () =>
{
    var i = 0;
    while (!token.IsCancellationRequested)
    {
        await db.PublishAsync("test-channel", i++);
        await Task.Delay(1000);
    }
});

Console.ReadKey();

var patternSendTask = Task.Run(async () =>
{
    var i = 0;
    while (!token.IsCancellationRequested)
    {
        await db.PublishAsync($"pattern:{Guid.NewGuid()}", i++);
        await Task.Delay(1000);
    }
});

Console.WriteLine("Unsubscribing to a single channel");
await channel.UnsubscribeAsync();
Console.ReadKey();

Console.WriteLine("Unsubscribing whole subscriber from test-channel");
await subscriber.UnsubscribeAsync("test-channel");
Console.ReadKey();

Console.WriteLine("Unsubscribing from all");
await subscriber.UnsubscribeAllAsync();
Console.ReadKey();
// end coding challenge

using System.Diagnostics;
using StackExchange.Redis;

var options = new ConfigurationOptions
{
    EndPoints = new EndPointCollection { "localhost:6379" }
};

var muxer = ConnectionMultiplexer.Connect(options);
var db = muxer.GetDatabase();

var stopwatch = Stopwatch.StartNew();

// TODO for Coding Challenge Start here on starting-point branch
// un-pipelined commands incur the added cost of an extra round trip
var pingTasks = new List<Task<TimeSpan>>();

// Batches allow you to more intentionally group together the commands that you want to send to Redis.
// If you employee a batch, all commands in the batch will be sent to Redis in one contiguous block, with no
// other commands from the client interleaved. Of course, if there are other clients to Redis, commands from those
// other clients may be interleaved with your batched commands.
var batch = db.CreateBatch();

for (var i = 0; i < 1000; i++)
{
    pingTasks.Add(batch.PingAsync());
}

batch.Execute();
await Task.WhenAll(pingTasks);
Console.WriteLine($"1000 batched commands took: {stopwatch.ElapsedMilliseconds}ms to execute, first result: {pingTasks[0].Result}");
// end Challenge
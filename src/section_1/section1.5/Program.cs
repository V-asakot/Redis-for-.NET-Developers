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

for (var i = 0; i < 1000; i++)
{
    pingTasks.Add(db.PingAsync());
}

await Task.WhenAll(pingTasks);

Console.WriteLine($"1000 automatically pipelined tasks took: {stopwatch.ElapsedMilliseconds}ms to execute, first result: {pingTasks[0].Result}");
// end Challenge
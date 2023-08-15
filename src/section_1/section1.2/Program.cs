using StackExchange.Redis;

// TODO for Coding Challenge Start here on starting-point branch

using StackExchange.Redis;
Console.WriteLine("Hello Redis!");

var muxer = ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" }
});

var db = muxer.GetDatabase();
var res = db.Ping();
Console.WriteLine($"The ping took: {res.TotalMilliseconds} ms");

// end programming challenge
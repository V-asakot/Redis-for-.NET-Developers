using StackExchange.Redis;
var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();

// TODO for Coding Challenge Start here on starting-point branch
var allUsersSet = "users";
var activeUsersSet = "users:state:active";
var inactiveUsersSet = "users:state:inactive";
var offlineUsersSet = "users:state:offline";
db.KeyDelete(new RedisKey[] { allUsersSet, activeUsersSet, inactiveUsersSet, offlineUsersSet });

db.SetAdd(activeUsersSet, new RedisValue[] { "User:1", "User:2" });
db.SetAdd(inactiveUsersSet, new RedisValue[] { "User:3", "User:4" });
db.SetAdd(offlineUsersSet, new RedisValue[] { "User:5", "User:6", "User:7" });

db.SetCombineAndStore(SetOperation.Union, allUsersSet, new RedisKey[] { activeUsersSet, inactiveUsersSet, offlineUsersSet });

var user6Offline = db.SetContains(offlineUsersSet, "User:6");
Console.WriteLine($"User:6 offline: {user6Offline}");
Console.WriteLine($"All Users In one shot: {string.Join(", ", db.SetMembers(allUsersSet))}");
Console.WriteLine($"All Users with scan  : {string.Join(", ", db.SetScan(allUsersSet))}");
Console.WriteLine("Moving User:1 from active to offline");
var moved = db.SetMove(activeUsersSet, offlineUsersSet, "User:1");
Console.WriteLine($"Move Successful: {moved}");
Console.ReadKey();
// end coding challenge
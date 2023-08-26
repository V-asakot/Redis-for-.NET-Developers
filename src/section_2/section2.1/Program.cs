using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();
// TODO for Coding Challenge Start here on starting-point branch
var instructorNameKey = new RedisKey("instructors:1:name");

db.StringSet(instructorNameKey, "Steve");
var instructor1Name = db.StringGet(instructorNameKey);

Console.WriteLine($"Instructor 1's name is: {instructor1Name}");

db.StringAppend(instructorNameKey, " Lorello");
instructor1Name = db.StringGet(instructorNameKey);
Console.WriteLine($"Instructor 1's full name is: {instructor1Name}");

var tempKey = "temperature";
db.StringSet(tempKey, 42);
var tempAsLong = db.StringIncrement(tempKey, 5);
Console.WriteLine($"New temperature: {tempAsLong}");

tempAsLong = db.StringIncrement(tempKey);
Console.WriteLine($"New Temp: {tempAsLong}");

var tempAsDouble = db.StringIncrement(tempKey, .5);
Console.WriteLine($"New temperature: {tempAsDouble}");

db.StringSet("temporaryKey", "hello world", expiry: TimeSpan.FromSeconds(1));

var conditionalKey = "ConditionalKey";
var conditionalKeyText = "this has been set";
var wasSet = db.StringSet(conditionalKey, conditionalKeyText, when: When.NotExists);
Console.WriteLine($"Key set: {wasSet}");

wasSet = db.StringSet(conditionalKey, "this text doesn't matter since it won't be set", when: When.NotExists);
Console.WriteLine($"Key set: {wasSet}");

wasSet = db.StringSet(conditionalKey, "we reset the key!");
Console.WriteLine($"Key set: {wasSet}");

Console.ReadKey();
// end coding challenge
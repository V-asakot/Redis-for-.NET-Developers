using StackExchange.Redis;

var muxer = ConnectionMultiplexer.Connect("localhost");
var db = muxer.GetDatabase();
// TODO for Coding Challenge Start here on starting-point branch
var fruitKey = "fruits";
var vegetableKey = "vegetables";
db.KeyDelete(new RedisKey[] { fruitKey, vegetableKey });

db.ListLeftPush(fruitKey, new RedisValue[] { "Banana", "Mango", "Apple", "Pepper", "Kiwi", "Grape" });
Console.WriteLine($"The first fruit in the list is: {db.ListGetByIndex(fruitKey, 0)}");
Console.WriteLine($"The last fruit in the list is:  {db.ListGetByIndex(fruitKey, -1)}");
db.ListRightPush(vegetableKey, new RedisValue[] { "Potato", "Carrot", "Asparagus", "Beet", "Garlic", "Tomato" });
Console.WriteLine($"The first vegetable in the list is: {db.ListGetByIndex(vegetableKey, 0)}");
Console.WriteLine($"The last vegetable in the list is:  {db.ListGetByIndex(vegetableKey, -1)}");
Console.WriteLine($"Fruit indexes 0 to -1: {string.Join(", ", db.ListRange(fruitKey))}");
Console.WriteLine($"Vegetables index 0 to -2: {string.Join(", ", db.ListRange(vegetableKey, 0, -2))}");
db.ListMove(vegetableKey, fruitKey, ListSide.Right, ListSide.Left);

Console.WriteLine("Enqueuing Celery");
db.ListLeftPush(vegetableKey, "Celery");
Console.WriteLine($"Dequeued: {db.ListRightPop(vegetableKey)}");

Console.WriteLine("Pushing Grapefruit");
db.ListLeftPush(fruitKey, "Grapefruit");
Console.WriteLine($"Popping Fruit: {string.Join(",", db.ListLeftPop(fruitKey, 2))}");

Console.WriteLine($"Position of Mango: {db.ListPosition(fruitKey, "Mango")}");

Console.WriteLine($"There are {db.ListLength(fruitKey)} fruits in our Fruit List");
Console.ReadKey();
// end coding challenge
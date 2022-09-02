// See https://aka.ms/new-console-template for more information
using System.Diagnostics;
using ConsoleApp1;
using MongoDB.Bson.Serialization;

const int Iterations = 10000;
const int wormCount = 100;


var bson = testJson.Bson;

Console.WriteLine("Worming...");
for (int i = 0; i < wormCount; i++)
{
    var marketsFromB1 = BsonSerializer.Deserialize<TestEntity>(bson);
}

while (true)
{
    Console.WriteLine("Press any key to start test. Press when cpu level is low!");
    Console.ReadLine();
    Stopwatch stopwatch = Stopwatch.StartNew();
    for (int i = 0; i < Iterations; i++)
    {
        var marketsFromB = BsonSerializer.Deserialize<TestEntity>(bson);
    }
    stopwatch.Stop();
    var time = stopwatch.Elapsed;
    Console.WriteLine("Test time: " + time);
}
        
    

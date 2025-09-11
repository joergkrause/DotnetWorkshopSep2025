using System.Collections.Generic;
using TestConsole;
using static System.Console;

WriteLine("Hello, World!");


var tr = new TestRecord(1, "Test");
var tr2 = tr with { Name = "New Name" };

var secondsPerHour = new Func<int, int>((int minPerHour) => minPerHour * 60)(60);


var rn = TwoRandomNumbers();
rn.NumberOne.ToString();
rn.NumberTwo.ToString();

(int NumberOne, int NumberTwo) TwoRandomNumbers()
{
  const int i = 1;
  Random rand = new ();
  return (rand.Next(1, 100), rand.Next(1, 100));
}




using var cts = new CancellationTokenSource();

var longTask = LongRunningExample(cts.Token);

// Cancel the task after 3 seconds
try
{  
  cts.CancelAfter(3000);
  await longTask;
}
catch (OperationCanceledException)
{
  WriteLine("Task was canceled.");
}

static async Task LongRunningExample(CancellationToken token)
{
  for (int i = 0; i < 10; i++)
  {
    token.ThrowIfCancellationRequested();
    await Task.Delay(1000); // Simulate work
    WriteLine($"Iteration {i + 1} complete");
  }
}


async void Foreach()
{

  await foreach (var number in GenerateSequence(5))
  {
    WriteLine(number);
  }

  static async IAsyncEnumerable<int> GenerateSequence(int count)
  {
    for (int i = 0; i < count; i++)
    {
      await Task.Delay(500); // Simulate asynchronous work
      yield return i;
    }
  }
}

async void WhenAll()
{
  var myTasks = new List<Task>
{
  SimulateWork("1"),
  SimulateWork("2"),
  SimulateWork("3")
};

  await Task.WhenAll(myTasks);

  static async Task SimulateWork(string id)
  {
    await Task.Delay(2000);
    WriteLine($"{id} Work Complete");
  }
}

//static async Task Output(string message)
//{
//   Console.WriteLine(message);
//  //foreach (var item in Foo())
//  //{
//  //  Console.WriteLine(item);
//  //}
//  //static IEnumerable<string> Foo()
//  //{
//  //  yield return "One";
//  //  yield return "Two";
//  //  yield return "Three";
//  //}

//  //static IEnumerable<IEnumerable<TSource>> ChunkBy<TSource>(IEnumerable<TSource> source, int chunkSize)
//  //{
//  //while (source.Any())
//  //{
//  //  yield return source.Take(chunkSize);
//  //  source = source.Skip(chunkSize);
//  //}
//}



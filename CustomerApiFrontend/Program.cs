
Console.WriteLine("Hello, World!");


var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5214");
var client = new Workshop.BackendApi.Customer.CustomerClient(channel);
//
var reply = await client.GetCustomersAsync(new Workshop.BackendApi.CustomersRequest { });
var customers = reply.Customers;
foreach (var customer in customers)
{
  Console.WriteLine($"Customer: {customer.Id} - {customer.Name}");
}
// Benchmark
Console.WriteLine("Start");
var sw = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < 100; i++)
{
  var r = await client.GetCustomersAsync(new Workshop.BackendApi.CustomersRequest { });
  var cs = r.Customers;
  // foreach (var c in cs)
  // {
  //   Console.WriteLine($"Customer: {c.Id} - {c.Name}");
  // }
}
sw.Stop();
Console.WriteLine($"End: {sw.ElapsedMilliseconds} ms");


Console.ReadLine();
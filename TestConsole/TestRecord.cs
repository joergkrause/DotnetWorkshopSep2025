using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole;

public record TestRecord(int Id, string Name);

internal class Demo
{
  public Demo(IDemoPrivate d)
  {
    Id = d.Id;
    Name = d.Name;
  }

  public int Id { get; init; }
  public string Name { get; init; }
}

public interface IDemoPrivate
{
  int Id { get; }
  string Name { get; }
}

public class DemoPrivate : IDemoPrivate
{
  public int Id { get; init; }
  public string Name { get; init; }
  public DemoPrivate(int id, string name)
  {
    Id = id;
    Name = name;
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
  internal class IniTest
  {
    public int Id { get; init; }

    public string Name { get; init; }

  }


  internal class TestProp
  {

    public int MyProperty { get { return 2; } }

    private int _myPropertyBackingField = 2;
    public int MyPropertyGetSet
    {
      get => _myPropertyBackingField;
      set => _myPropertyBackingField = value;
    }

    public int MyPropertyLambda => 2;

    public int MyPropertyNoLambda = 2;

  }
}

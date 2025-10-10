using Bunit;
using Microsoft.AspNetCore.Components;
using Workshop.Shared.Ui.Components.Conditions;
using Workshop.UseCases.Mappings;

namespace Workshop.Backend.Tests;

public abstract class BunitTestContext : TestContextWrapper
{
  public virtual void Setup() => TestContext = Init();

  public virtual void TearDown() => TestContext?.Dispose();

  private static Bunit.TestContext Init()
  {
    var ctx = new Bunit.TestContext();
    // Additional setup can be done here if needed

    ctx.Services.AddServiceDependencies();

    return ctx;
  }

  public class ConditionContext : BunitTestContext
  {
    public override void Setup()
    {
      base.Setup();
      // Additional setup specific to ConditionContext can be done here
    }
    public override void TearDown()
    {
      // Additional teardown specific to ConditionContext can be done here
      base.TearDown();
    }
  }

  [TestClass]
  public class ConditionComponentTests : ConditionContext
  {

    [TestMethod]
    [DataRow("If Content", "Else Content")]
    [DataRow("If Content", "")]
    public void ConditionComponent_RendersCorrectly_False(string ifContent, string elseContent)
    {
      Setup();
      // Arrange
      var cut = TestContext?.RenderComponent<Condition>();

      // Act
      var isCheck = false;

      RenderFragment ifFragment = build =>
      {
        build.AddContent(1, ifContent);
      };
      RenderFragment elseFragment = build =>
      {
        build.AddContent(1, elseContent);
      };
      cut?.SetParametersAndRender(parameters => parameters
        .Add(p => p.Check, isCheck)
        .Add(p => p.Else, elseFragment)
        .Add(p => p.If, ifFragment));

      // Assert
      Assert.IsFalse(cut?.Markup.Contains(ifContent));
      Assert.IsTrue(cut?.Markup.Contains(elseContent));

    }

    [TestMethod]
    [DataRow("If Content", "Else Content")]
    [DataRow("If Content", "")]
    public void ConditionComponent_RendersCorrectly_True(string ifContent, string elseContent)
    {
      Setup();
      // Arrange
      var cut = TestContext?.RenderComponent<Condition>();

      // Act
      var isCheck = true;

      RenderFragment ifFragment = build =>
      {
        build.AddContent(1, ifContent);
      };
      RenderFragment elseFragment = build =>
      {
        build.AddContent(1, elseContent);
      };
      cut?.SetParametersAndRender(parameters => parameters
        .Add(p => p.Check, isCheck)
        .Add(p => p.Else, elseFragment)
        .Add(p => p.If, ifFragment));

      // Assert
      Assert.IsTrue(cut?.Markup.Contains(ifContent));
      Assert.IsFalse(cut?.Markup.Contains(elseContent));

    }

  }
}
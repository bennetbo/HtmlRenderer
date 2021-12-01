using BrowserEngine.Dom;

namespace BrowserEngine.UnitTests.Dom;

[TestFixture]
public class NodeExtensionsTests
{
  [Test]
  public void PrettyPrint_Should_Return_ExpectedResult()
  {
    var dom = new ElementNode(
      new ElementData("Tag"),
      new Node[]
      {
        new TextNode("Value")
      });

    string result = dom.PrettyPrint();

    Assert.That(result.Length > 0);
  }
}

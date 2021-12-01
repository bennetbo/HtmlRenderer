using BrowserEngine.Dom;
using BrowserEngine.Parser;

namespace BrowserEngine.UnitTests.Parser;

[TestFixture]
public class HtmlTests
{
  [Test]
  public void Parse()
  {
    string html = "<html><p>Test</p></html>";

    var dom = Html.Parse(html);

    Assert.That(dom, Is.Not.Null);
    Assert.That(dom, Has.Property(nameof(dom.Children)).With.Exactly(1).Items);
  }
}

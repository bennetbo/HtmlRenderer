using BrowserEngine.Dom;

namespace BrowserEngine.Parser
{
  public static class Html
  {
    public static Node Parse(string html) => new HtmlParser(html).Parse();
  }
}

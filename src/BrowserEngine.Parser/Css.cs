namespace BrowserEngine.Parser;

public static class Css
{
  public static Stylesheet Parse(string css) => new CssParser(css).Parse();
}

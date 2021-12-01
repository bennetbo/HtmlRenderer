namespace BrowserEngine.Dom.Css;

public record Rule
{
  public List<Selector> Selectors { get; }
  public List<Declaration> Declarations { get; }

  public Rule(IEnumerable<Selector> selectors, IEnumerable<Declaration> declarations)
  {
    Selectors = new(selectors);
    Declarations = new(declarations);
  }
}

namespace BrowserEngine.Dom.Css;

public record Stylesheet
{
  public List<Rule> Rules { get; }

  public Stylesheet(IEnumerable<Rule> rules)
  {
    Rules = new(rules);
  }
}

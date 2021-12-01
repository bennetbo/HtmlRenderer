namespace BrowserEngine.Dom.Css;

public record SimpleSelector : Selector
{
  public string? Id { get; init; }
  public string? TagName { get; init; }
  public List<string> Classes { get; init; } = new();

  public override Specificity Specificity() => new(Id?.Length ?? 0, Classes.Count, TagName?.Length ?? 0);
}

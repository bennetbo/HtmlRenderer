namespace BrowserEngine.Dom;

public record ElementData
{
  public string TagName { get; init; }
  public Dictionary<string, string> Attributes { get; }

  public ElementData(string tagName)
  {
    TagName = tagName;
    Attributes = new();
  }

  public ElementData(string tagName, IEnumerable<KeyValuePair<string, string>> attributes)
  {
    TagName = tagName;
    Attributes = new(attributes);
  }
}

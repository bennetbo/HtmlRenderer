namespace BrowserEngine.Dom;

public record TextNode : Node
{
  public string Value { get; }

  public TextNode(string value) : base(NodeType.Text)
  {
    Value = value;
  }
}

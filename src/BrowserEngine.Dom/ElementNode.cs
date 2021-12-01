namespace BrowserEngine.Dom;

public record ElementNode : Node
{
  public ElementData Data { get; }

  public ElementNode(ElementData data) : base(NodeType.Element)
  {
    Data = data;
  }

  public ElementNode(ElementData data, IEnumerable<Node> children) : base(NodeType.Element, children)
  {
    Data = data;
  }
}

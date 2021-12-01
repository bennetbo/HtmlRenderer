namespace BrowserEngine.Dom;

public abstract record Node
{
  public List<Node> Children { get; }
  public NodeType Type { get; init; }

  public Node(NodeType type)
  {
    Type = type;
    Children = new();
  }

  public Node(NodeType type, IEnumerable<Node> children)
  {
    Type = type;
    Children = new(children);
  }
}

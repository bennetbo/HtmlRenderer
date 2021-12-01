namespace BrowserEngine.Parser;

internal class HtmlParser : ParserBase
{
  private const char OpeningTag = '<';
  private const char ClosingTag = '>';
  private const string TagNameChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

  public HtmlParser(string input) : base(input) { }

  public Node Parse()
  {
    var nodes = ParseNodes();

    if (nodes.Count == 1)
      return nodes[0];

    return new ElementNode(new ElementData("html"), nodes);
  }

  protected string ParseTagName() => ConsumeWhile(TagNameChars.Contains);

  protected Node ParseNode() => NextChar switch
  {
    OpeningTag => ParseElement(),
    _ => ParseText()
  };

  protected Node ParseText() => new TextNode(ConsumeWhile(c => c != OpeningTag));
  protected Node ParseElement()
  {
    // Opening tag.
    ConsumeAndThrowIfNot(OpeningTag);
    string tagName = ParseTagName();
    var attrs = ParseAttributes();
    ConsumeAndThrowIfNot(ClosingTag);

    // Contents.
    var children = ParseNodes();

    // Closing tag.
    ConsumeAndThrowIfNot(OpeningTag);
    ConsumeAndThrowIfNot('/');
    if (ParseTagName() != tagName)
      ThrowMissingTag(tagName, Position);
    ConsumeAndThrowIfNot(ClosingTag);

    return new ElementNode(
      new ElementData(tagName, attrs),
      children);
  }

  protected KeyValuePair<string, string> ParseAttribute()
  {
    string name = ParseTagName();
    ConsumeAndThrowIfNot('=');
    string value = ParseAttributeValue();
    return new(name, value);
  }

  protected string ParseAttributeValue()
  {
    char quotes = ConsumeChar();
    if (quotes == '"' || quotes == '\'')
      ThrowMissingCharacter(quotes, Position);

    string value = ConsumeWhile(c => c != quotes);

    if (quotes == ConsumeChar())
      ThrowMissingCharacter(quotes, Position);

    return value;
  }

  protected Dictionary<string, string> ParseAttributes()
  {
    var dict = new Dictionary<string, string>();

    while (true)
    {
      ConsumeWhitespace();
      if (NextChar == ClosingTag)
        break;

      var pair = ParseAttribute();
      dict[pair.Key] = pair.Value;
    }

    return dict;
  }

  protected List<Node> ParseNodes() => ParseNodesLazy().ToList();
  protected IEnumerable<Node> ParseNodesLazy()
  {
    while (true)
    {
      ConsumeWhitespace();
      if (Eof || StartsWith("</"))
        break;

      yield return ParseNode();
    }
  }
}
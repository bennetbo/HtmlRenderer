namespace BrowserEngine.Parser;

internal class CssParser : ParserBase
{
  private const string Numbers = "0123456789";
  private const string ValidFloatChars = Numbers + ".";
  private const string ValidIdentifierChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-_" + Numbers;

  public CssParser(string input) : base(input) { }

  public Stylesheet Parse() => new(ParseRulesLazy());

  protected List<Rule> ParseRules() => ParseRulesLazy().ToList();
  protected IEnumerable<Rule> ParseRulesLazy()
  {
    while (true)
    {
      ConsumeWhitespace();
      if (Eof)
        yield break;

      yield return ParseRule();
    }
  }

  protected Rule ParseRule() => new(ParseSelectors(), ParseDeclarations());

  protected List<Selector> ParseSelectors()
    => ParseSelectorsLazyUnordered().OrderByDescending(s => s.Specificity()).ToList();

  protected IEnumerable<Selector> ParseSelectorsLazyUnordered()
  {
    while (true)
    {
      yield return ParseSimpleSelector();
      ConsumeWhitespace();
      char next = NextChar;
      if (next == ',')
      {
        ConsumeChar();
        ConsumeWhitespace();
      }
      else if (next == '{')
        break;
      else
        ThrowParseException($"Unexpected character {next} in selector list.");
    }
  }

  protected SimpleSelector ParseSimpleSelector()
  {
    string id = string.Empty;
    string tagName = string.Empty;
    var classes = new List<string>();

    while (!Eof)
    {
      char next = NextChar;

      if (next == '#')
      {
        ConsumeChar();
        id = ParseIdentifier();
      }
      else if (next == '.')
      {
        ConsumeChar();
        classes.Add(ParseIdentifier());
      }
      else if (next == '*')
      {
        //Universal selector.
        ConsumeChar();
      }
      else if (ValidIdentifier(next))
      {
        tagName = ParseIdentifier();
      }
      else
        break;
    }

    return new SimpleSelector()
    {
      Id = id,
      TagName = tagName,
      Classes = classes
    };
  }

  protected List<Declaration> ParseDeclarations() => ParseDeclarationsLazy().ToList();
  protected IEnumerable<Declaration> ParseDeclarationsLazy()
  {
    while (true)
    {
      ConsumeWhitespace();
      if (NextChar == '}')
      {
        ConsumeChar();
        break;
      }
      yield return ParseDeclaration();
    }
  }

  protected Declaration ParseDeclaration()
  {
    string vale = ParseIdentifier();
    ConsumeWhitespace();
    ConsumeAndThrowIfNot(':');
    ConsumeWhitespace();
    var value = ParseValue();
    ConsumeWhitespace();
    ConsumeAndThrowIfNot(';');

    return new Declaration(vale, value);
  }

  protected Value ParseValue()
  {
    var next = NextChar;

    if (char.IsNumber(next))
      return ParseLength();

    if (next == '#')
      return ParseColor();

    return new Value.Keyword(ParseIdentifier());
  }

  protected Value.Length ParseLength() => new(ParseFloat(), ParseUnit());

  protected float ParseFloat() => float.Parse(ConsumeWhile(ValidFloatChars.Contains));
  protected Unit ParseUnit()
    => ParseIdentifier().ToLower() switch
    {
      "px" => Unit.Px,
      _ => throw new ParseException("Unrecognized unit.")
    };

  protected Value.Color ParseColor()
  {
    ConsumeAndThrowIfNot('#');
    return new Value.Color(
      new Color(
        ParseByte(),
        ParseByte(),
        ParseByte()));
  }

  private byte ParseByte()
  {
    var s = new char[] { ConsumeChar(), ConsumeChar() };    
    return Convert.ToByte(new string(s), 16);
  }

  protected string ParseIdentifier() => ConsumeWhile(ValidIdentifier);

  protected bool ValidIdentifier(char c) => ValidIdentifierChars.Contains(c);
}

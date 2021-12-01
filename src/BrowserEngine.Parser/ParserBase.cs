namespace BrowserEngine.Parser;

internal abstract class ParserBase
{
  public int Position { get; private set; }
  public string Input { get; }

  public ParserBase(string input)
  {
    Input = input;
  }

  public char NextChar => Input[Position];
  public bool Eof => Position >= Input.Length;
  public ReadOnlySpan<char> Rest => Input.AsSpan()[Position..];

  public bool StartsWith(string str) => Rest.StartsWith(str);

  public void SkipChar() => Position++;

  public char ConsumeChar()
  {
    char c = NextChar;
    Position++;
    return c;
  }

  public void SkipWhile(Predicate<char> func)
  {
    while (Position < Input.Length && func(NextChar))
      ConsumeChar();
  }

  public string ConsumeWhile(Predicate<char> func)
  {
    var sb = new StringBuilder();

    while (Position < Input.Length && func(NextChar))
      sb.Append(ConsumeChar());

    return sb.ToString();
  }

  public void ConsumeWhitespace() => SkipWhile(char.IsWhiteSpace);

  public static void ThrowMissingCharacter(char c, int pos) => ThrowParseException($"Expected {c} at {pos}.");
  public static void ThrowMissingTag(string c, int pos) => ThrowParseException($"Expected tag {c} at {pos}.");
  public static void ThrowParseException(string msg) => throw new ParseException(msg);

  public void ConsumeAndThrowIfNot(char c)
  {
    if (ConsumeChar() != c)
      ThrowMissingCharacter(c, Position);
  }
}

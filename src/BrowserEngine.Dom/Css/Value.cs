namespace BrowserEngine.Dom.Css;

public abstract record Value
{
  public record Keyword(string Value) : Value;
  public record Length(float Value, Unit Unit) : Value;
  public record Color(Css.Color Value) : Value;
}

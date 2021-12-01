namespace BrowserEngine.Dom.Css;

public readonly struct Color
{
  public static readonly Color Empty = new();

  public readonly byte A;
  public readonly byte R;
  public readonly byte G;
  public readonly byte B;

  public Color(byte r, byte g, byte b) : this(255, r, g, b) { }
  public Color(byte a, byte r, byte g, byte b)
  {
    A = a;
    R = r;
    G = g;
    B = b;
  }
}
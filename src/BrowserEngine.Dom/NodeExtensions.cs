namespace BrowserEngine.Dom;

public static class NodeExtensions
{
  public static string PrettyPrint(this Node node)
  {
    var sb = new StringBuilder();

    PrettyPrintRecursive(sb, node);

    return sb.ToString();
  }

  private static void PrettyPrintRecursive(StringBuilder builder, Node node)
  {
    builder.AppendLine(node.ToString());

    foreach(var child in node.Children)
    {
      builder.Append('\t');
      PrettyPrintRecursive(builder, child);
    }
  }
}

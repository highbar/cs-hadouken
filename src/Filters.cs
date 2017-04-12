using System;

namespace Hadouken
{
  public static class Filters
  {
    public static readonly Predicate<SimpleMessage> HasContent = message =>
    {
      return message != null && message.Body != null && message.Body.Length > 0;
    };
    public static readonly Predicate<SimpleMessage> None = message => true;
  }
}

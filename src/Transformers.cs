using System;

using Newtonsoft.Json;

namespace Hadouken
{
  public static class Transformers
  {
    public static readonly Func<String, String> FromSNS = body => {
      var anonType = new { Body = "" };
      var message = JsonConvert.DeserializeAnonymousType(body, anonType);

      return message.Body;
    };
    public static readonly Func<String, String> None = body => body;
  }
}

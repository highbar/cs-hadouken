using System;

using Amazon.SQS.Model;

namespace Hadouken
{
  public class SimpleMessage
  {
    public string Id { get; set; }
    public string Body { get; set; }

    private Action _acknowledgement;
    private Message _rawMessage;

    public SimpleMessage()
    {
    }

    public SimpleMessage(Message message)
    {
      Id = message.MessageId;
      Body = message.Body;
      _rawMessage = message;
    }

    public SimpleMessage(Message message, Action acknowledgementAction) : this(message)
    {
      _acknowledgement = acknowledgementAction;
    }

    public void Acknowledge()
    {
      if(_acknowledgement != null)
      {
        _acknowledgement();
      }
    }
  }
}

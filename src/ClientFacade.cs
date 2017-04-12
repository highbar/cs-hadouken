using System;
using System.Collections.Generic;
using System.Linq;

namespace Hadouken
{
  public abstract class ClientFacade<T>
  {
    private Predicate<SimpleMessage> _filter;
    public Predicate<SimpleMessage> Filter
    {
      get
      {
        return _filter;
      }
      set
      {
        if(value == null)
        {
          throw new ArgumentNullException();
        }

        _filter = value;
      }
    }

    private Action<Exception> _transformErrorHandler;
    public Action<Exception> TransformErrorHandler
    {
      get
      {
        return _transformErrorHandler;
      }
      set
      {
        if(value == null)
        {
          _transformErrorHandler = DefaultTransformErrorHandler;
        }
        else
        {
          _transformErrorHandler = value;
        }
      }
    }

    private static void DefaultTransformErrorHandler(Exception error)
    {
      //log the error here
    }

    public ClientFacade()
    {
      Filter = Filters.None;
      TransformErrorHandler = DefaultTransformErrorHandler;
    }

    public IEnumerable<SimpleMessage> GetMessages()
    {
      return GetInputs()
        .Select(input => Transform(input, TransformErrorHandler))
        .Where(input => FilterMessage(input));
    }

    private bool FilterMessage(SimpleMessage message)
    {
      bool valid = Filter(message);

      if(!valid)
      {
        //log a warning here
      }

      return valid;
    }

    protected abstract SimpleMessage Transform(T input, Action<Exception> errorHandler);
    protected abstract IEnumerable<T> GetInputs();
  }
}

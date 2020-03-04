using System;

namespace ExGens.FiveSquare.Services
{
  public sealed class AuthorizationException : Exception
  {
    public AuthorizationException(string message) : base(message)
    {

    }

    public AuthorizationException(string message, Exception reason) : base(message, reason)
    {

    }
  }
}
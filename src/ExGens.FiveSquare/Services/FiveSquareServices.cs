namespace ExGens.FiveSquare.Services
{
  internal sealed class FiveSquareServices
  {
    public FiveSquare FiveSquare { get; private set; }

    public void Authenticate(string accessToken)
    {
      FiveSquare = new FiveSquare(accessToken);
    }
  }
}

using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map.Layers
{
  internal sealed class ConstantMetric : IVisitMetric
  {
    private readonly float m_value;

    public ConstantMetric(float value)
    {
      m_value = value;
    }

    /// <inheritdoc />
    public float GetMetric(Visits<Venue> visit) => m_value;
  }
}
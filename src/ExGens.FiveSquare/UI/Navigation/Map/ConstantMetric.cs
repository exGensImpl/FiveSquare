using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class ConstantMetric : IVisitMetric
  {
    private readonly float m_value;

    public ConstantMetric(float value)
    {
      m_value = value;
    }

    /// <inheritdoc />
    public float GetMetric(Visit visit) => m_value;
  }
}
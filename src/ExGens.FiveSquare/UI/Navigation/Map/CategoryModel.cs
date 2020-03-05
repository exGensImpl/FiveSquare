using ExGens.FiveSquare.Domain;

namespace ExGens.FiveSquare.UI.Navigation.Map
{
  internal sealed class CategoryModel : NotifyPropertyChangedTrait
  {
    private bool m_selected;

    public Category Category { get; }

    public int Visits { get; }

    public CategoryModel(Category category, int visits)
    {
      Category = category;
      Visits = visits;
      Selected = true;
    }

    public bool Selected
    {
      get => m_selected;
      set => OnPropertyChanged(ref m_selected, value);
    }

    /// <inheritdoc />
    public override string ToString() => $"{Category.Name} ({Visits})";
  }
}
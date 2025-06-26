using LIB.ViewModels;

namespace LIB.DAL;

public partial class Address
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string Side { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Street { get; set; } = null!;

    public virtual ICollection<Crime> Crimes { get; set; } = new List<Crime>();
    public static Address CreateModel(AddressViewModel viewModel)
    {
        return new Address
        {
            Number = viewModel.Number,
            Side = viewModel.Side.ToString(),
            ZipCode = viewModel.ZipCode,
            Street = viewModel.Street,
        };
    }
}

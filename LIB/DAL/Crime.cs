using LIB.ViewModels; 

namespace LIB.DAL;

public partial class Crime
{
    public int Id { get; set; }

    public int IdAddress { get; set; }

    public string Grade { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime Start { get; set; }

    public DateTime? End { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<HeroCrime> HeroCrimes { get; set; } = new List<HeroCrime>();

    public virtual Address IdAddressNavigation { get; set; } = null!;
    public static Crime CreateModel(CrimeViewModel viewModel, int addressId)
    {
        return new Crime
        {
            IdAddress = addressId,
            Grade = viewModel.Grade.ToString(),
            Type = viewModel.Type.ToString(),
            Description = viewModel.Description,
            Start = DateTime.Now,
            Status = false
        };
    }
    public void Solved() 
    {
        this.Status = true;
        this.End = DateTime.Now;
    }
}

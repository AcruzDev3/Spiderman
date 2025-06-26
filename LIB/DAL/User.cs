namespace LIB.DAL;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<HeroCrime> HeroCrimes { get; set; } = new List<HeroCrime>();
}

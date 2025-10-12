using LIB.Models;
using LIB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class UserViewModel : IViewModel<User>
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), StringLength(255)]
        public string Email { get; set; }

        [DataType(DataType.Password), StringLength(255)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Salt { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string? Image { get; set; }
        public void Create(User model)
        {
            this.Id = model.UserId;
            this.Name = model.Name;
            this.Email = model.Email;
            this.Role = model.Role.Name;
            this.Image = model.Image;
        }
    }
}

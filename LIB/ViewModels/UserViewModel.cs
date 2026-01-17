using LIB.Models;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress), StringLength(255)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string? Image { get; set; }

        public UserViewModel(User model) {
            try {
                if (model == null) throw new Exception("El usuario no puede ser nulo");
                this.Id = model.UserId;
                this.Name = model.Name;
                this.Email = model.Email;
                this.Role = model.Role.Name;
                this.Image = model.Image;
            } catch (Exception) {
                throw;
            }
        }
    }
}

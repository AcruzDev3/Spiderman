using LIB.Models;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "El id del usuario es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "EL nombre del usuario el obligatorio")]
        [StringLength(50, ErrorMessage = "La longitud del nombre no puede ser mayor a 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El formato del email no es válido")]
            [StringLength(255, ErrorMessage = "La longitud del email no pueder ser mayor a 255 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        public string Role { get; set; }

        [StringLength(300, ErrorMessage = "La longitud de la ruta de la imagen no puede ser mayor a 300 caracteres")]
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

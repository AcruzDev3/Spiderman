using LIB.DAL;
using LIB.Enums;
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
        [DataType(DataType.Password), StringLength(255)]
        public string Password { get; set; }
        [StringLength(100)]
        public string Salt { get; set; }

        [Required, EnumDataType(typeof(RoleType), ErrorMessage = "The role type is not valid")]
        public RoleType Role { get; set; }
        public static UserViewModel CreateViewModel(User model)
        {
            if(Enum.TryParse<RoleType>(model.Role, out RoleType validRole)) 
            {
                return new UserViewModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Role = validRole,
                };
            }
            return null;
        }
    }
}

using API.DTOs;
using LIB.DTOs;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class UserManager : IManager<UserViewModel, CreateUserRequest, User>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public UserManager(SpidermanContext context, CrimeManager crimeManager)
        {
            _context = context;
            _crimeManager = crimeManager;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            UserViewModel? viewModel = null;
            try {
                if (id < 0) throw new Exception("El id del usuario no es válido");

                User? model = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
                if (model == null) throw new Exception("El usuario no existe");

                viewModel = new UserViewModel(model);
            } catch(Exception) {
                throw;
            }
            return viewModel;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            List<UserViewModel> viewModels = new List<UserViewModel>();
            try {
                List<User>? models = await GetAllModels();
                
                if(models == null) throw new Exception("No se han podido obtener los usuarios");
                
                foreach (User model in models) viewModels.Add(new UserViewModel(model));
            }
            catch (Exception) {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateUserRequest dto)
        {
            try
            {
                Role? role = await VerifyRoleUser(dto.Role);
                
                if (role == null) throw new Exception("El rol del usuario no existe");

                UserViewModel viewModel = new UserViewModel(dto);

                if(await Exists(new UserViewModel(dto)) == null) throw new Exception("El usuario ya existe");


                User? model = await CreateModel(viewModel, role, dto.Password);
                if (model == null) throw new Exception("El usuario no es válido");
               
                await _context.Users.AddAsync(model);
                
                int rowAffected = await _context.SaveChangesAsync();
                if (rowAffected != 1) throw new Exception("No se pudo crear el usuario");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                if(id < 1) throw new Exception("El usuario no es válido");
                User? model = await GetModel(id);
                if(model == null) throw new Exception("El usuario no existe");

                // Delete the crimes associated with the user
                int rowsAffectedDeletedCrimes = await _crimeManager.DeleteAllCrimesAssociatedWhithId(model.UserId);
                if(rowsAffectedDeletedCrimes == -1) throw new Exception("No se pudieron eliminar los crímenes asociados al usuario");

                _context.Users.Remove(model);
                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el usuario");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User?> Exists(UserViewModel viewModel)
        {
            try
            {
                if (viewModel == null) throw new Exception("La vista modelo del usuario es nula")
                        ;
                return await _context.Users.AsNoTracking()
                    .FirstOrDefaultAsync(
                        u => u.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        u.Email.Equals(viewModel.Email, StringComparison.CurrentCultureIgnoreCase) &&
                        u.Role.Name.Equals(viewModel.Role, StringComparison.CurrentCultureIgnoreCase)
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<User?> CreateModel(UserViewModel viewModel, Role role, string password) {
            User? user = null;
            try {
                if (viewModel == null) throw new Exception("El modelo vista no es válido");
                if (role == null) throw new Exception("El rol del usuario no es válido");

                user = new User {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    RoleId = role.RoleId,
                    Image = viewModel.Image,
                    Password = password, // Aqui abría que hasear la contraseña
                };
            } catch (Exception) {
                throw;
            }
            return user;
        }

        private async Task<Role?> VerifyRoleUser(string roleName)
        {
            try {
                return await _context.Roles.FirstOrDefaultAsync(
                    r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)
                );
            } catch (Exception) {
                throw;
            }
        }

        private async Task<User?> GetModel(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == id);
        }

        private async Task<List<User>?> GetAllModels()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }
        
    }
}

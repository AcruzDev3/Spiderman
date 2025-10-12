using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class UserManager : IManager<UserViewModel>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public UserManager(SpidermanContext context, CrimeManager crimeManager)
        {
            _context = context;
            _crimeManager = crimeManager;
        }

        public async Task<UserViewModel> GetOne(int id)
        {
            UserViewModel viewModel = new UserViewModel();
            User? model = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (model != null) viewModel.Create(model);

            return viewModel;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            List<UserViewModel> viewModels = new List<UserViewModel>();
            try
            {
                List<User>? models = await GetAllModels();
                if(models == null) throw new Exception("No se han podido obtener los usuarios");
                foreach (User model in models)
                {
                    UserViewModel viewModel = new UserViewModel();
                    viewModel.Create(model);
                    viewModels.Add(viewModel);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return viewModels;
        }

        public async Task<int> Create(UserViewModel viewModel)
        {
            int rowAffected = 0;
            try
            {
                int existsUserId = await Exists(viewModel);
                if (existsUserId > 0) throw new Exception("El usuario ya existe");

                User? model = await CreateModel(viewModel);
                if (model == null) throw new Exception("El usuario no es válido");
                await _context.Users.AddAsync(model);
                rowAffected = await _context.SaveChangesAsync();
                if (rowAffected != 1) throw new Exception("No se pudo crear el usuario");
            }
            catch (Exception)
            {
                throw;
            }
            return rowAffected;
        }

        public async Task<int> Delete(int id)
        {
            int rowsAffected = 0;
            try
            {
                if(id < 1) throw new Exception("El usuario no es válido");
                User? model = await GetModel(id);
                if(model == null) throw new Exception("El usuario no existe");

                // Delete the crimes associated with the user
                int rowsAffectedDeletedCrimes = await _crimeManager.DeleteAllCrimesAssociatedWhithId(model.UserId);
                if(rowsAffectedDeletedCrimes == -1) throw new Exception("No se pudieron eliminar los crímenes asociados al usuario");

                _context.Users.Remove(model);
                rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el usuario");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        public async Task<int> Exists(UserViewModel viewModel)
        {
            if(viewModel == null) return 0;
            User? user = null;
            try
            {
                user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                    u.Email.Equals(viewModel.Email, StringComparison.CurrentCultureIgnoreCase) &&
                    u.Role.Name.Equals(viewModel.Role, StringComparison.CurrentCultureIgnoreCase)
                    );
                if (user == null) return 0;
            }
            catch (Exception)
            {
                throw;
            }
            return user.UserId;
        }

        private async Task<User?> CreateModel(UserViewModel viewModel)
        {
            if (viewModel == null) return null;

            User? model = null;
            try
            {
                int idRole = await VerifyRoleUser(viewModel.Role);
                if (idRole == 0) throw new Exception("El rol del usuario no es válido");

                model = new User()
                {
                    Name = viewModel.Name,
                    Email = viewModel.Email,
                    Image = viewModel.Image,
                    Password = viewModel.Password,
                    RoleId = idRole,
                };
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        private async Task<int> VerifyRoleUser(string roleName)
        {
            Role? role = null;
            try
            {
                role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
                if (role == null) return 0;
            }
            catch (Exception)
            {
                throw;
            }
            return role.RoleId;
        }

        private async Task<User?> GetModel(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        private async Task<List<User>?> GetAllModels()
        {
            return await _context.Users.ToListAsync();
        }
        
    }
}

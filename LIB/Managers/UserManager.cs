using LIB.DAL;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class UserManager
    {
        private readonly SpidermanContext _context;
        public UserManager(SpidermanContext context) 
        {
            _context = context;
        }

        public async Task<UserViewModel> GetOne(int id)
        {
            User model = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(model != null)
            {
                return UserViewModel.CreateViewModel(model);
            }
            return null;
        }
    }
}

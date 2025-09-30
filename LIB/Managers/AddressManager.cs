using LIB.ViewModels;
using LIB.DAL;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
namespace LIB.Managers
{
    public class AddressManager
    {
        private readonly SpidermanContext _context;
        public AddressManager(SpidermanContext context) {
            _context = context;
        }

        public async Task<AddressViewModel> GetOne(int id) {

            Address model = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (model == null) return null;
            else return AddressViewModel.CreateViewModel(model);
        }
        private async Task<Address> GetModel(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> Create(Address model)
        {
            int response = 0;
            try
            {
                await _context.Addresses.AddAsync(model);
                await _context.SaveChangesAsync();
                response = model.Id;
            }pppppp
            catch (Exception ex)
            {
                response = -1;
            }
            return response;
        }
        public async Task<int> Delete(int id)
        {
            int response = 0;
            try
            {
                Address model = await GetModel(id);
                _context.Addresses.Remove(model);
                await _context.SaveChangesAsync();
                response = 1;
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return response;
            
        }
    }
}

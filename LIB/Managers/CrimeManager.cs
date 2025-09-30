using Azure;
using LIB.DAL;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LIB.Managers
{
    public class CrimeManager
    {
        private readonly SpidermanContext _context;
        private readonly AddressManager _addressManager;
        public CrimeManager(SpidermanContext context, AddressManager addressManager)
        {
            _context = context;
            _addressManager = addressManager;
        }

        public async Task<List<CrimeViewModel>> Get()
        {
            List<CrimeViewModel> viewModels = new List<CrimeViewModel>();
            List<Crime> models =  await _context.Crimes.AsNoTracking().ToListAsync();
            foreach (Crime model in models)
            {
                AddressViewModel addressViewModel =  await _addressManager.GetOne(model.IdAddress);
                if (addressViewModel == null) continue;
                viewModels.Add(CrimeViewModel.CreateViewModel(model, addressViewModel));
            }
            return viewModels; 
        }
        public async Task<CrimeViewModel> GetOne(int id)
        {
            if (id <= 0) return null;
            Crime model = await _context.Crimes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            AddressViewModel addressViewModel = await _addressManager.GetOne(id);
            if(addressViewModel != null)
            {
                return CrimeViewModel.CreateViewModel(model, addressViewModel);
            }
            return null;
        }

        private async Task<Crime> GetModel(int id)
        {
            if (id <= 0) return null;
            return await _context.Crimes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<int> Create(CrimeViewModel viewModel)
        {
            int response = 0;
            try
            {
                int idAddress = await _addressManager.Create(Address.CreateModel(viewModel.Address));
                Crime model = Crime.CreateModel(viewModel, idAddress);
                await _context.Crimes.AddAsync(model);
                await _context.SaveChangesAsync();
                response = 1;
            }
            catch(Exception ex)
            {
                response = -1;
            }
            return response;
        }
        public async Task<int> Solved(int idCrime)
        {
            int response = 0;
            try
            {
                if (idCrime <= 0) return -1;
                Crime crime = await GetModel(idCrime);
                if (crime == null) return -1;
                crime.Solved();
                _context.Crimes.Update(crime);
                _context.SaveChanges();
                response = 1;
            }
            catch (Exception ex)
            {
                response = -1;
            }
            return response;
        }

        public async Task<int> Delete(int id)
        {
            int response = 0;
            try {
                if (id <= 1) return -1;
                Crime crime = await GetModel(id);
                if (crime == null) return -2;
                int responseAddress = await _addressManager.Delete(crime.IdAddress);
                if (responseAddress != 1) return responseAddress;
                _context.Crimes.Remove(crime);
                await _context.SaveChangesAsync();
                response = 1;
            }
            catch(Exception ex)
            {
                response = -1;
            }
            return response;
        }
    }
}

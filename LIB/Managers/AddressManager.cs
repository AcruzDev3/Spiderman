using LIB.Enums;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
namespace LIB.Managers
{
    public class AddressManager: IManager<AddressViewModel>
    {
        private readonly SpidermanContext _context;
        public AddressManager(SpidermanContext context)
        {
            _context = context;
        }

        public async Task<AddressViewModel> GetById(int id)
        {
            AddressViewModel? addresViewModel = null;
            try
            {
                Address? model = await GetModel(id);
                if (model == null) throw new Exception("No se pudo encontrar la direccion");
                addresViewModel = new AddressViewModel(model);  
            }
            catch(Exception)
            {
                throw;
            }
            return addresViewModel; 
        }
        
        public async Task<List<AddressViewModel>> GetAll()
        {
            List<AddressViewModel> viewModels = new List<AddressViewModel>();
            try {
                List<Address>? models = await GetAllModels();
                
                if (models == null) throw new Exception("No se han podido obtener las direcciones");
                
                foreach (Address model in models) 
                    viewModels.Add(new AddressViewModel(model));
            }
            catch (Exception) 
            {
                throw;
            }
            return viewModels;
        }

        public async Task<int> Create(AddressViewModel viewModel)
        {
            int existingAddressId = 0;
            Address? model = null;

            try
            {
                if(viewModel == null) throw new Exception("La dirección no es válida");
                existingAddressId = await Exists(viewModel);
                if (existingAddressId == 0)
                {
                    model = new Address(viewModel);
                    await _context.Addresses.AddAsync(model);
                    int rowsAffected = await _context.SaveChangesAsync();
                    if (rowsAffected != 1) throw new Exception("No se ha podido crear la direccion");
                }
            }
            catch (Exception) 
            {
                throw;            
            }
            return model!.AddressId;
        }

        public async Task<int> Delete(int id)
        {
            int rowsAffected = 0;
            try
            {
                Address? model = await GetModel(id);
                if (model == null) throw new Exception("La direccion no existe");
                _context.Addresses.Remove(model);
                rowsAffected = await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }
        public async Task<int> Exists(AddressViewModel viewModel)
        {
            Address? model = null;
            try
            {
                if (viewModel == null) return 0;
                model = await _context.Addresses
                    .FirstOrDefaultAsync(m => m.Street.Equals(viewModel.Street, StringComparison.CurrentCultureIgnoreCase) &&
                    m.ZipCode.Equals(viewModel.ZipCode, StringComparison.CurrentCultureIgnoreCase) &&
                    m.Side.Equals(viewModel.Side.ToString(), StringComparison.CurrentCultureIgnoreCase) &&
                    m.Number == viewModel.Number
                    );
                if(model == null) return 0;
            }
            catch (Exception)
            {
                throw;
            }
            return model.AddressId;
        }
        private async Task<Address?> GetModel(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a => a.AddressId == id);
        }

        private async Task<List<Address>> GetAllModels()
        {
            return await _context.Addresses.AsNoTracking().ToListAsync();
        }
    }
}

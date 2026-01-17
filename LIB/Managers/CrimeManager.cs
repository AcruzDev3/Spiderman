using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class CrimeManager : IManager<CrimeViewModel>
    {
        private readonly SpidermanContext _context;
        private readonly AddressManager _addressManager;
        public CrimeManager(SpidermanContext context, AddressManager addressManager)
        {
            this._context = context;
            this._addressManager = addressManager;
        }

        public async Task<CrimeViewModel> GetById(int id)
        {
            CrimeViewModel? viewModel = null;
            try {

                Crime? model = await GetModel(id);
                if(model == null) throw new Exception("No se pudo encontrar el crimen");
                viewModel = new CrimeViewModel();
                viewModel.Create(model);
            }
            catch (Exception) {
                throw;
            }
            return viewModel;
        }
        public async Task<List<CrimeViewModel>> GetAll()
        {
            List<CrimeViewModel> viewModels = new List<CrimeViewModel>();

            try
            {
                List<Crime>?  models = await GetAllModels();
                if(models == null) throw new Exception("No se han podido obtener los crimenes");
                foreach (Crime model in models)
                {
                    CrimeViewModel viewModel = new CrimeViewModel();
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

        public async Task<int> Create(CrimeViewModel viewModel)
        {
            int rowsAffected = 0;
            try
            {
                int existingCrimeId = await Exists(viewModel);
                if(existingCrimeId > 0) throw new Exception("El crimen ya existe");

                Crime? model = await CreateModel(viewModel);
                if(model == null) throw new Exception("El crimen no es válido");
                await _context.Crimes.AddAsync(model);
                rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo crear el crimen");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        public async Task<int> Delete(int id)
        {
            int rowsAffected = 0;

            try
            {
                if(id < 1) throw new Exception("El crimen no es válido");
                Crime? crime = await GetModel(id);
                if (crime == null) throw new Exception("El crimen no existe");

                AddressViewModel? address = await _addressManager.GetOne(crime.AddressId);
                if (address == null) throw new Exception("La dirección asociada al crimen no existe");

                int rowsAffectedDeleteAddress = await _addressManager.Delete(address.Id);
                if(rowsAffectedDeleteAddress != 1) throw new Exception("No se pudo eliminar la dirección asociada al crimen");

                _context.Crimes.Remove(crime);
                rowsAffected = await _context.SaveChangesAsync();
                if(rowsAffected != 1) throw new Exception("No se pudo eliminar el crimen");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }
        
        public async Task<int> DeleteAllCrimesAssociatedWhithId(int id)
        {
            int rowsAffected = -1;
            try
            {
                _context.Crimes.RemoveRange(_context.Crimes.Where(c => c.Criminals.Any(cr => cr.CriminalId == id)));
            }
            catch(Exception)
            {
                throw;
            }
            return rowsAffected;
        }
        public async Task<int> Exists(CrimeViewModel viewModel)
        {
            if (viewModel == null) return 0;
            Crime? model = null;
            try
            {
                model = await _context.Crimes
                    .FirstOrDefaultAsync(m =>
                        m.Grade.Name.Equals(viewModel.Grade, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Type.Name.Equals(viewModel.Type, StringComparison.CurrentCultureIgnoreCase) &&
                        m.DateStart == viewModel.Start &&
                        m.DateEnd == viewModel.End &&
                        m.Status == viewModel.Status &&
                        m.AddressId == viewModel.Address.Id
                    );
                if (model == null) return 0;
            }
            catch (Exception)
            {
                throw;
            }
            return model.CrimeId;
        }

        public async Task<int> Solved(int idCrime)
        {
            int rowsAffected = 0;
            try
            {
                if (idCrime <= 0) return 0;
                Crime? crime = await GetModel(idCrime);
                if (crime == null) throw new Exception("No se pudo encontrar el crimen");

                crime.Status = true;
                crime.DateEnd = DateTime.Now;

                _context.Crimes.Update(crime);
                rowsAffected = await _context.SaveChangesAsync();
                if(rowsAffected != 1) throw new Exception("No se pudo actualizar el crimen");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        private async Task<int> VerifyGradeCrime(string gradeName)
        {
            CrimeGrade? gradeModel = null;
            try
            {
                gradeModel = await _context.CrimeGrades.FirstOrDefaultAsync(m => m.Name.Equals(gradeName, StringComparison.CurrentCultureIgnoreCase));
                if (gradeModel == null) throw new Exception("El grado del crimen no es válido");
            }
            catch (Exception)
            {
                throw;
            }
            return gradeModel.CrimeGradeId;
        }

        private async Task<int> VerifyTypeCrime(string typeName)
        {
            CrimeType? typeModel = null;
            try
            {
                typeModel = await _context.CrimeTypes.AsNoTracking().FirstOrDefaultAsync(m => m.Name.Equals(typeName, StringComparison.CurrentCultureIgnoreCase));
                if (typeModel == null) throw new Exception("El tipo del crimen no es válido");
            }
            catch (Exception)
            {
                throw;
            }
            return typeModel.CrimeTypeId;
        }

        private async Task<Crime?> GetModel(int id)
        {
            if (id <= 0) return null;
            return await _context.Crimes.AsNoTracking().FirstOrDefaultAsync(c => c.CrimeId == id);
        }
        private async Task<List<Crime>> GetAllModels()
        {
            return await _context.Crimes.AsNoTracking().ToListAsync();
        }
        private async Task<Crime?> CreateModel(CrimeViewModel viewModel)
        {
            Crime crime = new Crime();
            if (viewModel == null) return null;
            try
            {
                int idGrade = await VerifyGradeCrime(viewModel.Grade);
                if (idGrade == 0) throw new Exception("El grado del crimen no es válido");
                int idType = await VerifyTypeCrime(viewModel.Type);
                if (idType == 0) throw new Exception("El tipo del crimen no es válido");

                crime = new Crime
                {
                    AddressId = viewModel.Address.Id,
                    GradeId = idGrade,
                    TypeId = idType,
                    Description = viewModel.Description,
                    DateStart = viewModel.Start,
                    DateEnd = viewModel.End,
                    Status = viewModel.Status
                };
            }
            catch (Exception)
            {
                throw;
            }
            return crime;
        }
    }
}

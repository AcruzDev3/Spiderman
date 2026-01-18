using API.DTOs;
using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class CriminalManager : IManager<CriminalViewModel, CreateCriminalRequest, Criminal>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public CriminalManager(SpidermanContext context, CrimeManager crimeManager)
        {
            _context = context;
            _crimeManager = crimeManager;
        }
        public async Task<CriminalViewModel> GetById(int id)
        {
            CriminalViewModel? viewModel = null;
            try
            {
                Criminal? model = await GetModel(id);
                if (model == null) throw new Exception("No se pudo encontrar el criminal");
                viewModel = new CriminalViewModel(model);
            }
            catch (Exception)
            {
                throw;
            }
            return viewModel;
        }

        public async Task<List<CriminalViewModel>> GetAll()
        {
            List<CriminalViewModel> viewModels = new List<CriminalViewModel>();
            try
            {
                List<Criminal>? models = await GetAllModels();
                if (models == null) throw new Exception("No se han podido obtener los criminales");
                foreach (Criminal model in models)
                {
                    CriminalViewModel viewModel = new CriminalViewModel(model);
                    viewModels.Add(viewModel);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return viewModels;
        }

        public async Task Create(CreateCriminalRequest dto)
        {
            try
            {
                CriminalRiskLevel? riskLevel = await VerifyRiskCriminal(dto.Risk);
                if (riskLevel == null) throw new Exception("El nivel de riesgo no es válido");

                CriminalViewModel viewModel = new CriminalViewModel(dto, riskLevel);
                if(await Exists(viewModel) == null) throw new Exception("El criminal ya existe");

                Criminal? model = await CreateModel(viewModel, riskLevel);

                if(model == null) throw new Exception("No se pudo crear el criminal");

                await _context.Criminals.AddAsync(model);
                int rowsAffected = await _context.SaveChangesAsync();

                if(rowsAffected != 1) throw new Exception("No se pudo crear el criminal");
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
                if (id < 1) throw new Exception("El id del criminal no es válido");
                
                Criminal? criminal = await GetModel(id);
                if (criminal == null) throw new Exception("No se pudo encontrar el criminal");

                int rowsDeleted = await _crimeManager.DeleteAllCrimesAssociatedWhithId(id);
                if (rowsDeleted < 0) throw new Exception("No se pudieron eliminar los crímenes asociados al criminal");
                
                _context.Criminals.Remove(criminal);
                int rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el criminal");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Criminal?> Exists(CriminalViewModel viewModel)
        {
            if (viewModel == null) throw new Exception("El modelo de vista del criminal es nulo");
            try
            {
                return await _context.Criminals
                    .FirstOrDefaultAsync(m => m.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Risk.Name.Equals(viewModel.Risk, StringComparison.CurrentCultureIgnoreCase) &&
                        m.CriminalSince == viewModel.Since
                    );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<CriminalRiskLevel?> VerifyRiskCriminal(string riskName)
        {
            try
            {
                return await _context.CriminalRiskLevels.FirstOrDefaultAsync(
                    m => m.Name.Equals(riskName, StringComparison.CurrentCultureIgnoreCase)
                );
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<Criminal> CreateModel(CriminalViewModel viewModel, CriminalRiskLevel risk) {
            Criminal? criminal = null;
            try {
                if (viewModel == null) throw new Exception("El modelo de vista del criminal es nulo");
                if(risk == null) throw new Exception("El nivel de riesgo del criminal es nulo");

                criminal = new Criminal {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    RiskId = risk.CriminalRiskLevelId,
                    Image = viewModel.Image,
                    CriminalSince = viewModel.Since,
                };
            } catch (Exception) {
                throw;
            }
            return criminal;
        }

        private async Task<Criminal?> GetModel(int id) {
            return await _context.Criminals.FirstOrDefaultAsync(m => m.CriminalId == id);
        }

        private async Task<List<Criminal>?> GetAllModels() {
            return await _context.Criminals.ToListAsync();
        }
    }
}

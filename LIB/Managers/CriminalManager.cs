using LIB.Interfaces;
using LIB.Models;
using LIB.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LIB.Managers
{
    public class CriminalManager : IManager<CriminalViewModel>
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        public CriminalManager(SpidermanContext context, CrimeManager crimeManager)
        {
            _context = context;
            _crimeManager = crimeManager;
        }
        public async Task<CriminalViewModel> GetOne(int id)
        {
            CriminalViewModel? viewModel = null;
            try
            {
                Criminal? model = await GetModel(id);
                if (model == null) throw new Exception("No se pudo encontrar el criminal");
                viewModel = new CriminalViewModel();
                viewModel.Create(model);
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
                    CriminalViewModel viewModel = new CriminalViewModel();
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
        public async Task<int> Create(CriminalViewModel viewModel)
        {
            int rowsAffected = 0;
            try
            {
                int existingCriminalId = await Exists(viewModel);
                if(existingCriminalId > 0) throw new Exception("El criminal ya existe");

                Criminal? model = await CreateModel(viewModel);
                if(model == null) throw new Exception("No se pudo crear el criminal");
                await _context.Criminals.AddAsync(model);
                rowsAffected = await _context.SaveChangesAsync();
                if(rowsAffected != 1) throw new Exception("No se pudo crear el criminal");
            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        private async Task<Criminal?> CreateModel(CriminalViewModel viewModel)
        {
            Criminal? criminal = null;
            if (viewModel == null) return null;
            try
            {
                int idRisk = await VerifyRiskCriminal(viewModel.Risk);

                criminal = new Criminal
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    RiskId = idRisk,
                    Image = viewModel.Image,
                    CriminalSince = viewModel.CriminalSince,
                };
            }
            catch (Exception)
            {
                throw;
            }
            return criminal;
        }

        
        public async Task<int> Delete(int id)
        {
            int rowsAffected = 0;
            try
            {
                if (id < 1) throw new Exception("El id del criminal no es válido");
                
                Criminal? criminal = await GetModel(id);
                if (criminal == null) throw new Exception("No se pudo encontrar el criminal");

                int rowsDeleted = await _crimeManager.DeleteAllCrimesAssociatedWhithId(id);
                if (rowsDeleted < 0) throw new Exception("No se pudieron eliminar los crímenes asociados al criminal");
                
                _context.Criminals.Remove(criminal);
                rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected != 1) throw new Exception("No se pudo eliminar el criminal");


            }
            catch (Exception)
            {
                throw;
            }
            return rowsAffected;
        }

        public async Task<int> Exists(CriminalViewModel viewModel)
        {
            Criminal? model = null;
            if (viewModel == null) return 0;
            try
            {
                model = await _context.Criminals
                    .FirstOrDefaultAsync(m => m.Name.Equals(viewModel.Name, StringComparison.CurrentCultureIgnoreCase) &&
                        m.Risk.Name.Equals(viewModel.Risk, StringComparison.CurrentCultureIgnoreCase) &&
                        m.CriminalSince == viewModel.CriminalSince
                        );
                if (model == null) return 0;
            }
            catch (Exception)
            {
                throw;
            }
            return model.CriminalId;
        }

        private async Task<int> VerifyRiskCriminal(string riskName)
        {
            CriminalRiskLevel? risk = null;
            try
            {
                risk = await _context.CriminalRiskLevels.FirstOrDefaultAsync(m => m.Name.Equals(riskName, StringComparison.CurrentCultureIgnoreCase));
                if (risk == null) throw new Exception("El nivel de riesgo no es válido");
            }
            catch (Exception)
            {
                throw;
            }
            return risk.CriminalRiskLevelId;
        }

        private async Task<Criminal?> GetModel(int id)
        {
            return await _context.Criminals.FirstOrDefaultAsync(m => m.CriminalId == id);
        }

        private async Task<List<Criminal>?> GetAllModels()
        {
            return await _context.Criminals.ToListAsync();
        }
    }
}

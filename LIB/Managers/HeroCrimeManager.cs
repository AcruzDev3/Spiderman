using LIB.DAL;
using LIB.ViewModels;
using LIB.Managers;
using Microsoft.EntityFrameworkCore;

namespace LIB.Managers
{
    public class HeroCrimeManager
    {
        private readonly SpidermanContext _context;
        private readonly CrimeManager _crimeManager;
        private readonly UserManager _userManager;
        public HeroCrimeManager(SpidermanContext context, CrimeManager crimeManager, UserManager userManager)
        {
            _context = context;
            _crimeManager = crimeManager;
            _userManager = userManager;
        }
        public async Task<List<HeroCrimeViewModel>>Get()
        {
            List<HeroCrime> models = await _context.HeroCrimes.ToListAsync();
            List<HeroCrimeViewModel> viewModels = new List<HeroCrimeViewModel>();
            try
            {
                foreach (HeroCrime model in models)
                {
                    CrimeViewModel crimeViewModel = await _crimeManager.GetOne(model.IdCrime);
                    UserViewModel userViewModel = await _userManager.GetOne(model.IdHero);
                    viewModels.Add(HeroCrimeViewModel.CreateViewModel(userViewModel, crimeViewModel));
                }
            }
            catch (Exception ex)
            {
                viewModels = new List<HeroCrimeViewModel>();
            }
            return viewModels;
        }
        private async Task<HeroCrime> GetModel(int idCrime, int idHero)
        {
            return await _context.HeroCrimes.AsNoTracking().FirstOrDefaultAsync(hc => hc.IdCrime == idCrime && hc.IdHero == idHero);
        }
        public async Task<int> Create(int idCrime, int idHero)
        {
            int response = 0;
            try
            {
                if (idCrime >= 0 && idHero <= 0) return -1;
                if (GetModel(idCrime, idHero) != null) return -2;
                HeroCrime model = HeroCrime.CreateModel(idCrime, idHero);
                await _context.HeroCrimes.AddAsync(model);
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

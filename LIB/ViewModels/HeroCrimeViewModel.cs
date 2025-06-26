namespace LIB.ViewModels
{
    public class HeroCrimeViewModel
    {
        UserViewModel Hero { get; set; }
        CrimeViewModel Crime { get; set; }

        public static HeroCrimeViewModel CreateViewModel(UserViewModel userViewModel, CrimeViewModel crimeViewModel)
        {
            return new HeroCrimeViewModel
            {
                Hero = userViewModel,
                Crime = crimeViewModel
            };
        }
    }
}

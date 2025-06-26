using LIB.DAL;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required, EnumDataType(typeof(SideType))]
        public SideType Side { get; set; }

        [Required, StringLength(5)]
        public string ZipCode { get; set; }

        [Required, StringLength(150)]
        public string Street { get; set; }
        
        public static AddressViewModel CreateViewModel(Address model)
        {
            if(Enum.TryParse<SideType>(model.Side, out SideType validSide))
            {
                return new AddressViewModel
                {
                    Id = model.Id,
                    ZipCode = model.ZipCode,
                    Number = model.Number,
                    Street = model.Street,
                    Side = validSide
                };
            }
            return null;
        }
    }

}

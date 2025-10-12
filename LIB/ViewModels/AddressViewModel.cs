using LIB.Models;
using LIB.Enums;
using LIB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class AddressViewModel : IViewModel<Address>
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

        public void Create(Address model)
        {
            if (Enum.TryParse<SideType>(model.Side, out SideType validSide)) throw new Exception("El side no es válido");

            this.Id = model.AddressId;
            this.ZipCode = model.ZipCode;
            this.Number = model.Number;
            this.Street = model.Street;
            this.Side = validSide;
        }
    }

}

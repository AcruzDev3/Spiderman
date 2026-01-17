using LIB.Models;
using LIB.Enums;
using LIB.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class AddressViewModel
    {
        [Required, Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Number { get; set; }

        [Required, EnumDataType(typeof(SideType))]
        public SideType Side { get; set; }

        [Required, StringLength(5)]
        public string ZipCode { get; set; }

        [Required, StringLength(150)]
        public string Street { get; set; }

        public AddressViewModel(Address model) {
            try {
                if (model == null) throw new Exception("La dirección no puede ser nulo");
                if (!Enum.TryParse<SideType>(model.Side, out SideType validSide)) 
                    throw new Exception("El side no es válido");

                this.Id = model.AddressId;
                this.ZipCode = model.ZipCode;
                this.Number = model.Number;
                this.Street = model.Street;
                this.Side = validSide;
            } catch(Exception) {
                throw;
            }
        }
        public void Create(Address model)
        {
            
        }
    }

}

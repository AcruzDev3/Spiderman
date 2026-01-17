using LIB.Interfaces;
using LIB.Models;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class CrimeViewModel
    {
        public int Id { get; set; }
        [Required]
        public AddressViewModel Address { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        [Required]
        public bool Status { get; set; }

        public CrimeViewModel(Crime model, AddressViewModel address) {
            try {
                if (model == null) throw new Exception("El crimen no puede ser nulo");
                if (address == null) throw new Exception("La dirección no puede ser nula");
                this.Id = model.CrimeId;
                this.Address = address;
                this.Grade = model.Grade.Name;
                this.Type = model.Type.Name;
                this.Description = model.Description ?? string.Empty;
                this.Start = model.DateStart;
                this.Status = model.Status;
                this.End = model?.DateEnd;
            } catch(Exception) {
                throw;
            }
        }
    }
}
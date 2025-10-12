using LIB.Interfaces;
using LIB.Models;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class CrimeViewModel : IViewModel<Crime>
    {
        public int Id { get; set; }
        public AddressViewModel Address { get; set; }

        [Required]
        public string Grade { get; set; }

        [Required]
        public string Type { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        [Required]
        public bool Status { get; set; }


        public void Create(Crime model)
        {
            // Assess the level and category of the crime
            this.Id = model.CrimeId;

            //this.Address = model.Ad
            this.Grade = model.Grade.Name;
            this.Type = model.Type.Name;
            this.Description = model.Description;
            this.Start = model.DateStart;
            this.Status = model.Status;
            this.End = model?.DateEnd;

        }
    }
}
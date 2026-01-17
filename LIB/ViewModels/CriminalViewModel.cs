using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIB.Interfaces;
using LIB.Models;

namespace LIB.ViewModels
{
    public class CriminalViewModel
    {
        [Required, Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        public string Risk { get; set; }
        [Required, StringLength(255)]
        public string? Image { get; set; }
        [Required]
        public DateTime Since { get; set; }

        public CriminalViewModel(Criminal model) {
            try {
                if (model == null) throw new Exception("El modelo no puede ser nulo");
                
                if(model.Risk == null) throw new Exception("El riesgo del criminal no puede ser nulo");
                if(model.CriminalSince == DateTime.MinValue) throw new Exception("La fecha de inicio del criminal no puede ser nula");

                this.Id = model.CriminalId;
                this.Name = model.Name;
                this.Description = model?.Description;
                this.Risk = model.Risk.Name;
                this.Image = model?.Image;
                this.Since = model.CriminalSince;
            } catch(Exception) {
                throw;
            }
        }
    }
}

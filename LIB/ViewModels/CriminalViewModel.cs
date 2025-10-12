using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIB.Interfaces;
using LIB.Models;

namespace LIB.ViewModels
{
    public class CriminalViewModel: IViewModel<Criminal>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Risk { get; set; }
        public string? Image { get; set; }
        public DateTime CriminalSince { get; set; }


        public void Create(Criminal model)
        {
            this.Id = model.CriminalId;
            this.Name = model.Name;
            this.Description = model.Description;
            this.Risk = model.Risk.Name;
            this.Image = model.Image;
            this.CriminalSince = model.CriminalSince;
        }
    }
}

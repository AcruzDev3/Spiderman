using Azure.Core;
using LIB.DAL;
using LIB.Enums;
using System.ComponentModel.DataAnnotations;

namespace LIB.ViewModels
{
    public class CrimeViewModel
    {
        public int Id { get; set; }
        public AddressViewModel Address { get; set; }

        [EnumDataType(typeof(GradeType), ErrorMessage = "The grade of Crime is not valid")]
        public GradeType Grade { get; set; }

        [EnumDataType(typeof(CrimeType), ErrorMessage = "The type of Crime is not valid")]
        public CrimeType Type { get; set; }
        public string? Description { get; set; }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        
        public bool Status { get; set; }

        
        public static CrimeViewModel CreateViewModel(Crime model, AddressViewModel addressViewModel)
        {
            if(Enum.TryParse<CrimeType>(model.Grade, out CrimeType validType) && Enum.TryParse<GradeType>(model.Grade, out GradeType validGrade))
            {
                return new CrimeViewModel
                {
                    Id = model.Id,
                    Address = addressViewModel,
                    Grade = validGrade,
                    Type = validType,
                    Description = model.Description,
                    Start = model.Start,
                    Status = model.Status,
                    End = model.End
                };
            }
            return null;
        }
    }
}

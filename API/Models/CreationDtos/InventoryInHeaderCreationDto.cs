using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.CreationDtos
{
    public class InventoryInHeaderCreationDto
    {
        [Required(ErrorMessage = "A numeric BranchId field must be included.")]
        public short? BranchId {get; set;} //should automatically create the foreign key
        [Required(ErrorMessage = "A date type DocDate field must be included.")]
        public DateOnly? DocDate { get; set; }
        [Required(ErrorMessage = "A string Reference field must be included.")]
        public string? Reference { get; set; }
        [Required(ErrorMessage = "A string Remarks field must be included.")]
        public string? Remarks { get; set; } = null!;       
    }
}
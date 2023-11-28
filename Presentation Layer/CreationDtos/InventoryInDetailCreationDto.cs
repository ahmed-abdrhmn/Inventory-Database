using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.CreationDtos
{
    public class InventoryInDetailCreationDto
    {
        //We have to make these fields nullable for the validation to work.
        [Required(ErrorMessage = "A numeric InventoryInHeaderId field must be included.")]
        public int? InventoryInHeaderId { get; set; }
        [Required(ErrorMessage = "A numeric Serial field must be included.")]
        public int? Serial { get; set; }
        [Required(ErrorMessage = "A numeric ItemId field must be included.")]
        public int? ItemId { get; set; }
        [Required(ErrorMessage = "A numeric PackageId field must be included.")]
        public int? PackageId { get; set; }
        [Required(ErrorMessage = "A string BatchNumber field must be included.")]
        public string? BatchNumber { get; set; } = null!;
        [Required(ErrorMessage = "A string SerialNumber field must be included.")]
        public string? SerialNumber { get; set; } = null!;
        [Required(ErrorMessage = "A date ExpireDate field must be included.")]
        public DateOnly? ExpireDate { get; set; }
        [Required(ErrorMessage = "A numeric Quantity field must be included.")]
        public decimal? Quantity { get; set; }
        [Required(ErrorMessage = "A numeric ConsumerPrice field must be included.")]
        public decimal? ConsumerPrice { get; set; }        
    }
}
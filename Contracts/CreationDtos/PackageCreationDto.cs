using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.CreationDtos
{
    public class PackageCreationDto
    {
        [Required(ErrorMessage = "The string Name field must be supplied.")]
        public string? Name { get; set; } = null!;               
    }
}
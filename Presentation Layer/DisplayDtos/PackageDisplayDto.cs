using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Presentation.DisplayDtos
{
    public class PackageDisplayDto
    {
        public int PackageId { get; set; }
        public string Name { get; set; } = null!;

        public static PackageDisplayDto FromEntity(Package e){
            return new PackageDisplayDto(){
                PackageId = e.Id,
                Name = e.Name
            };
        }
    }
}
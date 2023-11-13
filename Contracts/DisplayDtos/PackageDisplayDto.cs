using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Contracts.DisplayDtos
{
    public class PackageDisplayDto
    {
        public byte PackageId { get; set; }
        public string Name { get; set; } = null!;

        public static PackageDisplayDto FromEntity(Package e){
            return new PackageDisplayDto(){
                PackageId = e.PackageId,
                Name = e.Name
            };
        }
    }
}
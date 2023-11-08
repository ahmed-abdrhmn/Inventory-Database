using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace API.Models
{
    public class PackageDto
    {
        public byte PackageId { get; set; }
        public string Name { get; set; } = null!;

        public static PackageDto FromEntity(Package e){
            return new PackageDto(){
                PackageId = e.PackageId,
                Name = e.Name
            };
        }
    }
}
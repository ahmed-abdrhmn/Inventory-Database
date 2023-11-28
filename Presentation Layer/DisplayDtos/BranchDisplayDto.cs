using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Presentation.DisplayDtos
{
    public class BranchDisplayDto
    {
        public int BranchId { get; set; }
        public string Name { get; set; } = null!;

        public static BranchDisplayDto FromEntity(Branch e){
            return new BranchDisplayDto(){
                BranchId = e.Id,
                Name = e.Name
            };
        }
    }
}
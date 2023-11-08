using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;

namespace API.Models
{
    public class BranchDto
    {
        public short BranchId { get; set; }
        public string Name { get; set; } = null!;

        public static BranchDto FromEntity(Branch e){
            return new BranchDto(){
                BranchId = e.BranchId,
                Name = e.Name
            };
        }
    }
}
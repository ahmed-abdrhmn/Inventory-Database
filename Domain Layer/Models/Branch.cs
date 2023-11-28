using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Models
{
    public class Branch: BaseEntity
    {
        //public short BranchId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<InventoryInHeader> InventoryInHeaders { get; set; } = new List<InventoryInHeader>(); //Assuming One to many relationship btw branch and header
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Item: BaseEntity
    {
        //public int ItemId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<InventoryInDetail> InventoryInDetails { get; set; } = new List<InventoryInDetail>(); //One to mamy with Details

    }
}
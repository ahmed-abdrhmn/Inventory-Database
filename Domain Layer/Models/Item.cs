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
    }
}
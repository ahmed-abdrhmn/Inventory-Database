using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models
{
    public class InventoryInDetail
    {
        public int InventoryInDetailId { get; set; }
        public InventoryInHeader InventoryInHeader { get; set; } = null!;
        public int Serial { get; set; }
        public Item Item { get; set; } = null!;
        public Package Package { get; set; } = null!;
        [MaxLength(50)]
        public string BatchNumber { get; set; } = null!;
        [MaxLength(50)]
        public string SerialNumber { get; set; } = null!;
        public DateOnly ExpireDate { get; set; }
        [Precision(18,6)]
        public decimal Quantity { get; set; }
        [Precision(18,6)]
        public decimal ConsumerPrice { get; set; }
        //computed fields should not be included in database
        //[Precision(18,6)]
        //public decimal TotalValue { get; set; }

    }
}
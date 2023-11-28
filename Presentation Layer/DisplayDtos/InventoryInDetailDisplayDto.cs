using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Presentation.DisplayDtos
{
    public class InventoryInDetailDisplayDto
    {
        public int InventoryInDetailId { get; set; }
        public int InventoryInHeaderId { get; set; }
        public int Serial { get; set; }
        public int ItemId { get; set; }
        public int PackageId { get; set; }
        public string BatchNumber { get; set; } = null!;
        public string SerialNumber { get; set; } = null!;
        public DateOnly ExpireDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal ConsumerPrice { get; set; }
        public decimal TotalValue {get; set;}

        public static InventoryInDetailDisplayDto FromEntity(InventoryInDetail e){
            return new InventoryInDetailDisplayDto() {
                InventoryInDetailId = e.Id,
                InventoryInHeaderId = e.InventoryInHeaderId,
                Serial = e.Serial,
                ItemId = e.ItemId,
                PackageId = e.PackageId,
                BatchNumber = e.BatchNumber,
                SerialNumber = e.SerialNumber,
                ExpireDate = e.ExpireDate,
                Quantity = e.Quantity,
                ConsumerPrice = e.ConsumerPrice,
                TotalValue = e.TotalValue
            };
        }
    }
}
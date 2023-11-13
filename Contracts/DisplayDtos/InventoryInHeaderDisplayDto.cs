using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

//this file contains the DTO that I will use to eliminate the circular references

namespace Contracts.DisplayDtos
{
    public class InventoryInHeaderDisplayDto
    {
        public int InventoryInHeaderId {get; set;} //TypeNameId is automatically primary key
        public BranchDisplayDto Branch {get; set;} = null!; //should automatically create the foreign key
        public DateOnly DocDate { get; set; }
        public string Reference { get; set; } = null!;
        public decimal TotalValue { get; set; }
        public string Remarks { get; set; } = null!;

        public static InventoryInHeaderDisplayDto FromEntity(InventoryInHeader e){
            return new InventoryInHeaderDisplayDto() {
                InventoryInHeaderId = e.InventoryInHeaderId,
                Branch = BranchDisplayDto.FromEntity(e.Branch),
                DocDate = e.DocDate,
                Reference = e.Reference,
                //TotalValue = 
                TotalValue = e.InventoryInDetails.Sum(x => x.Quantity * x.ConsumerPrice), //Code is duplicaton, but it works
                Remarks = e.Remarks
            };
        }
    }
}
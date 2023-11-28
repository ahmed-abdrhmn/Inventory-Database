using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Repository;

namespace Services
{
    internal static class TotalValueCalculator
    {
        static public void ComputeTotalValue(InventoryInDetail details){
            details.TotalValue = details.ConsumerPrice * details.Quantity;
        }

        static public void ComputeTotalValue(InventoryInHeader header, IRepository<InventoryInDetail> detail_repo){
            decimal totalValue = (from i in detail_repo.GetAll()
                                    where i.InventoryInHeaderId == header.Id
                                    select i.TotalValue).Sum();
            header.TotalValue = totalValue;
        }
    }
}
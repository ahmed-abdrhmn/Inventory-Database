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
        static public void ComputeTotalValue(InventoryInHeader doc){
            doc.TotalValue = 0.0M;
            foreach(var i in doc.InventoryInDetails){
                i.TotalValue = i.Quantity * i.ConsumerPrice;
                doc.TotalValue += i.TotalValue;
            }
        }
    }
}
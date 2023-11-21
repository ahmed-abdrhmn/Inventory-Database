using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;

namespace Services
{
    public interface IInventoryInDetailRepo
    {
        public InventoryInDetailDisplayDto Get(int id);
        public IEnumerable<InventoryInDetailDisplayDto> GetAll();
        public InventoryInDetailDisplayDto Create(InventoryInDetailCreationDto args);
        public void Delete(int id);
        public InventoryInDetailDisplayDto Update(int id, InventoryInDetailCreationDto args);        
    }
}
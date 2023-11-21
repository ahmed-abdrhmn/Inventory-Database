using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;

namespace Services
{
    public interface IInventoryInHeaderRepo
    {
        public InventoryInHeaderDisplayDto Get(int id);
        public IEnumerable<InventoryInHeaderDisplayDto> GetAll();
        public InventoryInHeaderDisplayDto Create(InventoryInHeaderCreationDto args);
        public void Delete(int id);
        public InventoryInHeaderDisplayDto Update(int id, InventoryInHeaderCreationDto args);          
    }
}
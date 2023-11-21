using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DisplayDtos;
using Contracts.CreationDtos;

namespace Services
{
    public interface IBranchRepo
    {
        public BranchDisplayDto Get(int id);
        public IEnumerable<BranchDisplayDto> GetAll();
        public BranchDisplayDto Create(BranchCreationDto args);
        public void Delete(int id);
        public BranchDisplayDto Update(int id, BranchCreationDto args);
    }
}
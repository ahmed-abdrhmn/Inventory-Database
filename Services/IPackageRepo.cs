using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;

namespace Services
{
    public interface IPackageRepo
    {
        public PackageDisplayDto Get(int id);
        public IEnumerable<PackageDisplayDto> GetAll();
        public PackageDisplayDto Create(PackageCreationDto args);
        public void Delete(int id);
        public PackageDisplayDto Update(int id, PackageCreationDto args);           
    }
}
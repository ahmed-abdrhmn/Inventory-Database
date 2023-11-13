using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;
using Contracts.Exceptions;
using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Impl
{
    public class PackageRepo: IPackageRepo
    {
        private readonly InventoryDbContext _inventoryDbContext;
        
        public PackageRepo(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        public IEnumerable<PackageDisplayDto> GetAll(){
            var Packages = from i in _inventoryDbContext.Packages select PackageDisplayDto.FromEntity(i);
            return Packages;            
        }
        public PackageDisplayDto Get(int id){
            var Package = (from i in _inventoryDbContext.Packages where i.PackageId == id select PackageDisplayDto.FromEntity(i))
                            .SingleOrDefault();

            if (Package is not null){
                return Package;
            }else{
                throw new IDNotFoundException("Package");
            }            
        }
        public void Delete(int id){
            int result = _inventoryDbContext.Packages
                            .Where(d => d.PackageId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return; //success
            }else{
                throw new IDNotFoundException("Package");
            }            
        }
        public PackageDisplayDto Create(PackageCreationDto args){
            var toAdd = new Package(){
                Name = args.Name!
            };

            _inventoryDbContext.Packages.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return PackageDisplayDto.FromEntity(toAdd); //this will include the id of the newly created Package            
        }
        public PackageDisplayDto Update(int id, PackageCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Packages where i.PackageId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                throw new IDNotFoundException("Package");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return PackageDisplayDto.FromEntity(toUpdate); //this will include the id of the newly created Package
            }            
        }
    }
}
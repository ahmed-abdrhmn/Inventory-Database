using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;
using Contracts.Exceptions;
using Domain.Data;
using Domain.Models;

namespace Services.Impl
{
    public class BranchRepo: IBranchRepo
    {
        private InventoryDbContext _inventoryDbContext;

        public BranchRepo(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        public IEnumerable<BranchDisplayDto> GetAll(){
            var Branchs = from i in _inventoryDbContext.Branches select BranchDisplayDto.FromEntity(i);
            return Branchs;            
        }
        public BranchDisplayDto Get(int id){
            var Branch = (from i in _inventoryDbContext.Branches where i.BranchId == id select BranchDisplayDto.FromEntity(i))
                            .SingleOrDefault();

            if (Branch is not null){
                return Branch;
            }else{
                throw new IDNotFoundException("Branch");
            }            
        }
        public void Delete(int id){
            int result = _inventoryDbContext.Branches
                            .Where(d => d.BranchId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return; //success
            }else{
                throw new IDNotFoundException("Branch");
            }            
        }
        public BranchDisplayDto Create(BranchCreationDto args){
            var toAdd = new Branch(){
                Name = args.Name!
            };

            _inventoryDbContext.Branches.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return BranchDisplayDto.FromEntity(toAdd); //this will include the id of the newly created Branch            
        }
        public BranchDisplayDto Update(int id, BranchCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Branches where i.BranchId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                throw new IDNotFoundException("Branch");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return BranchDisplayDto.FromEntity(toUpdate); //this will include the id of the newly created Branch
            }            
        }        
    }
}
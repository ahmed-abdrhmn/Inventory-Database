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
using Services;

namespace Services.Impl
{
    public class InventoryInHeaderRepo: IInventoryInHeaderRepo
    {
        private readonly InventoryDbContext _inventoryDbContext;
        
        public InventoryInHeaderRepo(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        public IEnumerable<InventoryInHeaderDisplayDto> GetAll(){
            var InventoryInHeaders = from i in _inventoryDbContext.InventoryInHeaders
                    .Include(h => h.Branch) 
                    .Include(h => h.InventoryInDetails) //<< -- TotalValue Column
                select InventoryInHeaderDisplayDto.FromEntity(i);
            
            return InventoryInHeaders!;            
        }

        public InventoryInHeaderDisplayDto Get(int id){
            var InventoryInHeader = (from i in _inventoryDbContext.InventoryInHeaders
                    .Include(h => h.Branch) 
                    .Include(h => h.InventoryInDetails) //<< -- TotalValue Column
                where i.InventoryInHeaderId == id select InventoryInHeaderDisplayDto.FromEntity(i))
                .SingleOrDefault();

            if (InventoryInHeader is not null){
                return InventoryInHeader!;
            }else{
                throw new IDNotFoundException("InventoryInHeader");
            }            
        }

        public InventoryInHeaderDisplayDto Create(InventoryInHeaderCreationDto args){
            Branch? relatedBranch = (from i in _inventoryDbContext.Branches
                where i.BranchId == args.BranchId
                select i).SingleOrDefault();
            
            if (relatedBranch is null){
                throw new IDNotFoundException("Branch");
            }
            
            var toAdd = new InventoryInHeader(){
                Branch = relatedBranch,
                DocDate = args.DocDate!.Value,
                Reference = args.Reference!,
                Remarks = args.Remarks!
            };

            _inventoryDbContext.InventoryInHeaders.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return InventoryInHeaderDisplayDto.FromEntity(toAdd); //this will include the id of the newly created InventoryInHeader            
        }
        
        public void Delete(int id){
            int result = _inventoryDbContext.InventoryInHeaders
                            .Where(d => d.InventoryInHeaderId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return; //success
            }else{
                throw new IDNotFoundException("InventoryInHeader");
            }
        }

        public InventoryInHeaderDisplayDto Update(int id, InventoryInHeaderCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.InventoryInHeaders 
                    .Include(h => h.InventoryInDetails) // Dont forget about TotalValue
                where i.InventoryInHeaderId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                throw new IDNotFoundException("InventoryInHeader");
            }else{
                Branch? relatedBranch = (from i in _inventoryDbContext.Branches
                    where i.BranchId == args.BranchId
                    select i).SingleOrDefault();
                
                if (relatedBranch is null){
                    throw new IDNotFoundException("Branch");
                }

                toUpdate.Branch = relatedBranch;
                toUpdate.DocDate = args.DocDate!.Value;
                toUpdate.Reference = args.Reference!;
                toUpdate.Remarks = args.Remarks!;

                _inventoryDbContext.SaveChanges();
                return InventoryInHeaderDisplayDto.FromEntity(toUpdate); //this will include the id of the newly created InventoryInHeader    
            } 
        }       
    }
}
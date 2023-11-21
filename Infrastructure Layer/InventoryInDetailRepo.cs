using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Contracts.CreationDtos;
using Contracts.DisplayDtos;
using Contracts.Exceptions;
using Domain.Models;
using Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Impl
{
    public class InventoryInDetailRepo: IInventoryInDetailRepo
    {
        private readonly InventoryDbContext _inventoryDbContext;
        
        public InventoryInDetailRepo(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        public IEnumerable<InventoryInDetailDisplayDto> GetAll(){
            //Here I convert everything to a Dto in order to eliminate cyclic reference errors
            //must use all the includes since the FromEntity Functions expect fully constructed entities
            var documents = from i in _inventoryDbContext.InventoryInDetails
                .Include(d => d.InventoryInHeader)
                    .ThenInclude(h => h.Branch)
                .Include(d => d.InventoryInHeader)  // <<-- These two lines are necessary to make sure TotalValue is updated properly
                    .ThenInclude(h => h.InventoryInDetails)  
                .Include(d => d.Item)
                .Include(d => d.Package)
            select InventoryInDetailDisplayDto.FromEntity(i);

            return documents!;
        }

        public InventoryInDetailDisplayDto Get(int id){           //Here I convert everything to a Dto in order to eliminate cyclic reference errors
            //must use all the includes since the FromEntity Functions expect fully constructed entities
            var document = (from i in _inventoryDbContext.InventoryInDetails
                .Include(d => d.InventoryInHeader)
                    .ThenInclude(h => h.Branch)
                .Include(d => d.InventoryInHeader)  // <<-- These two lines are necessary to make sure TotalValue is updated properly
                    .ThenInclude(h => h.InventoryInDetails)  
                .Include(d => d.Item)
                .Include(d => d.Package)
            where i.InventoryInDetailId == id
            select InventoryInDetailDisplayDto.FromEntity(i)).SingleOrDefault();

            if (document is not null){
                return document!;
            }else{
                throw new IDNotFoundException("InventoryInDetail");
            }
        }

        public InventoryInDetailDisplayDto Create(InventoryInDetailCreationDto args){
            InventoryInHeader? relatedHeader = (from i in _inventoryDbContext.InventoryInHeaders
                    .Include("Branch") // <-- Needed for full response
                    .Include("InventoryInDetails") //<<--To calculate TotalValue Properly
                where i.InventoryInHeaderId == args.InventoryInHeaderId
                select i).SingleOrDefault();
            
            //check header
            if (relatedHeader is null){
                throw new IDNotFoundException("InventoryInHeader");
            }

            Item? relatedItem = (from i in _inventoryDbContext.Items
                where i.ItemId == args.ItemId
                select i).SingleOrDefault();
            
            //check item
            if (relatedItem is null){
                throw new IDNotFoundException("Item");
            }

            Package? relatedPackage = (from i in _inventoryDbContext.Packages
                where i.PackageId == args.PackageId
                select i).SingleOrDefault();
            
            //check package
            if (relatedPackage is null){
                throw new IDNotFoundException("Package");
            } 

            //add to the database. The only reason I made the fields nullable is so they can be validated.
            //but here none of them should be null
            InventoryInDetail toAdd = new InventoryInDetail(){
                InventoryInHeader = relatedHeader,
                Serial = args.Serial!.Value,
                Item = relatedItem,
                Package = relatedPackage,
                BatchNumber = args.BatchNumber!,
                SerialNumber = args.SerialNumber!,
                ExpireDate = args.ExpireDate!.Value,
                Quantity = args.Quantity!.Value!,
                ConsumerPrice = args.ConsumerPrice!.Value!
            };

            _inventoryDbContext.Add<InventoryInDetail>(toAdd);
            _inventoryDbContext.SaveChanges();
            return InventoryInDetailDisplayDto.FromEntity(toAdd); //Return Representation of added object
        }

        public void Delete(int id){
            int result = _inventoryDbContext.InventoryInDetails
                            .Where(d => d.InventoryInDetailId == id)
                            .ExecuteDelete(); // <<-- Brand new feature in EF core 7
            
            if(result > 0){
                return; //success
            }else{
                throw new IDNotFoundException("InventoryInDetail");
            }
        }

        public InventoryInDetailDisplayDto Update(int id, InventoryInDetailCreationDto args){
            InventoryInDetail? toUpdate = (from i in _inventoryDbContext.InventoryInDetails
                where i.InventoryInDetailId == id select i).SingleOrDefault();
            
            if (toUpdate is null){
                throw new IDNotFoundException("InventoryInDetail");
            }
            
            InventoryInHeader? relatedHeader = (from i in _inventoryDbContext.InventoryInHeaders
                    .Include("Branch") //<<--Needed for full response
                    .Include("InventoryInDetails") //<<--To calculate TotalValue Properly
                where i.InventoryInHeaderId == args.InventoryInHeaderId
                select i).SingleOrDefault();
            
            //check header
            if (relatedHeader is null){
                throw new IDNotFoundException("InventoryInHeader");
            }else{
                toUpdate.InventoryInHeader = relatedHeader;
            }

            Item? relatedItem = (from i in _inventoryDbContext.Items
                where i.ItemId == args.ItemId
                select i).SingleOrDefault();
            
            //check item
            if (relatedItem is null){
                throw new IDNotFoundException("Item");
            }else{
                toUpdate.Item = relatedItem;
            }


            Package? relatedPackage = (from i in _inventoryDbContext.Packages
                where i.PackageId == args.PackageId
                select i).SingleOrDefault();
            
            //check package
            if (relatedPackage is null){
                throw new IDNotFoundException("Package");
            }else{
                toUpdate.Package = relatedPackage;
            } 

            toUpdate.Serial = args.Serial!.Value;
            toUpdate.BatchNumber = args.BatchNumber!;
            toUpdate.SerialNumber = args.SerialNumber!;
            toUpdate.ExpireDate = args.ExpireDate!.Value;
            toUpdate.Quantity = args.Quantity!.Value;
            toUpdate.ConsumerPrice = args.ConsumerPrice!.Value;

            _inventoryDbContext.SaveChanges(); //save changes should automatically apply the toUpdate
            return InventoryInDetailDisplayDto.FromEntity(toUpdate); //Return New details of the Updated object
        }      
    }
}
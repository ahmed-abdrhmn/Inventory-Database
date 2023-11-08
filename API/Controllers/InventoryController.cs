using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using API.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {   
        private readonly InventoryDbContext _inventoryDbContext; //depends injection

        public InventoryController(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        //read
        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryDocuments(){
            //Here I convert everything to a Dto in order to eliminate cyclic reference errors
            //must use all the includes since the FromEntity Functions expect fully constructed entities
            var documents = from i in _inventoryDbContext.InventoryInDetails
                .Include(d => d.InventoryInHeader)
                    .ThenInclude(h => h.Branch)
                .Include(d => d.InventoryInHeader)  // <<-- These two lines are necessary to make sure TotalValue is updated properly
                    .ThenInclude(h => h.InventoryInDetails)  
                .Include(d => d.Item)
                .Include(d => d.Package)
            select InventoryInDetailDto.FromEntity(i);
            
            return Ok(documents);
        }

        //read
        [HttpGet]
        [Route("ById/{id:int}")]
        public IActionResult GetInventoryDocumentById(int id){
            //Here I convert everything to a Dto in order to eliminate cyclic reference errors
            //must use all the includes since the FromEntity Functions expect fully constructed entities
            var document = (from i in _inventoryDbContext.InventoryInDetails
                .Include(d => d.InventoryInHeader)
                    .ThenInclude(h => h.Branch)
                .Include(d => d.InventoryInHeader)  // <<-- These two lines are necessary to make sure TotalValue is updated properly
                    .ThenInclude(h => h.InventoryInDetails)  
                .Include(d => d.Item)
                .Include(d => d.Package)
            where i.InventoryInDetailId == id
            select InventoryInDetailDto.FromEntity(i)).SingleOrDefault();

            if (document is not null){
                return Ok(document);
            }else{
                return NotFound("No document with supplied Id was found");
            }
        }

        //Delete
        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeleteInventoryDocument(int id){
            int result = _inventoryDbContext.InventoryInDetails
                            .Where(d => d.InventoryInDetailId == id)
                            .ExecuteDelete(); // <<-- Brand new feature in EF core 7
            System.Console.WriteLine($"Deleted Records {result}");
            
            if(result > 0){
                return Ok($"Document number {id} successfully deleted");
            }else{
                return NotFound("No document with supplied Id was found");
            }
        }

        [HttpPost]
        [Route("New")]
        public IActionResult CreateNewInventoryIn(InventoryInDetailCreationDto args){      
            InventoryInHeader? relatedHeader = (from i in _inventoryDbContext.InventoryInHeaders.Include("Branch") // <-- Needed for full response
                where i.InventoryInHeaderId == args.InventoryInHeaderId
                select i).SingleOrDefault();
            
            //check header
            if (relatedHeader is null){
                return NotFound("No InventoryInHeader with the Id you gave exists");
            }

            Item? relatedItem = (from i in _inventoryDbContext.Items
                where i.ItemId == args.ItemId
                select i).SingleOrDefault();
            
            //check item
            if (relatedItem is null){
                return NotFound("No Item with the Id you gave exists");
            }

            Package? relatedPackage = (from i in _inventoryDbContext.Packages
                where i.PackageId == args.PackageId
                select i).SingleOrDefault();
            
            //check package
            if (relatedPackage is null){
                return NotFound("No package with the Id you gave exists");
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
            return Ok(InventoryInDetailDto.FromEntity(toAdd)); //Return Representation of added object
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        //An REST put method should include ALL the fields of the resource
        public IActionResult UpdateInventoryDocument(int id, InventoryInDetailCreationDto args){
            InventoryInDetail? toUpdate = (from i in _inventoryDbContext.InventoryInDetails
                where i.InventoryInDetailId == id select i).SingleOrDefault();
            
            if (toUpdate is null){
                return NotFound("No IdentityInDetail with the Id you supplied was found");
            }
            
            InventoryInHeader? relatedHeader = (from i in _inventoryDbContext.InventoryInHeaders.Include("Branch") //<<--Needed for full response
                where i.InventoryInHeaderId == args.InventoryInHeaderId
                select i).SingleOrDefault();
            
            //check header
            if (relatedHeader is null){
                return NotFound("No InventoryInHeader with the Id you gave exists");
            }else{
                toUpdate.InventoryInHeader = relatedHeader;
            }

            Item? relatedItem = (from i in _inventoryDbContext.Items
                where i.ItemId == args.ItemId
                select i).SingleOrDefault();
            
            //check item
            if (relatedItem is null){
                return NotFound("No Item with the Id you gave exists");
            }else{
                toUpdate.Item = relatedItem;
            }


            Package? relatedPackage = (from i in _inventoryDbContext.Packages
                where i.PackageId == args.PackageId
                select i).SingleOrDefault();
            
            //check package
            if (relatedPackage is null){
                return NotFound("No package with the Id you gave exists");
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
            return Ok(InventoryInDetailDto.FromEntity(toUpdate)); //Return New details of the Updated object           
        }
     }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models.CreationDtos;
using DataAccess.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeaderController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDbContext; //depends injection

        public HeaderController(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllInventoryInHeaders(){
            var InventoryInHeaders = from i in _inventoryDbContext.InventoryInHeaders
                    .Include(h => h.Branch) 
                    .Include(h => h.InventoryInDetails) //<< -- TotalValue Column
                select InventoryInHeaderDto.FromEntity(i);
            
            return Ok(InventoryInHeaders);
        }

        [HttpGet]
        [Route("ById/{id:int}")]
        public IActionResult GetInventoryInHeaderById(int id){
            var InventoryInHeader = (from i in _inventoryDbContext.InventoryInHeaders
                    .Include(h => h.Branch) 
                    .Include(h => h.InventoryInDetails) //<< -- TotalValue Column
                where i.InventoryInHeaderId == id select InventoryInHeaderDto.FromEntity(i))
                .SingleOrDefault();

            if (InventoryInHeader is not null){
                return Ok(InventoryInHeader);
            }else{
                return NotFound("Not InventoryInHeader with supplied Id exisits");
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeleteInventoryInHeader(int id){
            int result = _inventoryDbContext.InventoryInHeaders
                            .Where(d => d.InventoryInHeaderId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return Ok($"InventoryInHeader number {id} successfully deleted");
            }else{
                return NotFound("No InventoryInHeader with supplied Id was found");
            }
        }

        [HttpPost]
        [Route("New")]
        public IActionResult NewInventoryInHeader(InventoryInHeaderCreationDto args){
            Branch? relatedBranch = (from i in _inventoryDbContext.Branches
                where i.BranchId == args.BranchId
                select i).SingleOrDefault();
            
            if (relatedBranch is null){
                return NotFound("Not Branch with supplied Id exisits");
            }
            
            var toAdd = new InventoryInHeader(){
                Branch = relatedBranch,
                DocDate = args.DocDate!.Value,
                Reference = args.Reference!,
                Remarks = args.Remarks!
            };

            _inventoryDbContext.InventoryInHeaders.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return Ok(InventoryInHeaderDto.FromEntity(toAdd)); //this will include the id of the newly created InventoryInHeader
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdateInventoryInHeader(int id, InventoryInHeaderCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.InventoryInHeaders 
                    .Include(h => h.InventoryInDetails) // Dont forget about TotalValue
                where i.InventoryInHeaderId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                return NotFound("No InventoryInHeader with supplied Id was found");
            }else{
                Branch? relatedBranch = (from i in _inventoryDbContext.Branches
                    where i.BranchId == args.BranchId
                    select i).SingleOrDefault();
                
                if (relatedBranch is null){
                    return NotFound("Not Branch with supplied Id exisits");
                }

                toUpdate.Branch = relatedBranch;
                toUpdate.DocDate = args.DocDate!.Value;
                toUpdate.Reference = args.Reference!;
                toUpdate.Remarks = args.Remarks!;

                _inventoryDbContext.SaveChanges();
                return Ok(InventoryInHeaderDto.FromEntity(toUpdate)); //this will include the id of the newly created InventoryInHeader
            }
        }        
    }
}
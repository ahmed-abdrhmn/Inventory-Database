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
    public class BranchController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDbContext; //depends injection

        public BranchController(InventoryDbContext inventoryDbContext){
            this._inventoryDbContext = inventoryDbContext;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllBranches(){
            var branches = from i in _inventoryDbContext.Branches select BranchDto.FromEntity(i);
            return Ok(branches);
        }

        [HttpGet]
        [Route("ById/{id:int}")]
        public IActionResult GetBranchById(int id){
            var branch = (from i in _inventoryDbContext.Branches where i.BranchId == id select BranchDto.FromEntity(i))
                            .SingleOrDefault();

            if (branch is not null){
                return Ok(branch);
            }else{
                return NotFound("Not Branch with supplied Id exisits");
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public IActionResult DeleteBranch(int id){
            int result = _inventoryDbContext.Branches
                            .Where(d => d.BranchId == id)
                            .ExecuteDelete();
            
            if(result > 0){
                return Ok($"Branch number {id} successfully deleted");
            }else{
                return NotFound("No branch with supplied Id was found");
            }
        }

        [HttpPost]
        [Route("New")]
        public IActionResult NewBranch(BranchCreationDto args){
            var toAdd = new Branch(){
                Name = args.Name!
            };

            _inventoryDbContext.Branches.Add(toAdd);
            _inventoryDbContext.SaveChanges();
            return Ok(BranchDto.FromEntity(toAdd)); //this will include the id of the newly created branch
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public IActionResult UpdateBranch(int id, BranchCreationDto args){
            var toUpdate = (from i in _inventoryDbContext.Branches where i.BranchId == id select i)
                .SingleOrDefault();

            if (toUpdate is null){
                return NotFound("No branch with supplied Id was found");
            }else{
                toUpdate.Name = args.Name!;
                _inventoryDbContext.SaveChanges();
                return Ok(BranchDto.FromEntity(toUpdate)); //this will include the id of the newly created branch
            }
        }        
    }
}
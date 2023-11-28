using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.CreationDtos;
using Presentation.DisplayDtos;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IService<Branch> _branchService;

        public BranchController(
        IService<Branch> branchService){
            this._branchService = branchService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllBranches(){
            var results = from i in _branchService.GetAll() select BranchDisplayDto.FromEntity(i);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetBranchById(int id){
            var result = _branchService.Get(id);
            return Ok(BranchDisplayDto.FromEntity(result));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteBranch(int id){
            _branchService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewBranch(BranchCreationDto args){
            Branch entity = new() {
                Name = args.Name!
            };

            var result = _branchService.Create(entity);
            return Ok(BranchDisplayDto.FromEntity(result));
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBranch(int id, BranchCreationDto args){
            Branch entity = new() {
                Id = id,
                Name = args.Name!
            };

            var result = _branchService.Update(entity);
            return Ok(BranchDisplayDto.FromEntity(result));
        }        
    }
}
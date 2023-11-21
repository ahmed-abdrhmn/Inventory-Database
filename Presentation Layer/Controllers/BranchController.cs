using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.CreationDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController : ControllerBase
    {
        //Interfaces with service layer are injected at runtime
        private readonly IBranchRepo _iBranchRepo;

        public BranchController(
            IBranchRepo iBranchRepo){
                _iBranchRepo = iBranchRepo;
            }

        [HttpGet]
        [Route("")]
        public IActionResult GetAllBranches(){
            var result = _iBranchRepo.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetBranchById(int id){
            var result = _iBranchRepo.Get(id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteBranch(int id){
            _iBranchRepo.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("")]
        public IActionResult NewBranch(BranchCreationDto args){
            var result = _iBranchRepo.Create(args);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateBranch(int id, BranchCreationDto args){
            var result = _iBranchRepo.Update(id,args);
            return Ok(result);
        }        
    }
}
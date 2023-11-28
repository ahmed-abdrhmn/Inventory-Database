using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Domain.Models;
using Repository;

namespace Services
{
    public class BranchService: IService<Branch>
    {
        private readonly IRepository<Branch> _branch_repo;

        public BranchService(
            IRepository<Branch> branch
        ){
            this._branch_repo = branch;
        }

        public IEnumerable<Branch> GetAll(){
            return _branch_repo.GetAll();;
        }

        public Branch Get(int id){
            return _branch_repo.GetById(id);
        }

        public Branch Create(Branch branch){
            //I will assume args has all it fields set
            return _branch_repo.Create(branch);
        }

        public void Delete(int id){
            _branch_repo.Delete(id);
        }

        public Branch Update(Branch branch){
            return _branch_repo.Update(branch);
        }      
    }
}
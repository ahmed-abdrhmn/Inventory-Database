using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Domain.Models;
using Repository;

namespace Services
{
    public class PackageService: IService<Package>
    {
        private readonly IRepository<Package> _package_repo;

        public PackageService(
            IRepository<Package> package
        ){
            this._package_repo = package;
        }

        public IEnumerable<Package> GetAll(){
            return _package_repo.GetAll();;
        }

        public Package Get(int id){
            return _package_repo.GetById(id);
        }

        public Package Create(Package package){
            //I will assume args has all it fields set
            return _package_repo.Create(package);
        }

        public void Delete(int id){
            _package_repo.Delete(id);
        }

        public Package Update(Package package){
            return _package_repo.Update(package);
        }      
    }
}
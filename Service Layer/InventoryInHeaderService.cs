using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Domain.Models;
using Repository;

namespace Services
{
    public class InventoryInHeaderService: IService<InventoryInHeader>
    {
        private readonly IRepository<InventoryInHeader> _header_repo;
        private readonly IRepository<InventoryInDetail> _detail_repo;

        public InventoryInHeaderService(
            IRepository<InventoryInHeader> header_repo,
            IRepository<InventoryInDetail> detail_repo
        ){
            this._header_repo = header_repo;
            this._detail_repo = detail_repo;
        }

        public IEnumerable<InventoryInHeader> GetAll(){
            return _header_repo.GetAll();;
        }

        public InventoryInHeader Get(int id){
            return _header_repo.GetById(id);
        }

        public InventoryInHeader Create(InventoryInHeader header){
            //I will assume args has all it fields set
            TotalValueCalculator.ComputeTotalValue(header,_detail_repo); //TotalValue should be zero but I am not 100% sure
            return _header_repo.Create(header);
        }

        public void Delete(int id){
            _header_repo.Delete(id);
        }

        public InventoryInHeader Update(InventoryInHeader header){
            //No need to update the totalValue if header itself changes
            //TotalValueCalculator.ComputeTotalValue(header,_detail_repo);
            
            return _header_repo.Update(header);
        }      
    }
}
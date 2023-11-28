using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using Domain.Models;
using Repository;

namespace Services
{
    public class InventoryInDetailService: IService<InventoryInDetail>
    {
        private readonly IRepository<InventoryInDetail> _detail_repo;
        private readonly IRepository<InventoryInHeader> _header_repo;

        public InventoryInDetailService(
            IRepository<InventoryInDetail> detail_repo,
            IRepository<InventoryInHeader> header_repo
        ){
            this._detail_repo = detail_repo;
            this._header_repo = header_repo;
        }

        public IEnumerable<InventoryInDetail> GetAll(){
            return _detail_repo.GetAll();;
        }

        public InventoryInDetail Get(int id){
            return _detail_repo.GetById(id);
        }

        public InventoryInDetail Create(InventoryInDetail detail){
            //I will assume args has all it fields set
            TotalValueCalculator.ComputeTotalValue(detail);
            var result = _detail_repo.Create(detail);

            //we must update the total value field in the related header after creating an inventory in detail
            var header = _header_repo.GetById(detail.InventoryInHeaderId);
            TotalValueCalculator.ComputeTotalValue(header,_detail_repo);
            _header_repo.Update(header);

            return result;
        }

        public void Delete(int id){
            var detail = _detail_repo.GetById(id);
            _detail_repo.Delete(id);

            //we must update the total value field in the related header after deleting an inventory in detail
            var header = _header_repo.GetById(detail.InventoryInHeaderId);
            TotalValueCalculator.ComputeTotalValue(header,_detail_repo);
            _header_repo.Update(header);
        }

        public InventoryInDetail Update(InventoryInDetail detail){
            //Computes total Value
            TotalValueCalculator.ComputeTotalValue(detail);
            var result = _detail_repo.Update(detail);

            //we must update the total value field in the related header after updating an inventory in detail
            var header = _header_repo.GetById(detail.InventoryInHeaderId);
            TotalValueCalculator.ComputeTotalValue(header,_detail_repo);
            _header_repo.Update(header);
            
            return result;
        }      
    }
}
using Domain.Models;
using Repository;

namespace Services;

public class DocumentService: IDocumentService
{
    private IRepository<InventoryInHeader> _repo;
    
    public DocumentService(IRepository<InventoryInHeader> repo){
        this._repo = repo;
    }

    public IEnumerable<InventoryInHeader> GetAll(){
        return _repo.GetAll();
    }

    public InventoryInHeader GetById(int id){
        return _repo.GetById(id);
    }

    public void Delete(int id){
        _repo.Delete(id);
    }

    public InventoryInHeader Create(InventoryInHeader arg){
        TotalValueCalculator.ComputeTotalValue(arg);
        return _repo.Create(arg);
    }

    public InventoryInHeader Update (InventoryInHeader arg){
        TotalValueCalculator.ComputeTotalValue(arg);
        return _repo.Update(arg);
    }
}

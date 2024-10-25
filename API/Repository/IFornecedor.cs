using API.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Repository;

public interface IFornecedor
{
    public Task<Fornecedor[]> GetAllAsync();
    public ValueTask<Fornecedor> FindAsync(int id);
    public ValueTask<EntityEntry<Fornecedor>> AddAsync(Fornecedor fornecedor);
    public void Update(int id, Fornecedor fornecedor);
    public Task<EntityEntry<Fornecedor>?> RemoveAsync(int id);
    Task<int> Save();
}
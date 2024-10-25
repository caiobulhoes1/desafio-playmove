using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace API.Repository;

public class FornecedorRepository : IFornecedor
{
    private readonly FornecedorContext _context;
    public FornecedorRepository(FornecedorContext context)
    {
        _context = context;
    }
    public ValueTask<EntityEntry<Fornecedor>> AddAsync(Fornecedor fornecedor)
    {
        return _context.Set<Fornecedor>().AddAsync(fornecedor);
    }

    public ValueTask<Fornecedor?> FindAsync(int id)
    {
        return _context.Set<Fornecedor>().FindAsync(id);
    }

    public async Task<Fornecedor[]> GetAllAsync()
    {
        return await _context.Set<Fornecedor>().ToArrayAsync();
    }

    public async ValueTask<Fornecedor?> GetByCnpjAsync(string cnpj)
    {
        return await _context.FornecedorDB.FirstOrDefaultAsync(f => f.Cnpj == cnpj);
    }

    public async Task<EntityEntry<Fornecedor>?> RemoveAsync(int id)
    {
        Fornecedor? entity = await _context.Set<Fornecedor>().FindAsync(id);
        return entity == null ? null : _context.Set<Fornecedor>().Remove(entity);
    }

    public async Task<int> Save()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(int id, Fornecedor fornecedor)
    {
        Fornecedor? entity = _context.Set<Fornecedor>().Find(id);

        if (entity != null)
        {
            _context.Entry(entity).CurrentValues.SetValues(fornecedor);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using VirtualShop.Application.Common.Uow;
using VirtualShop.Infrastructure.Data;

namespace VirtualShop.Infrastructure.Uow;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _applicationDbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task Begin()
    {
        if (_transaction != null)
        {
            throw new InvalidOperationException("A transaction is already in progress.");
        }

        _transaction = await _applicationDbContext.Database.BeginTransactionAsync();
    } 

    public async Task Commit()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        try
        {
            await _applicationDbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await RollBack();
            throw;
        }
        finally
        {
            await DisposeTransaction();
        }
    }

    public async Task RollBack()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction in progress.");
        }

        await _transaction.RollbackAsync();
        await DisposeTransaction();
    }

    private async Task DisposeTransaction()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}


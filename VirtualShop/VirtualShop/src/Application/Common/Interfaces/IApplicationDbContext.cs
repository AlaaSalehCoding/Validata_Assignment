﻿ 

namespace VirtualShop.Application.Common.Interfaces;
public interface IApplicationDbContext
{ 

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

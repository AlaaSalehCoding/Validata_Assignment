﻿namespace VirtualShop.Domain.Common;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
}

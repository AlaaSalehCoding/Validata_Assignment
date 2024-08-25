using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualShop.Application.Common.Uow;
public interface IUnitOfWork
{
    Task Begin();
    Task Commit();
    Task RollBack();
}

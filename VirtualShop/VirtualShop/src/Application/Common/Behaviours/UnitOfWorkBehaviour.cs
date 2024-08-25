using VirtualShop.Application.Common.Uow;

namespace VirtualShop.Application.Common.Behaviours;

public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehaviour(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).Name.EndsWith("Command", StringComparison.OrdinalIgnoreCase) == false)
        {
            return await next();
        }
        try
        {
            await _unitOfWork.Begin();
            var result = await next();
            await _unitOfWork.Commit();
            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollBack();
            throw;
        }
    }
}

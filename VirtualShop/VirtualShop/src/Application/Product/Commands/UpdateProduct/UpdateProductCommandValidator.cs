namespace VirtualShop.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(1)
            .MaximumLength(250)
            .NotEmpty();
        RuleFor(v => v.Price)
            .GreaterThan(0)
            .NotEmpty();
    }
}

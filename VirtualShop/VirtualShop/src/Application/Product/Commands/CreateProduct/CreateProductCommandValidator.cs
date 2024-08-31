﻿namespace VirtualShop.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
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

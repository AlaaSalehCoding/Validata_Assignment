namespace VirtualShop.Application.Items.Commands.AddItemToOrder;

public class AddItemToOrderCommandValidator : AbstractValidator<AddItemToOrderCommand>
{
    public AddItemToOrderCommandValidator()
    {
        RuleFor(i => i.Quantity).GreaterThanOrEqualTo(1);
    }
}

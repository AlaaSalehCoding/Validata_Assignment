namespace VirtualShop.Application.Common.Filtration;

public class SearchFilter
{
    public string FieldName { get; set; } = null!;
    public object FieldValue { get; set; } = null!;
    public Operator Operator { get; set; } = Operator.Equals;
    public LogicOperator LogicOperator { get; set; } = LogicOperator.And;
    public List<SearchFilter>? SubFilters { get; set; }
}
public enum LogicOperator { And = 0, Or = 1 }
public enum Operator { Equals = 0, Contains = 1, GreaterThan = 2, LessThan = 3, GreaterThanOrEqual = 4, LessThanOrEqual = 5 }

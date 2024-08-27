namespace VirtualShop.Application.Common.Filtration;

public class SortFilter
{
    public string FieldName { get; set; } = null!;
    public SortDirection SortDirection { get; set; } = SortDirection.asc;
    public List<SortFilter>? SubSort { get; set; }
}
public enum SortDirection { asc = 0, desc = 1 }



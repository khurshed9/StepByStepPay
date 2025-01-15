namespace Application.Filter;

public record PurchaseFilter(
    int? ProductId,
    int? InstallmentMonths,
    int? PhoneNumber) : BaseFilter;
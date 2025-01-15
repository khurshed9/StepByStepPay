namespace Domain.Entities;

public class Purchase : BaseEntity
{
    public int ProductId { get; set; } 

    public int InstallmentMonths { get; set; }
    
    public int PhoneNumber { get; set; }
    
}
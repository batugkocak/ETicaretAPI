using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Domain.Entities;

public class Order: BaseEntity
{
    public Guid CustomerId {get; set;} //EF zaten kendisi koyacak. 
    
    public string Description { get; set; }

    //Adres ValueObject olabilirdi (Street/City ... içeren başka bir "değer" sınıfı) ama şimdilik o kadar detaya gerek yok.
    public string Address { get; set; }
    
    public ICollection<Product> Products { get; set; } 

    public Customer Customer { get; set; }
}
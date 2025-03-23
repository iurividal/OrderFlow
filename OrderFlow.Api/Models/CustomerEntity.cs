namespace OrderFlow.Api.Models;

public class CustomerEntity
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public  List<AddressEntity> Address { get; set; }
}
namespace OrderFlow.Shared;

public class CustomerModel
{
    // Properties Name, Email,Phone,DocumentId,Adress,Number,Complement,Neighborhood,City,State,ZipCode
    public string Id { get; set; }
    
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string DocumentId { get; set; }

    public DateTime CreatedAt { get; set; }
    public List<AddressModel> Address { get; set; } = new();

    public CustomerModel()
    {
    }

    public CustomerModel(string name)
    {
        Name = name;
    }

    public CustomerModel(string id, string name, string documentId)
    {
        Id = id;
        Name = name;
        DocumentId = documentId;
    }
}
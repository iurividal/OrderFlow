namespace OrderFlow.Shared;

public class CustomerModel
{
    // Properties Name, Email,Phone,DocumentId,Adress,Number,Complement,Neighborhood,City,State,ZipCode
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string DocumentId { get; set; }
    public List<AdressModel> Adress { get; set; }

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

public class AdressModel
{
    // Properties Adress,Number,Complement,Neighborhood,City,State,ZipCode
    public string Street { get; set; }
    public string Number { get; set; }
    public string Complement { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public TypeAdress AdressType { get; set; } //Residencial, //Comercial, //Entrega, //Cobrança
}

public enum TypeAdress
{
    Residencial,
    Comercial,
    Entrega,
    Cobrança
}
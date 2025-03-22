namespace OrderFlow.Api.Models;

public class AdressEntity
{
    public long Id { get; set; }

    public long CustomerId { get; set; }
    
    public string Street { get; set; }
    
    public string Number { get; set; }
    
    public string Complement { get; set; }
   
    public string Neighborhood { get; set; }
   
    public string City { get; set; }
   
    public string State { get; set; }
    
    public string ZipCode { get; set; }

    public string AdressType { get; set; }
}
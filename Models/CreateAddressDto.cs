namespace GetInItBackEnd.Models;

public class CreateAddressDto
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string BuildingNumber { get; set; }
    public string PostalCode { get; set; }
}
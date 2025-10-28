namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial record PersonDTO
{
    public Guid? CompanyFk { get; set; }
    public long? Id { get; set; }
    public long? DocumentTypeFk { get; set; }
    public string? DocumentValue { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public long? GenderFk { get; set; }
    public string? MobileNumber { get; set; }
    public byte[]? Avatar { get; set; }
    public string? FullName { get; set; }
    public string? DocumentTypeName { get; set; }
    public string? GenreName { get; set; }
};
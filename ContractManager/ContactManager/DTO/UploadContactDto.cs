using CsvHelper.Configuration.Attributes;

namespace ContactManager.DTO;

public class UploadContactDto
{
    public string Name { get; set; }
    
    [Name("Date of birth")]
    public DateTime DateOfBirth { get; set; }
    
    [Name("Married")]
    public bool IsMarried { get; set; }
    
    public string Phone { get; set; }
    
    public decimal Salary { get; set; }
}
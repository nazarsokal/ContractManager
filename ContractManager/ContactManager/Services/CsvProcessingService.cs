using System.Globalization;
using ContactManager.Models;
using ContactManager.ServiceContracts;
using CsvHelper;

namespace ContactManager.Services;

public class CsvProcessingService : ICsvProcessingService
{
    public async Task<List<Contact>> ProcessContactsAsync(IFormFile filePassed)
    {
        using var reader = new StreamReader(filePassed.OpenReadStream());
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        var records = csv.GetRecords<Contact>().ToList();

        return records;
    }
}
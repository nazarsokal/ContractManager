using ContactManager.DTO;
using ContactManager.Models;

namespace ContactManager.ServiceContracts;

public interface IContactService
{
    public Task<int> AddContactToDbAsync(List<PostContactDto> contacts);
    
    public Task<List<GetContactDto>> GetAllContactsAsync();
    
    public Task<int> DeleteContactAsync(Guid id);

    public Task<int> UpdateContactAsync(Guid id, UpdateContactDto? updateContact);
}
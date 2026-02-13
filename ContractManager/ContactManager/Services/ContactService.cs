using AutoMapper;
using ContactManager.Context;
using ContactManager.DTO;
using ContactManager.Models;
using ContactManager.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services;

public class ContactService : IContactService
{
    private readonly ContactDbContext _context;
    private readonly IMapper _mapper;
    
    public ContactService(ContactDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<int> AddContactToDbAsync(List<PostContactDto> contacts)
    {
        foreach (var contact in contacts)
        {
            contact.ContactId = Guid.NewGuid();
        }
        
        var contactEntities = _mapper.Map<List<Contact>>(contacts);
        _context.Contacts.AddRange(contactEntities);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<GetContactDto>> GetAllContactsAsync()
    {
        var contacts = await _context.Contacts.ToListAsync();
        return _mapper.Map<List<GetContactDto>>(contacts);
    }

    public Task<int> DeleteContactAsync(Guid id)
    {
        var contact = _context.Contacts.Find(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            return _context.SaveChangesAsync();
        }
        else
        {
            return Task.FromResult(0);
        }
    }

    public async Task<int> UpdateContactAsync(Guid id, UpdateContactDto? updateContact)
    {
        var updatedContact = _mapper.Map<Contact>(updateContact);
        updatedContact.ContactId = id;
        
        _context.Contacts.Update(updatedContact);
        return await _context.SaveChangesAsync();
    }
}
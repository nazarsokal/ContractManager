using AutoMapper;
using ContactManager.DTO;
using ContactManager.Models;

namespace ContactManager.Mapper;

public class ContactProfile : Profile
{
    public ContactProfile()
    {
        CreateMap<GetContactDto, Contact>();
        CreateMap<Contact, GetContactDto>();

        CreateMap<UploadContactDto, PostContactDto>()
            .ForMember(dest => dest.ContactId, opt => opt.MapFrom(_ => Guid.NewGuid()));

        CreateMap<PostContactDto, Contact>();
        
        CreateMap<UpdateContactDto, Contact>();
    }
}

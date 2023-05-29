using System.Xml;
using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Models.Validators;

namespace GetInItBackEnd;

public class GetInItMappingProfile : Profile
{
    public GetInItMappingProfile()
    {

        CreateMap<Account, AccountDto>()
            .ForPath(a => a.CompanyName, c => c.MapFrom(
                dto => dto.Company.Name))
            .ForPath(a => a.Role,c => c.MapFrom(dto => dto.Role));
        CreateMap<Account, AccountCompanyEmployeeDto>();
        CreateMap<CreateAccountDto, Account>();
        CreateMap<CreateEmployeeDto, Account>();
        CreateMap<UpdateEmailDto, Account>();
        CreateMap<Account, UpdateEmailDto>();
        
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        CreateMap<CreateAddressDto, Address>();
        
        CreateMap<TechnologyDto, Technology>();
        
        CreateMap<CreateOfferDto, Offer>();
        CreateMap<Offer,OfferDto >();

        

    }
}
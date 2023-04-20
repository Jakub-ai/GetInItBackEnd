using System.Xml;
using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;
using GetInItBackEnd.Models.Company;

namespace GetInItBackEnd;

public class GetInItMappingProfile : Profile
{
    public GetInItMappingProfile()
    {

        CreateMap<Account, AccountDto>()
            .ForPath(a => a.CompanyName, c => c.MapFrom(
                dto => dto.Company!.Name))
            .ForPath(a => a.Url, c => c.MapFrom(dto => dto.Company!.Url));
        CreateMap<Account, AccountCompanyEmployeeDto>();
        CreateMap<CreateAccountDto, Account>();
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        CreateMap<CreateAddressDto, Address>();
        
    }
}
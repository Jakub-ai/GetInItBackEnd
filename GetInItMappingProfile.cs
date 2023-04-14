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
     
        CreateMap<Account, CreateAccountDto>();
        CreateMap<Company, CompanyDto>();

        CreateMap<Address, AddressDto>();
      





        CreateMap<CreateAccountCompanyDto, Account>()

            .ForMember(a => a.Company, 
                c => c.MapFrom(dto => new Company
            {
              Name = dto.CompanyName,
              Url = dto.Url,
              Industry = dto.Industry,
              Nip = dto.Nip,
              Regon = dto.Regon,
              Address = new Address
              {
                  Country = dto.Country,
                  City = dto.City,
                  Street = dto.Street,
                  BuildingNumber = dto.BuildingNumber,
                  PostalCode = dto.PostalCode
              }
              
            }));
    }
}
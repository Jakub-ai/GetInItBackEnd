using System.Xml;
using AutoMapper;
using GetInItBackEnd.Entities;
using GetInItBackEnd.Models;
using GetInItBackEnd.Models.Account;
using GetInItBackEnd.Models.Address;
using GetInItBackEnd.Models.Company;
using GetInItBackEnd.Models.JobApplicationDto;
using GetInItBackEnd.Models.Offer;
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
        CreateMap<CreateAccountDto, Account>()
            .ForPath(a => a.Company.Address, c => c.MapFrom(dto => dto.CreateCompanyDto.AddressDto));
        CreateMap<CreateEmployeeDto, Account>();
        CreateMap<RegisterUserDto, Account>();
        CreateMap<UpdateEmailDto, Account>();
        CreateMap<UpdatePasswordDto, Account>();
        CreateMap<Account, UpdateEmailDto>();
        CreateMap<Account, ProfileDto>();
        
        CreateMap<Company, CompanyDto>();
        CreateMap<CreateCompanyDto, Company>();
        CreateMap<UpdateCompanyDto, Company>();

        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        CreateMap<CreateAddressDto, Address>();
        
        CreateMap<TechnologyDto, Technology>();
        CreateMap<Technology, TechnologyDto>();
        
        CreateMap<CreateOfferDto, Offer>().ForMember(o=> o.City, c => c.MapFrom(dto => dto.City));
        
        CreateMap<Offer, OfferDto>()
            .ForMember(o => o.CompanyName, c => c.MapFrom(c=> c.Company.Name))
            .ForMember(O => O.City, a => a.MapFrom(dto => dto.City))
            .ForPath(o => o.Technologies, c => c.MapFrom(dto => dto.Technologies));
        CreateMap<Offer, SearchOfferDto>()
            .ForMember(o => o.CompanyName, c => c.MapFrom(c=> c.Company.Name))
            .ForMember(O => O.City, a => a.MapFrom(dto => dto.City))
            .ForPath(o => o.Technologies, c => c.MapFrom(dto => dto.Technologies));
        CreateMap<Offer, TechnicalOfferDto>()
            .ForMember(o => o.CompanyName, c => c.MapFrom(c=> c.Company.Name))
            .ForMember(O => O.City, a => a.MapFrom(dto => dto.City))
            .ForPath(o => o.Technologies, c => c.MapFrom(dto => dto.Technologies));
        CreateMap<UpdateOfferDto, Offer>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<CreateJobApplicationDto, JobApplication>().ForMember(a => a.Name, c => c.MapFrom(d => d.ApplicantName));
        CreateMap<JobApplication,JobApplicationDto >().ForMember(a => a.ApplicantName, c => c.MapFrom(d => d.Name));;
        CreateMap<JobApplication,SearchApplicationDto >().ForMember(a => a.ApplicantName, c => c.MapFrom(d => d.Name));;


    }
}
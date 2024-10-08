using AutoMapper;
using CoberfuziFileManager.Domain.DTOs;
using CoberfuziFileManager.Models;

namespace CoberfuziFileManager.Domain.Mappings;

public class MappingProfile : Profile
{

    public MappingProfile()
    {

        CreateMap<Client, ClientCompleteDTO>();
        CreateMap<ClientCompleteDTO, Client>();

        CreateMap<Supplier, SupplierCompleteDTO>();
        CreateMap<SupplierCompleteDTO, Supplier>();

        CreateMap<Work, WorkCompleteDTO>();
        CreateMap<WorkCompleteDTO, Work>();

    }
    
}
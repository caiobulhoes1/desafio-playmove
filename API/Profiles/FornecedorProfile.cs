using API.Entities;
using AutoMapper;

namespace API.Profiles;

public class FornecedorProfile : Profile
{
    public FornecedorProfile()
    {
        CreateMap<Fornecedor, FornecedorDTO>();
        CreateMap<FornecedorDTO, Fornecedor>();
        CreateMap<FornecedorUpdateDTO, Fornecedor>();
        CreateMap<FornecedorCreateDTO, Fornecedor>();
        CreateMap<FornecedorDTO, FornecedorCreateDTO>();
    }
}
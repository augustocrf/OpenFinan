using System;
using AutoMapper;
using OpenFinan.Domain.Entity;
using OpenFinan.WebApi.Dtos.Cliente;

namespace OpenFinan.WebApi;

public class WebApiAutoMapperProfile : Profile
{
    public WebApiAutoMapperProfile()
    {
        //Cliente
        CreateMap<ClienteEntity, RetornaClienteGet>().ReverseMap();
        CreateMap<IncluiClientePost, ClienteEntity>().ReverseMap();
        CreateMap<AtualizaClientePut, ClienteEntity>().ReverseMap();
    }
}

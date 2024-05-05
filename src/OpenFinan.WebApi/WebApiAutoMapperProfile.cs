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
        //TipoFinanciamento
        CreateMap<TipoFinanciamentoEntity, RetornaTipoFinanciamentoGet>().ReverseMap();
        CreateMap<IncluiTipoFinanciamentoPost, TipoFinanciamentoEntity>().ReverseMap();
        CreateMap<AtualizaTipoFinanciamentoPut, TipoFinanciamentoEntity>().ReverseMap();
        //Financiamento
        CreateMap<FinanciamentoEntity, RetornaFinanciamentoGet>().ReverseMap();
        CreateMap<IncluiFinanciamentoPost, FinanciamentoEntity>().ReverseMap();
        CreateMap<AtualizaFinanciamentoPut, FinanciamentoEntity>().ReverseMap();
        //ParcelaFinanciamento
        CreateMap<ParcelaFinanciamentoEntity, RetornaParcelaFinanciamentoGet>().ReverseMap();
        CreateMap<IncluiParcelaFinanciamentoPost, ParcelaFinanciamentoEntity>().ReverseMap();
        CreateMap<AtualizaParcelaFinanciamentoPut, ParcelaFinanciamentoEntity>().ReverseMap();
         
    }
}

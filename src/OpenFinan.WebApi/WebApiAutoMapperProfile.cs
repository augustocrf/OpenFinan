using System;
using AutoMapper;
using OpenFinan.Domain.Entity;
using OpenFinan.WebApi.Dtos.Cliente;
using OpenFinan.WebApi.Dtos.TipoFinanciamento;
using OpenFinan.WebApi.Dtos.Financiamento;
using OpenFinan.WebApi.Dtos.ParcelaFinanciamento;

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
        CreateMap<RetornaFinanciamentoGet, FinanciamentoEntity>().ReverseMap();
            /*.ForMember(dst => dst.valorcredito, 
                        map => map.MapFrom(src => src.valortotal))
            .ForMember(dst => dst.dataultimovencimento,
                        map => map.MapFrom(src => src.dataprimeiraparcela.AddMonths(dst.quantidadeparcela)))
            .ReverseMap();*/
        CreateMap<IncluiFinanciamentoPost, FinanciamentoEntity>().ReverseMap();
        CreateMap<AtualizaFinanciamentoPut, FinanciamentoEntity>().ReverseMap();
        //ParcelaFinanciamento
        CreateMap<ParcelaFinanciamentoEntity, RetornaParcelaFinanciamentoGet>().ReverseMap();
        CreateMap<IncluiParcelaFinanciamentoPost, ParcelaFinanciamentoEntity>().ReverseMap();
        CreateMap<AtualizaParcelaFinanciamentoPut, ParcelaFinanciamentoEntity>().ReverseMap();
         
    }
}

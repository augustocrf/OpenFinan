using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Services;
using OpenFinan.WebApi.Dtos.TipoFinanciamento;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenFinan.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TipoFinanciamentoController : ControllerBase
{
    private readonly ITipoFinanciamentoService tipoFinanciamentoService;

    public TipoFinanciamentoController(ITipoFinanciamentoService tipoFinanciamentoService)
    {
        this.tipoFinanciamentoService = tipoFinanciamentoService ?? throw new ArgumentNullException(nameof(tipoFinanciamentoService));
    }

    // GET: api/values
    [HttpGet]
    //[ProducesResponseType(typeof(TipoFinanciamentoCoreException), 400)]
    [ProducesResponseType(typeof(IEnumerable<RetornaTipoFinanciamentoGet>), 200)]
    public async Task<IActionResult> Get()
    {
        var result = await tipoFinanciamentoService.RetornaTipoFinanciamentosAsync();
        var tipoFinanciamentos = Mapper.Map<IEnumerable<RetornaTipoFinanciamentoGet>>(result);

        return Ok(tipoFinanciamentos);
    }

    // GET api/values/1
    [HttpGet("{idtipofinanciamento}")]
    public async Task<IActionResult> Get(int idtipofinanciamento)
    {
        var result = await tipoFinanciamentoService.RetornaTipoFinanciamentoAsync(idtipofinanciamento);

        var tipofinanciamento = Mapper.Map<RetornaTipoFinanciamentoGet>(result);

        return Ok(tipofinanciamento);
    }

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]IncluiTipoFinanciamentoPost incluiTipoFinanciamentoPost)
    {
        var tipofinanciamento = Mapper.Map<TipoFinanciamentoEntity>(incluiTipoFinanciamentoPost);

        await tipoFinanciamentoService.IncluiTipoFinanciamentoAsync(tipofinanciamento);

        return Ok(tipofinanciamento);
    }

    // PUT api/values/2
    [HttpPut("{idtipofinanciamento}")]
    public async Task<IActionResult> Put(int idtipofinanciamento, [FromBody] AtualizaTipoFinanciamentoPut atualizaTipoFinanciamentoPut)
    {
        var tipofinanciamento = Mapper.Map<TipoFinanciamentoEntity>(atualizaTipoFinanciamentoPut);
        tipofinanciamento.idtipofinanciamento = idtipofinanciamento;

        try
        {
            await tipoFinanciamentoService.AtualizaTipoFinanciamentoAsync(tipofinanciamento);

            return Ok(tipofinanciamento);
        }
        catch (TipoFinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado.Key))
        {
            return NotFound(TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado);
        }
    }

    // DELETE api/values/3
    [HttpDelete("{idtipofinanciamento}")]
    public async Task<IActionResult> Delete(int idtipofinanciamento)
    {
        try
        {            
            await tipoFinanciamentoService.ExcluiTipoFinanciamentoAsync(idtipofinanciamento);

            return Ok();
        }
        catch (TipoFinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado.Key))
        {
            return NotFound(TipoFinanciamentoCoreError.TipoFinanciamentoNaoEncontrado);
        }
    }
}
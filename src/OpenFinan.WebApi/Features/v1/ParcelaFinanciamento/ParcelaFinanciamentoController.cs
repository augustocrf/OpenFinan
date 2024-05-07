using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Services;
using OpenFinan.WebApi.Dtos.ParcelaFinanciamento;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenFinan.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ParcelaFinanciamentoController : ControllerBase
{
    private readonly IParcelaFinanciamentoService parcelafinanciamentoService;

    public ParcelaFinanciamentoController(IParcelaFinanciamentoService parcelafinanciamentoService)
    {
        this.parcelafinanciamentoService = parcelafinanciamentoService ?? throw new ArgumentNullException(nameof(parcelafinanciamentoService));
    }

    // GET: api/values
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RetornaParcelaFinanciamentoGet>), 200)]
    public async Task<IActionResult> Get()
    {
        var result = await parcelafinanciamentoService.RetornaParcelasFinanciamentosAsync();
        var parcelasfinanciamentos = Mapper.Map<IEnumerable<RetornaParcelaFinanciamentoGet>>(result);

        return Ok(parcelasfinanciamentos);
    }

    // GET api/values/1
    [HttpGet("{idparcelafinanciamento}")]
    public async Task<IActionResult> Get(int idparcelafinanciamento)
    {
        var result = await parcelafinanciamentoService.RetornaParcelaFinanciamentoAsync(idparcelafinanciamento);

        var parcelafinanciamento = Mapper.Map<RetornaParcelaFinanciamentoGet>(result);

        return Ok(parcelafinanciamento);
    }

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]IncluiParcelaFinanciamentoPost incluiparcelafinanciamentopost)
    {
        var parcelafinanciamento = Mapper.Map<ParcelaFinanciamentoEntity>(incluiparcelafinanciamentopost);

        await parcelafinanciamentoService.IncluiParcelaFinanciamentoAsync(parcelafinanciamento);

        return Ok(parcelafinanciamento);
    }

    // PUT api/values/2
    [HttpPut("{idparcelafinanciamento}")]
    public async Task<IActionResult> Put(int idparcelafinanciamento, [FromBody] AtualizaParcelaFinanciamentoPut atualizaparcelafinanciamentoput)
    {
        var parcelafinanciamento = Mapper.Map<ParcelaFinanciamentoEntity>(atualizaparcelafinanciamentoput);
        parcelafinanciamento.idparcelafinanciamento = idparcelafinanciamento;

        try
        {
            await parcelafinanciamentoService.AtualizaParcelaFinanciamentoAsync(parcelafinanciamento);

            return Ok(parcelafinanciamento);
        }
        catch (ParcelaFinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado.Key))
        {
            return NotFound(ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado);
        }
    }

    // DELETE api/values/3
    [HttpDelete("{idparcelafinanciamento}")]
    public async Task<IActionResult> Delete(int idparcelafinanciamento)
    {
        try
        {            
            await parcelafinanciamentoService.ExcluiParcelaFinanciamentoAsync(idparcelafinanciamento);

            return Ok();
        }
        catch (ParcelaFinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado.Key))
        {
            return NotFound(ParcelaFinanciamentoCoreError.ParcelaFinanciamentoNaoEncontrado);
        }
    }
}
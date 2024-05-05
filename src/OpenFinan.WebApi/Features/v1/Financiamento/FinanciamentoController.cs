using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Services;
using OpenFinan.WebApi.Dtos.Financiamento;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenFinan.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class FinanciamentoController : ControllerBase
{
    private readonly IFinanciamentoService financiamentoService;

    public FinanciamentoController(IFinanciamentoService financiamentoService)
    {
        this.financiamentoService = financiamentoService ?? throw new ArgumentNullException(nameof(financiamentoService));
    }

    // GET: api/values
    [HttpGet]
    //[ProducesResponseType(typeof(TipoFinanciamentoCoreException), 400)]
    [ProducesResponseType(typeof(IEnumerable<RetornaFinanciamentoGet>), 200)]
    public async Task<IActionResult> Get()
    {
        var result = await financiamentoService.RetornaFinanciamentosAsync();
        var financiamentos = Mapper.Map<IEnumerable<RetornaFinanciamentoGet>>(result);

        return Ok(financiamentos);
    }

    // GET api/values/1
    [HttpGet("{idfinanciamento}")]
    public async Task<IActionResult> Get(int idfinanciamento)
    {
        var result = await financiamentoService.RetornaFinanciamentoAsync(idfinanciamento);

        var financiamento = Mapper.Map<RetornaFinanciamentoGet>(result);

        return Ok(financiamento);
    }

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]IncluiFinanciamentoPost incluiFinanciamentoPost)
    {
        var financiamento = Mapper.Map<FinanciamentoEntity>(incluiFinanciamentoPost);

        await financiamentoService.IncluiFinanciamentoAsync(financiamento);

        return Ok(financiamento);
    }

    // PUT api/values/2
    [HttpPut("{idfinanciamento}")]
    public async Task<IActionResult> Put(int idfinanciamento, [FromBody] AtualizaFinanciamentoPut atualizaFinanciamentoPut)
    {
        var financiamento = Mapper.Map<FinanciamentoEntity>(atualizaFinanciamentoPut);
        financiamento.idfinanciamento = idfinanciamento;

        try
        {
            await financiamentoService.AtualizaFinanciamentoAsync(financiamento);

            return Ok(financiamento);
        }
        catch (FinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == FinanciamentoCoreError.FinanciamentoNaoEncontrado.Key))
        {
            return NotFound(FinanciamentoCoreError.FinanciamentoNaoEncontrado);
        }
    }

    // DELETE api/values/3
    [HttpDelete("{idfinanciamento}")]
    public async Task<IActionResult> Delete(int idfinanciamento)
    {
        try
        {            
            await financiamentoService.ExcluiFinanciamentoAsync(idfinanciamento);

            return Ok();
        }
        catch (FinanciamentoCoreException ex) when (ex.Errors.Any(c => c.Key == FinanciamentoCoreError.FinanciamentoNaoEncontrado.Key))
        {
            return NotFound(FinanciamentoCoreError.FinanciamentoNaoEncontrado);
        }
    }
}
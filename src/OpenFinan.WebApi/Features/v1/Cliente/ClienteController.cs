using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OpenFinan.Domain.Exceptions;
using OpenFinan.Domain.Entity;
using OpenFinan.Domain.Services;
using OpenFinan.WebApi.Dtos.Cliente;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenFinan.WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService clienteService;

    public ClienteController(IClienteService clienteService)
    {
        this.clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
    }

    // GET: api/values
    [HttpGet]
    //[ProducesResponseType(typeof(ClienteCoreException), 400)]
    [ProducesResponseType(typeof(IEnumerable<RetornaClienteGet>), 200)]
    public async Task<IActionResult> Get()
    {
        var result = await clienteService.RetornaClientesAsync();
        var clientes = Mapper.Map<IEnumerable<RetornaClienteGet>>(result);

        return Ok(clientes);
    }

    // GET api/values/2003892003
    [HttpGet("{cpf}")]
    public async Task<IActionResult> Get(int cpf)
    {
        var result = await clienteService.RetornaClienteAsync(cpf);

        var cliente = Mapper.Map<RetornaClienteGet>(result);

        return Ok(cliente);
    }

    // POST api/values
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]IncluiClientePost incluiClientePost)
    {
        var cliente = Mapper.Map<ClienteEntity>(incluiClientePost);

        await clienteService.IncluiClienteAsync(cliente);

        return Ok(cliente);
    }

    // PUT api/values/500200030010
    [HttpPut("{cpf}")]
    public async Task<IActionResult> Put(int cpf, [FromBody] AtualizaClientePut atualizaClientePut)
    {
        var cliente = Mapper.Map<ClienteEntity>(atualizaClientePut);
        cliente.cpf = cpf;

        try
        {
            await clienteService.AtualizaClienteAsync(cliente);

            return Ok(cliente);
        }
        catch (ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
        {
            return NotFound(ClienteCoreError.ClienteNaoEncontrado);
        }
    }

    // DELETE api/values/51023145678
    [HttpDelete("{cpf}")]
    public async Task<IActionResult> Delete(int cpf)
    {
        try
        {            
            await clienteService.ExcluiClienteAsync(cpf);

            return Ok();
        }
        catch (ClienteCoreException ex) when (ex.Errors.Any(c => c.Key == ClienteCoreError.ClienteNaoEncontrado.Key))
        {
            return NotFound(ClienteCoreError.ClienteNaoEncontrado);
        }
    }
}
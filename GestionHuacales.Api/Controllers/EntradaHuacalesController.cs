using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradaHuacalesController(EntradaHuacalesServices entradaHuacalesServices) : ControllerBase
{
    // GET: api/<EntradaHuacalesController>
    [HttpGet]
    public async Task<EntradaHuacalesDto[]> Get()
    {
        return await entradaHuacalesServices.Listar(h => true);
    }

    // GET api/<EntradaHuacalesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<EntradaHuacalesController>
    [HttpPost]
    public async Task Post([FromBody] EntradaHuacalesDto entradaHuacales)
    {
        var huacales  = new EntradaHuacales
        {
            Fecha = DateTime.Now,
            NombreCliente = entradaHuacales.NombreCliente,
            entradaHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradaHuacalesDetalle
            {
                IdTipo = h.IdTipo,
                Cantidad = h.Cantidad,
                Precio = h.Precio,
            }).ToArray()
        };
       await entradaHuacalesServices.Guardar(huacales);
    }

    // PUT api/<EntradaHuacalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] EntradaHuacalesDto entradaHuacales)
    {

        var existente = await entradaHuacalesServices.Buscar(id);

        if (existente == null)
        {
            var nuevo = new EntradaHuacales
            {
                IdEntrada = id,
                Fecha = DateTime.Now,
                NombreCliente = entradaHuacales.NombreCliente,
                entradaHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradaHuacalesDetalle
                {
                    IdTipo = h.IdTipo,
                    Cantidad = h.Cantidad,
                    Precio = h.Precio,

                }).ToArray()
            };
            await entradaHuacalesServices.Guardar(nuevo);
        }

        existente.NombreCliente = entradaHuacales.NombreCliente;
        existente.entradaHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradaHuacalesDetalle
        {
            IdTipo = h.IdTipo,
            Cantidad = h.Cantidad,
            Precio = h.Precio,
        }).ToArray();

        await entradaHuacalesServices.Guardar(existente);
    }

    // DELETE api/<EntradaHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task<string> Delete(int id)
    {
        var eliminado=await entradaHuacalesServices.Eliminar(id);

        if (eliminado)
            return "Entrada eliminada correctamente.";
        else
            return "No se encontró la entrada especificada.";


    }
}

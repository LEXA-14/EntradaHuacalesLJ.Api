using GestionHuacales.Api.DTO;
using GestionHuacales.Api.Models;
using GestionHuacales.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace GestionHuacales.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TiposHuacalesDtoController(TipoHuacalesServices tipoHuacalesServices) : ControllerBase
{
    // GET: api/<TiposHuacales>
    [HttpGet]
    public async Task<List<TipoHuacalesDto>> Get()
    {

        var lista = await tipoHuacalesServices.Lista(t => true);

        return lista.Select(t => new TipoHuacalesDto
        {
            Descripcion = t.Descripcion,
            //Existencia = t.Existencia,
        }).ToList();


    }

    // GET api/<TiposHuacales>/5
    [HttpGet("{id}")]
    public async Task<TipoHuacalesDto?> Get(int id)
    {
        var tipo = await tipoHuacalesServices.Buscar(id);

        if (tipo == null) return null;

        return new TipoHuacalesDto
        {
            Descripcion = tipo.Descripcion

        };
    }

    // POST api/<TiposHuacales>
    [HttpPost]
    public async Task Post([FromBody] TipoHuacalesDto tiposHuacales)
    {
        var tipo = new TiposHuacales
        {
            Descripcion = tiposHuacales.Descripcion,
            //Existencia = tiposHuacales.Existencia
        };

        await tipoHuacalesServices.Guardar(tipo);



    }

    // PUT api/<TiposHuacales>/5
    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] TipoHuacalesDto tiposHuacales)
    {

        var existente = await tipoHuacalesServices.Buscar(id);

        if (existente == null)
        {
            var nuevo = new TiposHuacales
            {
                IdTipo = id,
                Descripcion = tiposHuacales.Descripcion,
                //Existencia = tiposHuacales.Existencia,

            };
            await tipoHuacalesServices.Guardar(nuevo);
        }
        else
        {


            existente.Descripcion = tiposHuacales.Descripcion;
            //existente.Existencia = tiposHuacales.Existencia;

            await tipoHuacalesServices.Modificar(existente);
        }

    }

    // DELETE api/<TiposHuacales>/5
    [HttpDelete("{id}")]
    public async Task<string> Delete(int id)
    {
        var eliminado = await tipoHuacalesServices.Eliminar(id);

        if (eliminado)
            return "Entrada eliminada correctamente.";
        else
            return "No se encontró la entrada especificada.";

    }
}









using GestionHuacales.Api.DAL;
using GestionHuacales.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestionHuacales.Api.Services;

public class TipoHuacalesServices(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<bool>Guardar(TiposHuacales tiposHuacales)
    {
        if(!await Existe(tiposHuacales.IdTipo))
        {
          return await Insertar(tiposHuacales);
        }
        else
        {
             return await Modificar(tiposHuacales);
        }
    }

    private async Task<bool>Insertar(TiposHuacales tipohuacales)
    {
        using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Add(tipohuacales);
        return await contexto.SaveChangesAsync() > 0;
    }
    
    private async Task<bool>Existe(int id)
    {
        using var contexto=await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.AnyAsync(t=>t.IdTipo==id);
    }

    public async Task<bool>Modificar(TiposHuacales tipohuacales)
    {
        using var contexto=await DbFactory.CreateDbContextAsync();
        contexto.Update(tipohuacales);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<TiposHuacales?>Buscar(int id)
    {
        using var contexto=await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.FirstOrDefaultAsync(t => t.IdTipo == id);
    }
    //eliminar 
    //Listar

    public async Task<bool>Eliminar(int id)
    {
        using var contexto= await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.Where(t => t.IdTipo == id).ExecuteDeleteAsync()>0;
    }

    public async Task<List<TiposHuacales>> Lista(Expression<Func<TiposHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.Where(criterio).ToListAsync();
    }
}

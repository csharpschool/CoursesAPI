using Courses.Data.Interfaces;

namespace Courses.API.Extensions;

public static class HttpExtensions
{
    public static async Task<IResult> HttpGetAsync<TEntity, TDto>(this IDbService db) where TEntity : class, IEntity where TDto : class =>
        Results.Ok(await db.GetAsync<TEntity, TDto>());
    public static async Task<IResult> HttpSingleAsync<TEntity, TDto>(this IDbService db, int id) where TEntity : class, IEntity where TDto : class
    {
        var result = await db.SingleAsync<TEntity, TDto>(e => e.Id.Equals(id));
        if (result is null) return Results.NotFound();
        return Results.Ok(result);
    }
    public static async Task<IResult> HttpPostAsync<TEntity, TDto>(this IDbService db, TDto dto) where TEntity : class, IEntity where TDto : class
    {
        try
        {
            var entity = await db.AddAsync<TEntity, TDto>(dto);
            if (await db.SaveChangesAsync())
            {
                var node = typeof(TEntity).Name.ToLower();
                return Results.Created($"/{node}s/{entity.Id}", entity);
            }
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.");
    }
    public static async Task<IResult> HttpPutAsync<TEntity, TDto>(this IDbService db, int id, TDto dto) where TEntity : class, IEntity where TDto : class
    {
        try
        {
            if (!await db.AnyAsync<TEntity>(e => e.Id.Equals(id))) return Results.NotFound();

            db.Update<TEntity, TDto>(id, dto);
            if (await db.SaveChangesAsync()) return Results.NoContent();

        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't update the {typeof(TEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't update the {typeof(TEntity).Name} entity.");
    }
    public static async Task<IResult> HttpDeleteAsync<TEntity>(this IDbService db, int id) where TEntity : class, IEntity
    {
        try
        {
            if (!await db.DeleteAsync<TEntity>(id)) return Results.NotFound();

            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't delete the {typeof(TEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't delete the {typeof(TEntity).Name} entity.");
    }
    public static async Task<IResult> HttpDeleteAsync<TReferenceEntity, TDto>(this IDbService db, TDto dto) where TReferenceEntity : class, IReferenceEntity where TDto : class
    {
        try
        {
            if (!db.Delete<TReferenceEntity, TDto>(dto)) return Results.NotFound();

            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't delete the {typeof(TReferenceEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't delete the {typeof(TReferenceEntity).Name} entity.");
    }
    public static async Task<IResult> HttpAddAsync<TEntity, TDto>(this IDbService db, TDto dto) where TEntity : class, IReferenceEntity where TDto : class
    {
        try
        {
            var entity = await db.AddAsync<TEntity, TDto>(dto);
            if (await db.SaveChangesAsync()) return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.\n{ex}.");
        }

        return Results.BadRequest($"Couldn't add the {typeof(TEntity).Name} entity.");
    }
}

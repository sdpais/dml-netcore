using DML.PostgresDb.Services;
using DML.PostgresDb.Interfaces;
using DML.Domain.Entities;
using DML.ConfigServices;
using DML.ConfigServices.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DML.RBAC.Service;


public class EntityService
{
    protected IDbServiceAsync _dbServiceAsync;
    public EntityService(IConfiguration configuration)
    {
        _dbServiceAsync = new DbServiceAsync(configuration);
    }
    public async Task<Entity> Create(Entity entity)
    {
        int id = await _dbServiceAsync.ExecuteScalar("INSERT INTO ENTITIES (EntityName, ClientId, ModuleId, IsActive) Values (@entityname, @ClientId, @ModuleId, @IsActive) RETURNING Id; ", entity);
        entity.Id = id;
        return entity;
    }

    public async Task<Entity> Update(Entity entity)
    {
        await _dbServiceAsync.EditData("UPDATE ENTITIES SET EntityName = @entityname, ModuleId = @ModuleId, IsActive = @IsActive WHERE Id = @Id and ClientId = @ClientId", entity);
        return entity;
    }

    public async Task<bool>  Delete(Entity entity)
    {
        entity.IsActive = false;
        await _dbServiceAsync.ExecuteScalar("UPDATE ENTITIES SET IsActive = false WHERE Id = @Id  and ClientId = @ClientId", entity);
        return true;
    }

    public async Task<List<Entity>> Get()
    {
       var entities =  await _dbServiceAsync.GetMany<Entity>("SELECT Id, EntityName, ClientId, ModuleId, IsActive, CreatedOn, UpdatedOn FROM Entities  WHERE IsActive =  true ");
        return entities.ToList<Entity>();
    }
}

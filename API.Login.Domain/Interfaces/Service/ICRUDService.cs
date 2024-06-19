using API.Login.Domain.Dtos.Response;

namespace API.Login.Domain.Interfaces.Service;

public interface ICRUDService<TEntity> where TEntity : class
{
    Task<ControllerMessenger> AddAsync(TEntity entity);
    Task<ControllerMessenger> DeleteAsync(int? id);
    Task<ControllerMessenger> GetByIdAsync(int? id);
    Task<ControllerMessenger> UpdateAsync(int id, object objectUpdated);
}

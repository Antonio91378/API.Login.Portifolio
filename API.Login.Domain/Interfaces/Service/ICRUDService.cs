using API.Login.Domain.Dtos.Response;
using API.Login.Domain.Entities.User;

namespace API.Login.Domain.Interfaces.Service;

public interface ICRUDService<TEntity> where TEntity : class
{
    Task<ControllerMessenger> AddAsync(TEntity entity);
    Task<ControllerMessenger> DeleteAsync(int? id);
    Task<ControllerMessenger> GetAsync(int? id);
    Task<ControllerMessenger> UpdateAsync(int id, object objectUpdated);
}

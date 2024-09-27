using CompanyManagerDev.Models.Db;

namespace CompanyManagerDev.Services
{
    public interface IDbManager<T> where T : BaseEntity
    {
        Task SaveAsync(T entity);

        Task SaveAsync(T entity, string topic);

        Task UpdateAsync(T entity);

        Task UpdateAsync(T entity, string topic);

    }
}

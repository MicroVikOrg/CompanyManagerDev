using CompanyManagerDev.Models.Db;

namespace CompanyManagerDev.Services
{
    public interface IDbManagerFactory
    {
        DbManager<T> GetDbManager<T>() where T : BaseEntity;

    }
}

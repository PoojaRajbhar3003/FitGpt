using FitGpt.Models.ResponseModels;

namespace FitGpt.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<RepositoryResponseModel<List<T>>> GetAll();
        Task<RepositoryResponseModel<T>> GetWhereAsync(string colName, object value);
        Task<RepositoryResponseModel<List<T>>> GetAllWhereAsync(string colName, object value);
        Task<RepositoryResponseModel<bool>> PostAsync(T entity);
        Task<RepositoryResponseModel<bool>> GetAnyAsync(string colName, object value);
        Task<RepositoryResponseModel<bool>> UpdateWhereAsync(string colName, object value, Action<T> updateAction);
        Task<RepositoryResponseModel<bool>> DeleteWhereAsync(string colName, object value);
        Task<RepositoryResponseModel<bool>> DeleteAsync(T entity);
    }
}

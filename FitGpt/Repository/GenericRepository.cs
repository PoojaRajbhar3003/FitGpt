using FitGpt.Data;
using FitGpt.IRepository;
using FitGpt.Models.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FitGpt.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly FitGptDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(FitGptDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }

        public Expression<Func<T, bool>>CreateLambdaExpression(string colName, object value)
        {
            // Create the parameter expression
            var parameter = Expression.Parameter(typeof(T), "x");

            // Create the property expression
            var property = Expression.Property(parameter, colName);

            var propertyType = property.Type;

            // Convert value to the property type
            object convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(value, propertyType);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }

            // Create the constant expression
            var constant = Expression.Constant(convertedValue, propertyType);

            // Create the equality expression
            var equality = Expression.Equal(property, constant);

            // Create the lambda expression
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);

            return lambda;
        }
        public T GetById(object id)
        {
            var keyProperty = typeof(T).GetProperty("Id");
            if (keyProperty == null)
            {
                throw new InvalidOperationException("Entity must have an 'Id' property.");
            }
            return null; 
        }

        public async Task<RepositoryResponseModel<List<T>>> GetAll()
        {
            RepositoryResponseModel<List<T>> dBResponse = new();
            try
            {
                dBResponse.Data = await _dbSet.ToListAsync();
                dBResponse.Success = dBResponse.Data.Count > 0;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<RepositoryResponseModel<T>> GetWhereAsync(string colName, object value)
        {
            RepositoryResponseModel<T> dBResponse = new();
            // Create the parameter expression
            var parameter = Expression.Parameter(typeof(T), "x");

            // Create the property expression
            var property = Expression.Property(parameter, colName);

            var propertyType = property.Type;

            // Convert value to the property type
            object convertedValue;
            try
            {
                convertedValue = Convert.ChangeType(value, propertyType);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);

            }

            // Create the constant expression
            var constant = Expression.Constant(convertedValue, propertyType);

            // Create the equality expression
            var equality = Expression.Equal(property, constant);

            // Create the lambda expression
            var lambda = Expression.Lambda<Func<T, bool>>(equality, parameter);
            try
            {
                dBResponse.Data = await _dbSet.Where(lambda).FirstOrDefaultAsync();
                dBResponse.Success = dBResponse.Data != null;
                return dBResponse;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<RepositoryResponseModel<List<T>>> GetAllWhereAsync(string colName, object value)
        {
            RepositoryResponseModel<List<T>> dBResponse = new();
            var lambda = CreateLambdaExpression(colName, value);

            try
            {
                dBResponse.Data = await _dbSet.Where(lambda).ToListAsync();
                dBResponse.Success = dBResponse.Data.Count > 0;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<RepositoryResponseModel<bool>> GetAnyAsync(string colName, object value)
        {
            RepositoryResponseModel<bool> dBResponse = new();
            var lambda = CreateLambdaExpression(colName, value);

            try
            {
                dBResponse.Success = await _dbSet.AnyAsync(lambda);
                return dBResponse;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
            
        }

        public async Task<RepositoryResponseModel<bool>> PostAsync(T entity)
        {
            RepositoryResponseModel<bool> dBResponse = new();
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                dBResponse.Success = true;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<RepositoryResponseModel<bool>> DeleteWhereAsync(string colName, object value)
        {
            RepositoryResponseModel<bool> dBResponse = new();

            var lambda = CreateLambdaExpression(colName, value);

            try
            {
                var entities = await _dbSet.Where(lambda).ToListAsync();
                if (!entities.Any())
                {
                    dBResponse.Success = false;
                    return dBResponse;
                }

                _dbSet.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();

                dBResponse.Success = true;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the delete request.", ex);
            }
        }
        public async Task<RepositoryResponseModel<bool>> DeleteAsync(T entity)
        {
            RepositoryResponseModel<bool> dBResponse = new();

            try
            {
                _dbSet.RemoveRange(entity);
                await _dbContext.SaveChangesAsync();

                dBResponse.Success = true;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the delete request.", ex);
            }
        }

        public async Task<RepositoryResponseModel<bool>> UpdateWhereAsync(string colName, object value, Action<T> updateAction)
        {
            RepositoryResponseModel<bool> dBResponse = new();

            var lambda = CreateLambdaExpression(colName, value);

            try
            {
                var entities = await _dbSet.Where(lambda).ToListAsync();
                if (!entities.Any())
                {
                    dBResponse.Success = false;
                    return dBResponse;
                }

                foreach (var entity in entities)
                {
                    updateAction(entity);
                }
                
                await _dbContext.SaveChangesAsync();

                dBResponse.Success = true;
                return dBResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the update request.", ex);
            }
        }

    }
}

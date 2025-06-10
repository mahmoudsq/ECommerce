using ECommerce.Core.Common;
using ECommerce.Core.Interfaces;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context = context;

        public async Task<Result> Add(T entity)
        {
            try
            {
                if (entity == null)
                    return Result.Failure("Entity cannot be null", ErrorType.Validation);

                await _context.Set<T>().AddAsync(entity);
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result.Failure("Failed to add entity - no changes were saved", ErrorType.Forbidden);

                return Result.Success("Success");
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure($"Database error while adding entity: {ex.InnerException?.Message ?? ex.Message}", ErrorType.ServerError);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to add entity: {ex.Message}", ErrorType.ServerError);
            }
        }

        public async Task<Result> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return Result.Failure("Invalid ID provided", ErrorType.Validation);
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                    return Result.Failure($"Entity with ID {id} not found", ErrorType.NotFound);
                _context.Set<T>().Remove(entity);
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result.Failure("Failed to delete entity - no changes were saved", ErrorType.Forbidden);

                return Result.Success("Success");

            }
            catch (DbUpdateException ex)
            {
                return Result.Failure($"Database error while deleting entity: {ex.InnerException?.Message ?? ex.Message}", ErrorType.ServerError);
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to delete entity: {ex.Message}", ErrorType.Forbidden);

            }

        }

        public async Task<Result<IReadOnlyList<T>>> GetAll() => 
            Result.Success<IReadOnlyList<T>>(await _context.Set<T>().AsNoTracking().ToListAsync());

        public async Task<Result<IReadOnlyList<T>>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return Result.Success<IReadOnlyList<T>>(await query.AsNoTracking().ToListAsync());
        }

        public async Task<Result<T?>> GetById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity == null)
                return Result.Failure<T?>($"Entity with ID {id} not found", ErrorType.NotFound);

            return Result.Success<T?>(entity);
        }

        public async Task<Result<T?>> GetById(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
                query = query.Include(include);
            var entity = await query.AsNoTracking().FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
            return Result.Success(entity);
        }

        public async Task<Result> Update(T entity)
        {
            try
            {
                if (entity == null)
                    return Result.Failure("Entity cannot be null");

                // Check if entity exists
                var entityId = GenericRepository<T>.GetEntityId(entity);
                if (entityId <= 0)
                    return Result.Failure("Entity must have a valid ID", ErrorType.NotFound);

               /* var existingEntity = await _context.Set<T>().FindAsync(entityId);
                if (existingEntity == null)
                    return Result.Failure($"Entity with ID {entityId} not found", ErrorType.NotFound);*/


                _context.Entry(entity).State = EntityState.Modified;
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected == 0)
                    return Result.Failure("No changes were detected or saved", ErrorType.Forbidden);

                return Result.Success("Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure("The entity was modified by another user. Please refresh and try again",
                    ErrorType.Conflict);
            }
            catch (DbUpdateException ex)
            {
                return Result.Failure($"Database error while updating entity: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return Result.Failure($"Failed to update entity: {ex.Message}", ErrorType.Forbidden);
            }

        }

        private static int GetEntityId(T entity)
        {
            var property = typeof(T).GetProperty("Id");
            return property != null ? (int)(property.GetValue(entity) ?? 0) : 0;
        }
    }
}

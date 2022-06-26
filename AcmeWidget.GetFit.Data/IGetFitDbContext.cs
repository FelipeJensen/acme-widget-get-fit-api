namespace AcmeWidget.GetFit.Data;

public interface IGetFitDbContext
{
    IQueryable<T> Query<T>() where T : class;
    Task<T?> FindAsync<T, TK>(TK id) where T : class;
    Task AddAsync<T>(T entity);
    void Remove<T>(T entity);
    Task SaveChangesAsync();
}
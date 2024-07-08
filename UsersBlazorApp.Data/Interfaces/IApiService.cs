namespace UsersBlazorApp.Data.Interfaces;

public interface IApiService<T>
{
	public Task<List<T>> GetAllAsync();

	public Task<T> GetIdAsync(int id);

	public Task<T> AddAsync(T entity);

	public Task<bool> UpdateAsync(T entity);

	public Task<bool> DeleteAsync(int id);
}

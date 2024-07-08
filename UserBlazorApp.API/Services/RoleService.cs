using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class RoleService(UsersDbContext Contexto) : IApiService<AspNetRoles>
{
	public async Task<List<AspNetRoles>> GetAllAsync()
	{
		return await Contexto.AspNetRoles
			.Include(u => u.AspNetRoleClaims).ToListAsync();
	}

	public async Task<AspNetRoles> GetIdAsync(int id)
	{
		return await Contexto.AspNetRoles
			.Include(r => r.AspNetRoleClaims)
			.FirstOrDefaultAsync(r => r.Id == id);
	}

	public async Task<AspNetRoles> AddAsync(AspNetRoles rol)
	{
		Contexto.AspNetRoles.Add(rol);
		await Contexto.SaveChangesAsync();
		return rol;
	}

	public async Task<bool> UpdateAsync(AspNetRoles rol)
	{
		Contexto.Entry(rol).State = EntityState.Modified;
		return await Contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var rol = await Contexto.AspNetRoles.FindAsync(id);
		if (rol == null) return false;

		Contexto.AspNetRoles.Remove(rol);
		return await Contexto.SaveChangesAsync() > 0;
	}
}

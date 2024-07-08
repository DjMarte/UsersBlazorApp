using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class RoleClaimService(UsersDbContext Contexto) : IApiService<AspNetRoleClaims>
{
	public async Task<List<AspNetRoleClaims>> GetAllAsync() {
		return await Contexto.AspNetRoleClaims.Include(r => r.Role).ToListAsync();
	}

	public async Task<AspNetRoleClaims> GetIdAsync(int id) {
		return await Contexto.AspNetRoleClaims.FindAsync(id);
	}

	public async Task<AspNetRoleClaims> AddAsync(AspNetRoleClaims user) {
		Contexto.AspNetRoleClaims.Add(user);
		await Contexto.SaveChangesAsync();
		return user;
	}

	public async Task<bool> UpdateAsync(AspNetRoleClaims user) {
		Contexto.Entry(user).State = EntityState.Modified;
		return await Contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteAsync(int id) {
		var usuario = await Contexto.AspNetRoleClaims.FindAsync(id);
		if (usuario == null) return false;

		Contexto.AspNetRoleClaims.Remove(usuario);
		return await Contexto.SaveChangesAsync() > 0;
	}
}

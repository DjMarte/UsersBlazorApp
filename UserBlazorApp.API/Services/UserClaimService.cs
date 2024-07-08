using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UserClaimService(UsersDbContext Contexto) : IApiService<AspNetUserClaims>
{
	public async Task<List<AspNetUserClaims>> GetAllAsync()
	{
		return await Contexto.AspNetUserClaims.Include(u => u.UserId).ToListAsync();
	}

	public async Task<AspNetUserClaims> GetIdAsync(int id)
	{
		return await Contexto.AspNetUserClaims.FindAsync(id);
	}

	public async Task<AspNetUserClaims> AddAsync(AspNetUserClaims userClaim)
	{
		Contexto.AspNetUserClaims.Add(userClaim);
		await Contexto.SaveChangesAsync();
		return userClaim;
	}

	public async Task<bool> UpdateAsync(AspNetUserClaims userClaim)
	{
		Contexto.Entry(userClaim).State = EntityState.Modified;
		return await Contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var userClaim = await Contexto.AspNetUserClaims.FindAsync(id);
		if (userClaim == null) return false;

		Contexto.AspNetUserClaims.Remove(userClaim);
		return await Contexto.SaveChangesAsync() > 0;
	}
}

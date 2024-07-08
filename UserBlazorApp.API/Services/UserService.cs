using Microsoft.EntityFrameworkCore;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Services;

public class UserService(UsersDbContext Contexto) /*: IApiService<AspNetUsers>*/
{
	public async Task<List<AspNetUsers>> GetAllAsync() {
		return await Contexto.AspNetUsers
			.Include(u => u.AspNetUserClaims)
			.Include(r => r.Role)
				.ThenInclude(a => a.AspNetRoleClaims)
			.ToListAsync();
	}

	public async Task<AspNetUsers> GetIdAsync(int id) {
		return await Contexto.AspNetUsers
			.Include(u => u.AspNetUserClaims)
			.Include(u => u.Role)
				.ThenInclude(r => r.AspNetRoleClaims)
			.FirstOrDefaultAsync(u => u.Id == id);
	}

	public async Task<AspNetUsers> AddAsync(AspNetUsers user, List<int> rolesId) {
		// Asignar roles al usuario si hay roles en la solicitud
		if (rolesId != null && rolesId.Any()) {
			var roles = await Contexto.AspNetRoles
				.Where(role => rolesId.Contains(role.Id))
				.Include(role => role.AspNetRoleClaims)
				.ToListAsync();

			user.Role = roles;

			foreach (var role in roles) {
				foreach (var claim in role.AspNetRoleClaims) {
					user.AspNetUserClaims.Add(new AspNetUserClaims {
						ClaimType = claim.ClaimType,
						ClaimValue = claim.ClaimValue,
						User = user
					});
				}
			}
		}

		Contexto.AspNetUsers.Add(user);
		await Contexto.SaveChangesAsync();
		return user;
	}

	public async Task<bool> UpdateAsync(AspNetUsers user) {
		Contexto.Entry(user).State = EntityState.Modified;
		return await Contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> DeleteAsync(int id) {
		var usuario = await Contexto.AspNetUsers.FindAsync(id);
		if (usuario == null) return false;

		Contexto.AspNetUsers.Remove(usuario);
		return await Contexto.SaveChangesAsync() > 0;
	}
}

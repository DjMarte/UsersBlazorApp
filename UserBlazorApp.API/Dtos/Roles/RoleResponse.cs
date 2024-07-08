using UserBlazorApp.API.Dtos.Claims;

namespace UserBlazorApp.API.Dtos.Roles;

public class RoleResponse
{
	public int Id { get; set; }

	public string? Name { get; set; }

	public List<RoleClaimResponse> RoleClaims { get; set; } = new List<RoleClaimResponse>();
}

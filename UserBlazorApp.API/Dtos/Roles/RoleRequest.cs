using UserBlazorApp.API.Dtos.Claims;

namespace UserBlazorApp.API.Dtos.Roles;

public class RoleRequest
{
	public string? Name { get; set; }

	public List<RoleClaimRequest> RoleClaims { get; set; } = new List<RoleClaimRequest>();
}

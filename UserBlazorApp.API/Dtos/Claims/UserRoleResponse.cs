namespace UserBlazorApp.API.Dtos.Claims;

public class UserRoleResponse
{
	public int Id { get; set; }

	public string? Name { get; set; }

	public ICollection<RoleClaimResponse> RoleClaims { get; set; } = new List<RoleClaimResponse>();
}
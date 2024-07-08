using UserBlazorApp.API.Dtos.Claims;

namespace UserBlazorApp.API.Dtos.Users;

public class UserResponse
{
	public int Id { get; set; }

	public string? UserName { get; set; }

	public string? Email { get; set; }

	public string? PasswordHash { get; set; }

	public string? PhoneNumber { get; set; }

	public DateTimeOffset? LockoutEnd { get; set; }

	public List<UserClaimResponse> Claims { get; set; } = new List<UserClaimResponse>();

	public List<UserRoleResponse> Roles { get; set; } = new List<UserRoleResponse>();
}

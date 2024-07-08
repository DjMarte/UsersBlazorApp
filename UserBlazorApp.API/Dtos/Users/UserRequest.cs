using UserBlazorApp.API.Dtos.Roles;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Dtos.Users;

public class UserRequest
{
	public string? UserName { get; set; }

	public string? Email { get; set; }

	public string? PasswordHash { get; set; }

	public string? PhoneNumber { get; set; }

	public DateTimeOffset? LockoutEnd { get; set; }

	public List<RoleRequest> Role { get; set; } = new List<RoleRequest>();

	//public List<int> RoleIds { get; set; } = new List<int>();
}
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Dtos.Claims;

public class UserRoleRequest
{
	public int UserId { get; set; }
	public int RoleId { get; set; }
}
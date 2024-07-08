namespace UserBlazorApp.API.Dtos.Claims;

public class RoleClaimResponse
{
	public int Id { get; set; }
	public int RoleId { get; set; }
	public string? ClaimType { get; set; }
	public string? ClaimValue { get; set; }
}
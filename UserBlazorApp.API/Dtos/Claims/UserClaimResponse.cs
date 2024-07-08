namespace UserBlazorApp.API.Dtos.Claims;

public class UserClaimResponse
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string? ClaimType { get; set; }
	public string? ClaimValue { get; set; }
}

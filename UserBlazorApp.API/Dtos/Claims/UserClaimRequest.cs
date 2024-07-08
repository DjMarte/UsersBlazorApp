namespace UserBlazorApp.API.Dtos.Claims;

public class UserClaimRequest
{
	public int UserId { get; set; }
	public string? ClaimType { get; set; }
	public string? ClaimValue { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebService.Models.ViewModels;

public class LoginModel
{
	[Required]
	public string Name { get; init; }

	[Required]
	[UIHint("password")]
	public string Password { get; init; }

	public string ReturnUrl { get; init; } = "/";
}

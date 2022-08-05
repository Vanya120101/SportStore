using Microsoft.AspNetCore.Mvc;

namespace SportsStore.WebService.Models.ViewModels;
public class CartIndexViewModel
{
	public Cart Cart { get; init; }
	public string ReturnUrl { get; init; }
}

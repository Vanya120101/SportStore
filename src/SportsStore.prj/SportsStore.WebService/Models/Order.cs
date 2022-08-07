using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.WebService.Models;

public class Order
{
	[BindNever]
	public int Id { get; init; }

	[BindNever]
	public ICollection<CartLine> Lines { get; set; }

	[BindNever]
	public bool Shipped { get; set; }

	[Required(ErrorMessage = "Please enter a name")]
	public string Name { get; init; }

	[Required(ErrorMessage = "Please enter the first address line")]
	public string Line1 { get; init; }

	public string Line2 { get; init; }

	public string Line3 { get; init; }

	[Required(ErrorMessage = "Please enter a city name")]
	public string City { get; init; }

	[Required(ErrorMessage = "Please enter a state name")]
	public string State { get; init; }

	public string Zip { get; init; }

	[Required(ErrorMessage = "Please enter a country name")]
	public string Country { get; init; }

	public bool GiftWrap { get; init; }

	
}

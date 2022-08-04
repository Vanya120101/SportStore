using System.Collections;
using System.Collections.Generic;

namespace SportsStore.WebService.Models.ViewModels;

public class ProductsListViewModel
{
	public IEnumerable<Product> Products { get; init; }
	public PagingInfo PagingInfo { get; init; }
	public string CurrentCaterogy { get; init; }
}

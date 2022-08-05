namespace SportsStore.WebService.Models;

public class CartLine
{
	public int Id { get; init; }
	public Product Product { get; init; }
	public int Quantity { get; set; }

}

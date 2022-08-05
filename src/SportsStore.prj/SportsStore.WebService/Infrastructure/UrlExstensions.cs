using Microsoft.AspNetCore.Http;

namespace SportsStore.WebService.Infrastructure;

public static class UrlExstensions
{
	public static string PathAndQuery(this HttpRequest httpRequest)
	{
		return httpRequest.QueryString.HasValue ? $"{httpRequest.Path}{httpRequest.QueryString}"
												: httpRequest.Path.ToString();
	}
}

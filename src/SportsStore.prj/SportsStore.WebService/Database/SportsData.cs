using System;
using System.Collections.Generic;

namespace SportsStore.WebService.Database;

public static class SportsData
{
	public static List<string> SportItems => new()
	{
"tennis",
   "tennis racket",
   "tennis ball",
   "court",
   "net",
"football",
   "football",
   "helmet",
"soccer",
   "soccer ball",
   "goal",
"baseball",
   "baseball",
   "bat",
   "glove",
   "base",
   "cap",
"bowling",
   "bowling ball",
   "pin",
   "lane",
"golf",
   "golf ball",
   "golf clubs",
   "tee",
   "hole",
"skiing",
   "skis",
   "ski poles",
   "boots",
   "goggles",
"swimming",
   "swimming suit",
   "goggles",
   "swimming pool",
"volleyball",
   "volleyball",
   "net",
   "court",
"horseback riding",
   "saddle",
"weight lifting",
   "weights",
"running",
   "tennis shoes",
   "track",
"cycling",
   "bicycle (bike)",
   "helmet",
"skating",
   "skates",
"hockey",
   "skates",
   "stick",
   "puck",
   "ice rink",
"hiking",
   "hiking boots",
   "trail",
	};

	public static List<string> Categories => new ()
	{
		"tennis", "bowling", "swimming", "cycling", "football", "gold", "volleyball", "skating",
		"soccer", "skiing", "horseback riding", "hockey", "baseball", "skiing", "weight lifting",
		"hiking", "running"
	};

	private static readonly Random _random = new Random();
	public static string GetRandomCategory()
	{
		var index = _random.Next(0, Categories.Count);
		return Categories[index];
	}

	public static string GetRandomItems()
	{
		var index = _random.Next(0, SportItems.Count);
		return SportItems[index];
	}

	public static decimal GetRandomPrice() => _random.Next(20, 500);
}

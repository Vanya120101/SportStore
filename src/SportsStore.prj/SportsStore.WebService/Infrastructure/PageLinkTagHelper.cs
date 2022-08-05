using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.WebService.Models.ViewModels;
using System.Collections.Generic;

namespace SportsStore.WebService.Infrastructure;

[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
	private readonly IUrlHelperFactory _urlHelperFactory;

	[ViewContext]
	[HtmlAttributeNotBound]
	public ViewContext ViewContext { get; set; }

	public PagingInfo PagingInfo { get; set; }
	public string Category { get; set; }
	public string PageAction { get; set; }

	public bool PageClassesEnabled { get; set; } = false;
	public string PageClass { get; set; }
	public string PageClassNormal { get; set; }
	public string PageClassSelected { get; set; }

	public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
	{
		_urlHelperFactory = urlHelperFactory ?? throw new System.ArgumentNullException(nameof(urlHelperFactory));
	}

	public override void Process(TagHelperContext context, TagHelperOutput output)
	{
		var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
		var result = new TagBuilder("div");
		for(var i = 1; i <= PagingInfo.TotalPages; i++)
		{
			var tag = new TagBuilder("a");
			tag.Attributes["href"] = urlHelper.Action(new UrlActionContext() { Action = PageAction, Values = new { productPage = i, category = Category } });
			if(PageClassesEnabled)
			{
				tag.AddCssClass(PageClass);
				tag.AddCssClass(i == PagingInfo.CurrentPage ? PageClassSelected : PageClassNormal);
			}

			tag.InnerHtml.Append(i.ToString());
			result.InnerHtml.AppendHtml(tag);
		}

		output.Content.AppendHtml(result.InnerHtml);
	}
}

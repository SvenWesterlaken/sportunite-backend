using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using SportUnite.UI.Models.ViewModels;

namespace SportUnite.UI.ViewComponents
{
    public class PageListViewComponent : ViewComponent{

		public IViewComponentResult Invoke(IPagedList list, string c)
		{
			var model = new PageListModel() {Controller = c, PagedList = list};
			return View(model);
		}
	}
}

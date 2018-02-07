using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList.Core;

namespace SportUnite.UI.ViewModels
{
    public class PageListModel
    {
	    public IPagedList PagedList { get; set; }
	    public string Controller { get; set; }
    }
}

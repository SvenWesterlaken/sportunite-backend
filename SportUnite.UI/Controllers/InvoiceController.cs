using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportUnite.UI.Controllers
{
	[Authorize]
	public class InvoiceController : Controller
    {
	    public ActionResult Index()
	    {
		    return View();
	    }

	    public ActionResult Add()
	    {
		    return View("Error");
	    }

	    public ActionResult Update()
	    {
		    return View("Error");
	    }

	    public ActionResult Read()
	    {
		    return View("Error");
	    }

	    public ActionResult Delete()
	    {
		    return View("Error");
	    }
    }
}
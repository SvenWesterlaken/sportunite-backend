﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SportUnite.UI.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
	    public ActionResult Index()
	    {
		    return View();
	    }
    }
}

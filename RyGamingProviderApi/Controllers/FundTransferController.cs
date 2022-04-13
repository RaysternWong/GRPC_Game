using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyGamingProviderApi.Controllers
{
    public class FundTransferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

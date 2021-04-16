using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.TestApi;

namespace WebStore.Controllers
{
    public class ValuesController : Controller
    {
        private readonly IValueService _ValuesService;

        public ValuesController(IValueService ValuesService)
        {
            _ValuesService = ValuesService;
        }
        public IActionResult Index()
        {
            var values = _ValuesService.Get();
            return View(values);
        }
    }
}

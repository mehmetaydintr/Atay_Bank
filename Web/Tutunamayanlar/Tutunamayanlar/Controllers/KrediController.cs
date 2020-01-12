using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tutunamayanlar.Controllers
{
    [Authorize]
    public class KrediController : Controller
    {
        // GET: Kredi
        [HttpGet]
        public ActionResult Kredi()
        {
            return View();
        }
    }
}
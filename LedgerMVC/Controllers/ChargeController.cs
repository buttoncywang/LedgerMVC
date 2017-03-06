using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LedgerMVC.Models;

namespace LedgerMVC.Controllers
{
    public class ChargeController : Controller
    {
        // GET: Charge
        public ActionResult ChargeMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChargeMoney(ChargeItem chargeItem)
        {
            return View();
        }
    }
}
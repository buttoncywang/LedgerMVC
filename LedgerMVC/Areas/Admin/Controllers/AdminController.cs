using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LedgerMVC.Models;
using MvcPaging;
using LedgerMVC.Repository;

namespace LedgerMVC.Areas.Admin.Controllers
{
    [Authorize(Roles= "Admin_User")]
    public class AdminController : Controller
    {
        private ChargeService chargeService;
        private const int pageSize = 10;
        public AdminController()
        {
            var unitOfWork = new EFUnitOfWork();
            chargeService = new ChargeService(unitOfWork);
        }

        // GET: Admin/Admin
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(this.ShowPageRecords(pageIndex));
        }

        [HttpPut]
        public ActionResult Index(ChargeItem updateChargeItem)
        {
            return null;
        }

        [HttpDelete]
        public ActionResult Index(Guid id)
        {
            return null;
        }

        private IPagedList<ChargeItem> ShowPageRecords(int currentPageIndex)
        {
            var chargeList = chargeService.ShowRecordsWithPagination(currentPageIndex);
            return chargeList.ToPagedList(currentPageIndex,pageSize);
        }

    }
}
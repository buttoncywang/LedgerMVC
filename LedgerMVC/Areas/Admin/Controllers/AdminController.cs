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

        private IPagedList<ChargeItem> ShowPageRecords(int currentPageIndex)
        {
            var chargeList = chargeService.ShowRecordsWithPagination(currentPageIndex);
            return chargeList.ToPagedList(currentPageIndex,pageSize);
        }

    }
}
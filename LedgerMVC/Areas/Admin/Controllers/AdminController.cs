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
        private const int PageSize = 10;
        public AdminController()
        {
            var unitOfWork = new EFUnitOfWork();
            chargeService = new ChargeService(unitOfWork);
        }

        // GET: Admin/Admin
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(ShowPageRecords(pageIndex));
        }

        //Show Single Record for update
        public ActionResult EditRecord(Guid? id)
        {
            var unUpdateRecord = id.HasValue ? chargeService.GetSingleRecord(id.Value):null;
            if (unUpdateRecord == null)
                return HttpNotFound();
            else
            {
                return PartialView("_EditRecord",MappingModel(unUpdateRecord));
            }
        }

        [HttpPost]
        public ActionResult EditRecord(ChargeItem updateItem)
        {
            if (ModelState.IsValid)
            {
                chargeService.UpdateRecord(updateItem, updateItem.RecordGuid);
                chargeService.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(updateItem);
        }


        public ActionResult DeleteRecord(Guid? id)
        {
            var unDeleteRecord = id.HasValue? chargeService.GetSingleRecord(id.Value):null;
            if (unDeleteRecord != null)
                return PartialView("_DeleteRecord",MappingModel(unDeleteRecord));
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult DeleteConfirm(Guid? id)
        {
            var unDeleteRecord = id.HasValue? chargeService.GetSingleRecord(id.Value):null;
            if (unDeleteRecord != null)
            {
                chargeService.DeleteRecord(unDeleteRecord.Id);
                chargeService.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(MappingModel(unDeleteRecord));
            }
        }

        private IPagedList<ChargeItem> ShowPageRecords(int currentPageIndex)
        {
            var chargeList = chargeService.ShowRecordsWithPagination(currentPageIndex);
            return chargeList.ToPagedList(currentPageIndex,PageSize);
        }


        private ChargeItem MappingModel(AccountBook accountBook)
        {
            var chargeItem = new ChargeItem()
            {
                ChargeDate = accountBook.Dateee,
                ChargePrice = accountBook.Amounttt,
                ChargeType = accountBook.Categoryyy == 0 ? "支出" : "收入",
                RecordGuid = accountBook.Id,
                Memo = accountBook.Remarkkk
            };
            return chargeItem;
        }
    }
}
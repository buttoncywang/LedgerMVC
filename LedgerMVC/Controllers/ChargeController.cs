using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LedgerMVC.Models;
using LedgerMVC.Repository;
using MvcPaging;

namespace LedgerMVC.Controllers
{
    public class ChargeController : Controller
    {
        private const int DefaultPageSize = 10;
        private ChargeService chargeService;
        public ChargeController()
        {
            var unitOfWork = new EFUnitOfWork();
            chargeService = new ChargeService(unitOfWork);
        }

        public ActionResult Index(int? page)
        {
            return View(this.ShowPagedRecords(page));
        }


        public ActionResult ChargeMoney(int? page)
        {
            return View(ShowPagedRecords(page));
        }

        /*回傳分頁的資訊(JSON)*/
        public ActionResult ShowPagedChargeRecords(int currentPageIndex)
        {
            return Json(ShowPagedRecords(currentPageIndex), JsonRequestBehavior.AllowGet);
        }

        /*取得分頁的內容*/
        private IPagedList<ChargeItem> ShowPagedRecords(int? currentPageIndex)
        {
            int pageIndex = currentPageIndex.HasValue ? currentPageIndex.Value - 1 : 0;
            var chargeList = chargeService.ShowRecordsWithPagination(pageIndex);
            return chargeList.ToPagedList(pageIndex, DefaultPageSize);
        }

        
        public ActionResult AddCharge()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCharge(ChargeItem chargeItem)
        {
            if (ModelState.IsValid)
            {
                if (chargeItem.ChargeDate <= DateTime.Now)
                {
                    chargeService.AddNewAccountRecord(chargeItem);
                    //2017/03/24 將Commit改為在action中呼叫
                    chargeService.SaveChanges();
                    return RedirectToAction("AddCharge");

                }
                else
                    return View(chargeItem);
            }
            else
            {
                return View(chargeItem);
            }
        }
    }
}
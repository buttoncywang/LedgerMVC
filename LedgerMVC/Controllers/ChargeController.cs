﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LedgerMVC.Models;
using LedgerMVC.Repository;

namespace LedgerMVC.Controllers
{
    public class ChargeController : Controller
    {

        private ChargeService chargeService;

        public ChargeController()
        {
            var unitOfWork = new EFUnitOfWork();
            chargeService = new ChargeService(unitOfWork);
        }

        public ActionResult Index()
        {
            return View(ShowPagedRecords(1));
        }
        /*回傳分頁的資訊(JSON)*/
        public ActionResult ShowPagedChargeRecords(int currentPageIndex)
        {
            return Json(ShowPagedRecords(currentPageIndex), JsonRequestBehavior.AllowGet);
        }

        /*取得分頁的內容*/
        private ChargePaginViewModel ShowPagedRecords(int currentPageIndex)
        {
            var chargeList = chargeService.ShowRecordsWithPagination(currentPageIndex);
            return chargeList;
        }

        public ActionResult AddCharge()
        {
            return View();
        }

        [ChildActionOnly]
        [HttpPost]
        public ActionResult AddCharge(ChargeItem chargeItem)
        {
            if (ModelState.IsValid)
            {
                if (chargeItem.ChargeDate <= DateTime.Now)
                {
                    chargeService.AddNewAccountRecord(chargeItem);
                    return View("AddCharge");
                    //return Content("新增成功");
                }
                else
                    return Content("日期不得大於今日");
            }

            return View();
        }
    }
}
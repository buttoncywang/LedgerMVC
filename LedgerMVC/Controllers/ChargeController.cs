using System;
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
            /*2017/03/09 改用Parital View的方式呈現(原本使用Child Action)*/
            //var ChargeList = this.GetChargeItems();
            //return View(ChargeList);
            return View(this.ShowPagedRecords(1));
        }
        /*回傳分頁的資訊(JSON)*/
        public ActionResult ShowPagedChargeRecords(int currentPageIndex)
        {
            return Json(this.ShowPagedRecords(currentPageIndex), JsonRequestBehavior.AllowGet);
        }

        /*取得分頁的內容*/
        private ChargePaginViewModel ShowPagedRecords(int currentPageIndex)
        {
            var ChargeList = chargeService.ShowRecordsWithPagination(currentPageIndex);
            return ChargeList;
        }

        //[HttpGet]
        //public ActionResult ChargeMoney()
        //{
        //    var ChargeList = this.GetChargeItems();   
        //    return View(ChargeList);
        //}

        [HttpPost]
        public ActionResult ChargeMoney(ChargeItem chargeItem)
        {
            return View();
        }


        //private IEnumerable<ChargeItem> GetChargeItems()
        //{

        //    DateTime dt=DateTime.Now;
        //    var ChargeItem1 = new ChargeItem
        //    {
        //        ChargeRecordId = 1,
        //        ChargeDate = dt,
        //        ChargeType = "支出",
        //        ChargePrice = 1000,
        //        Memo = ""
        //    };

        //    var ChargetItem2 = new ChargeItem
        //    {
        //        ChargeRecordId = 2,
        //        ChargeDate = dt.AddDays(-1),
        //        ChargeType = "收入",
        //        ChargePrice = 40000,
        //        Memo = "打工收入"
        //    };

        //    var ChargetItem3 = new ChargeItem
        //    {
        //        ChargeRecordId = 3,
        //        ChargeDate = dt.AddDays(-10),
        //        ChargeType = "收入",
        //        ChargePrice = 100000,
        //        Memo = "打工收入"
        //    };

        //    List<ChargeItem> ChargeList = new List<ChargeItem>
        //    {
        //        ChargeItem1,
        //        ChargetItem2,
        //        ChargetItem3
        //    };

        //    return ChargeList;
        //}
    }
}
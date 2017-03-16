using LedgerMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerMVC.Models
{
    public class ChargeService
    {
        private IRepository<AccountBook> _accountBookRep;
        private IUnitOfWork _unitWork;

        //private ModelCharge _db;

        //public ChargeService() {
        //    _db = new ModelCharge();
        //}

        public ChargeService(IUnitOfWork UnitOfWork)
        {
            _unitWork = UnitOfWork;
            _accountBookRep = new Repository<AccountBook>(UnitOfWork);
        }

        /*顯示全部資料(無分頁)*/
        public IEnumerable<AccountBook> ShowAllRecords()
        {
            return _accountBookRep.ShowAllRecords();
        }

        /*依類型搜尋*/
        public IEnumerable<AccountBook> ShowSearchedRecords(int category)
        {
            return _accountBookRep.ShowSearchedRecords(d => d.Categoryyy == category);
        }

        public AccountBook GetSingleRecord(Guid LedgerItemID)
        {
            return _accountBookRep.GetSingleRecord(d => d.Id == LedgerItemID);
        }

        public ChargePaginViewModel ShowRecordsWithPagination(int currentPage)
        {
            int maxRow = 50; //一頁50筆資料
            var chargePageViewModel = new ChargePaginViewModel();
            chargePageViewModel.AccountBooks = _accountBookRep.ShowPaginationRecords((currentPage - 1) * maxRow, maxRow, d => d.Dateee);
            //chargePageViewModel.AccountBooks= _db.AccountBook.OrderBy(d => d.Dateee).Skip((currentPage - 1) * maxRow).Take(maxRow).ToList();
            double PageCount = _accountBookRep.GetRowCount();
            PageCount = PageCount / maxRow;
            chargePageViewModel.TotalPages = (int)Math.Ceiling(PageCount);
            chargePageViewModel.CurrentPageIndex = currentPage;
            return chargePageViewModel;
        }
    }
}
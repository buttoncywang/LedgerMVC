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
        public IEnumerable<ChargeItem> ShowAllRecords()
        {
            var accountBookRecords= _accountBookRep.ShowAllRecords().ToList();
            return this.MappingModel(accountBookRecords);
        }

        /*依類型搜尋*/
        public IEnumerable<ChargeItem> ShowSearchedRecords(int category)
        {
            var accountBookRecordsBySearch = _accountBookRep.ShowSearchedRecords(d => d.Categoryyy == category).ToList();
            return this.MappingModel(accountBookRecordsBySearch); ;
        }


        private List<ChargeItem> MappingModel(List<AccountBook> dbRecords) {
            var modelRecords = new List<ChargeItem>();
            int sn = 0;
            foreach (var accountBookRecord in dbRecords)
            {
                var chargeItem = new ChargeItem();
                chargeItem.ChargeDate = accountBookRecord.Dateee;
                chargeItem.ChargePrice = accountBookRecord.Amounttt;
                if (accountBookRecord.Categoryyy == 0)
                    chargeItem.ChargeType = "支出";
                else
                    chargeItem.ChargeType = "收入";
                chargeItem.Memo = accountBookRecord.Remarkkk;
                chargeItem.ChargeRecordId = ++sn;
                modelRecords.Add(chargeItem);
            }
            return modelRecords;
        }

        public AccountBook GetSingleRecord(Guid LedgerItemID)
        {
            return _accountBookRep.GetSingleRecord(d => d.Id == LedgerItemID);
        }

        public ChargePaginViewModel ShowRecordsWithPagination(int currentPage)
        {
            int maxRow = 50; //一頁50筆資料
            var chargePageViewModel = new ChargePaginViewModel();
            var accountBookRecords= _accountBookRep.ShowPaginationRecords((currentPage - 1) * maxRow, maxRow, d => d.Dateee).ToList();
            chargePageViewModel.ChargeItems = this.MappingModel(accountBookRecords);
            //chargePageViewModel.AccountBooks= _db.AccountBook.OrderBy(d => d.Dateee).Skip((currentPage - 1) * maxRow).Take(maxRow).ToList();
            double PageCount = _accountBookRep.GetRowCount();
            PageCount = PageCount / maxRow;
            chargePageViewModel.TotalPages = (int)Math.Ceiling(PageCount);
            chargePageViewModel.CurrentPageIndex = currentPage;
            return chargePageViewModel;
        }
    }
}
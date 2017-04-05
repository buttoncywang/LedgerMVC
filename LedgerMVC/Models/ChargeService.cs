using LedgerMVC.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MvcPaging;

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

        public List<ChargeItem> ShowRecordsWithPagination(int currentPage)
        {
            var accountBookRecords = _accountBookRep.ShowPaginationRecords(d => d.Dateee).ToList();
            var ChargeItems = this.MappingModel(accountBookRecords);
            return ChargeItems;
        }

        public void AddNewAccountRecord(ChargeItem chargeRecord)
        {
            var accountBookRecord = new AccountBook()
            {
                Id = Guid.NewGuid(),
                Amounttt = chargeRecord.ChargePrice,
                Categoryyy = int.Parse(chargeRecord.ChargeType),
                Dateee = chargeRecord.ChargeDate,
                Remarkkk = chargeRecord.Memo
            };
            _accountBookRep.CreateRecord(accountBookRecord);
        }

        public void SaveChanges()
        {
            _accountBookRep.Commit();
        }
    }
}
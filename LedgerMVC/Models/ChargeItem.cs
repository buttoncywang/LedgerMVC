using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerMVC.Models
{
    public class ChargeItem
    {
        public string ChargeType { get; set; }
        public int ChargePrice { get; set; }
        public string Memo { get; set; }

        public DateTime ChargeDate { get; set; }

        public int ChargeRecordId { get; set; }
    }
}
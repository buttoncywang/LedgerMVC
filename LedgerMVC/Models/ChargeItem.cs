using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LedgerMVC.Models
{
    public class ChargeItem
    {
        [Display(Name = "類型")]
        [Required(ErrorMessage = "必需選擇記帳類型")]
        public string ChargeType { get; set; }
        [Display(Name = "金額")]
        [Required(ErrorMessage = "金額必需大於0元")]
        [Range(1, int.MaxValue, ErrorMessage = "金額必需大於0元")]
        public int ChargePrice { get; set; }
        [Required(ErrorMessage = "必需填寫備註")]
        [Display(Name = "備註")]
        [StringLength(100, MinimumLength = 1)]
        public string Memo { get; set; }
        [Display(Name = "日期")]
        [Required(ErrorMessage = "請選擇日期")]
        public DateTime ChargeDate { get; set; }

        public int? ChargeRecordId { get; set; }
        public Guid RecordGuid { get; set; }
    }
}
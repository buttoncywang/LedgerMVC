﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerMVC.Models
{
    public class ChargePaginViewModel
    {
        public IEnumerable<ChargeItem> ChargeItems { get; set; }
        public int CurrentPageIndex { get; set; }
        public int TotalPages { get; set; }
    }
}
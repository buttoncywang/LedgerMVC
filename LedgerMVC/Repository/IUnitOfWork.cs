using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedgerMVC.Repository
{
    public interface IUnitOfWork
    {
        DbContext Context { get; set; }

        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LedgerMVC.Repository
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; set; }
        IQueryable<T> ShowAllRecords();
        IQueryable<T> ShowPaginationRecords(int skipRecords, int takeRecords, Expression<Func<T, DateTime>> orderBy);
        IQueryable<T> ShowSearchedRecords(Expression<Func<T, bool>> filter);
        T GetSingleRecord(Expression<Func<T, bool>> filter);
        int GetRowCount();
        void CreateRecord(T entity);
        void RemoveRecord(T entity);
        void Commit();
    }
}

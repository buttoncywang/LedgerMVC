using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace LedgerMVC.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public IUnitOfWork UnitOfWork { get; set; }

        private DbSet<T> _Objectset;
        private DbSet<T> ObjectSet
        {
            get
            {
                if (_Objectset == null)
                {
                    _Objectset = UnitOfWork.Context.Set<T>();
                }
                return _Objectset;
            }
        }

        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        void IRepository<T>.Commit()
        {
            UnitOfWork.Save();
        }

        void IRepository<T>.CreateRecord(T entity)
        {
            ObjectSet.Add(entity);
        }

        T IRepository<T>.GetSingleRecord(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.SingleOrDefault(filter);
        }

        void IRepository<T>.RemoveRecord(T entity)
        {
            ObjectSet.Remove(entity);
        }

        int IRepository<T>.GetRowCount()
        {
            return ObjectSet.Count();
        }

        IQueryable<T> IRepository<T>.ShowAllRecords()
        {
            return ObjectSet;
        }

        IQueryable<T> IRepository<T>.ShowPaginationRecords(int skipRecords, int takeRecords, Expression<Func<T, DateTime>> orderBy)
        {
            return ObjectSet.OrderByDescending(orderBy).Skip(skipRecords).Take(takeRecords);
        }

        IQueryable<T> IRepository<T>.ShowSearchedRecords(Expression<Func<T, bool>> filter)
        {
            return ObjectSet.Where(filter);
        }
    }
}
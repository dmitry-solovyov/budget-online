using System;
using BudgetOnline.Data.MSSQL.EF;

namespace BudgetOnline.Data.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private BudgetDatabase _context;

        private bool _disposed;

        protected BudgetDatabase DataContext
        {
            get { return _context ?? (_context = new BudgetDatabase()); }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

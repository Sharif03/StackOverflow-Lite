using System;
using StackOverflowLite.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowLite.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
		protected IAdoNetUtility AdoNetUtility { get; private set; }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext; 
            AdoNetUtility = new AdoNetUtility(_dbContext.Database.GetDbConnection());
		}

		public void Dispose() => _dbContext?.Dispose();
        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public void Save() => _dbContext?.SaveChanges();
        public virtual async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}

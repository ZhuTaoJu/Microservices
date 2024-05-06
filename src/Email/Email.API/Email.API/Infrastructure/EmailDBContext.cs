using System.Data;
using Email.API.Core;
using Email.API.Domain.AggregatesModel.EmailAggregate;
using Email.API.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Services.Core.BaseDbContext;

namespace Email.API.Infrastructure
{
    //public class EmailDBContext : DbContext, IUnitOfWork
    public class EmailDBContext : BaseDbContext
    { 
        public const string DEFAULT_SCHEMA = "email";
        public DbSet<EmailEntity> Emails { get; set; }

        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;

        public EmailDBContext(DbContextOptions<EmailDBContext> options) : base(options)
        {
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            //// Dispatch Domain Events collection. 
            //// Choices:
            //// A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            //// side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            //// B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            //// You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            
            //await _mediator.DispatchDomainEventsAsync(this);

            //// After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            //// performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmailEntityConfiguration());
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder
        //        .Properties<string>()
        //        .AreUnicode(false)
        //        .HaveMaxLength(500);
        //}

    }

    public class EmailContextDesignFactory : IDesignTimeDbContextFactory<EmailDBContext>
    {
        public EmailDBContext CreateDbContext(string[] args)
        {
            //var optionsBuilder = new DbContextOptionsBuilder<EmailContext>()
            //    .UseSqlServer("Server=.;Initial Catalog=Microsoft.eShopOnContainers.Services.CatalogDb;Integrated Security=true");

            var optionsBuilder = new DbContextOptionsBuilder<EmailDBContext>()
                .UseMySQL("Server=127.0.0.1;port=3306;database=zt.Services.EmailDB;uid=root;pwd=**********;");

            return new EmailDBContext(optionsBuilder.Options);
        }
    }
}

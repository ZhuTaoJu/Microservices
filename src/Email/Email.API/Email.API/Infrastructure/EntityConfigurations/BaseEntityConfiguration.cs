using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Services.Core;
using Email.API.Domain.AggregatesModel.BaseEntity;

namespace Email.API.Infrastructure.EntityConfigurations
{
    public abstract class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : ALLBase
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var entityType = typeof(TEntity);

            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.CreateUser).HasMaxLength(500).IsRequired(false);
            builder.Property(ci => ci.UpdateUser).HasMaxLength(500).IsRequired(false);

            if (typeof(ISoftDelete).IsAssignableFrom(entityType))
            {
                builder.HasQueryFilter(d => EF.Property<bool>(d, "IsDeleted") == false);
            }
            Configure1(builder);
        }

       public abstract void  Configure1(EntityTypeBuilder<TEntity> builder);
    }
}

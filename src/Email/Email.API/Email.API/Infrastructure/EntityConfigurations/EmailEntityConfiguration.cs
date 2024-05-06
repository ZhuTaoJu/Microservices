using Email.API.Domain.AggregatesModel.EmailAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Email.API.Infrastructure.EntityConfigurations
{
    public class EmailEntityConfiguration : BaseEntityConfiguration<EmailEntity, long>
    {
        public override void Configure1(EntityTypeBuilder<EmailEntity> builder)  
        {
            builder.ToTable("Email");
            builder.Property(ci => ci.Remark).IsRequired(false);

            string[] names = new string[] { "CallName", "Email", "Phone", "Description" }; 

            foreach (string name in names) {
                builder.Property(name).HasMaxLength(500).IsRequired(false);
            }            
        }


    }
}

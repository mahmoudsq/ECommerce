using ECommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Data.Config
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)/*.ValueGeneratedOnAdd()*/;
            builder.Property(x => x.Name).IsRequired();
           /* builder.Property(x => x.OldPrice).IsRequired()
                .HasColumnType("decimal(18,2)");*/
            builder.Property(x => x.NewPrice).IsRequired()
                .HasColumnType("decimal(18,2)");
            //builder.HasOne(x=>x.)

        }
    }
}

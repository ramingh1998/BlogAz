using BlogAz.Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAz.Infrastructure.EntityConfigurations.Blogs
{
    public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.ToTable("BlogCategories");
            builder.HasKey(q => q.Id);
            builder.Property(q => q.CreatedAt).HasColumnType("datetime");
            builder.Property(q => q.UpdatedAt).HasColumnType("datetime");
            builder.HasOne(q => q.Category).WithMany(q => q.BlogCategories).HasForeignKey(q => q.CategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Blog).WithMany(q => q.BlogCategories).HasForeignKey(q => q.BlogId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

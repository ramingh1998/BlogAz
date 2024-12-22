using BlogAz.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAz.Infrastructure.EntityConfigurations.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Password).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.FirstName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.LastName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.UserName).IsRequired().HasColumnType("nvarchar(100)");
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired();
            builder.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

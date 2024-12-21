using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogAz.Domain.Entities.Admins;
using BlogAz.Domain.Entities.Blogs;
using BlogAz.Domain.Entities.Categories;
using BlogAz.Domain.Entities.Users;
using Common.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlogAz.Infrastructure.Persistence
{
    public class AppDbContext : BaseEfContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator) : base(options, mediator)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

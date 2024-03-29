﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        entry.Entity.CreatedUser = "Customer";
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateUser = "Customer";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateUser = "Customer";
                        break;
                    case EntityState.Deleted:
                        entry.Entity.DeleteDate = DateTime.Now;
                        entry.Entity.DeleteUser = "Customer";
                        entry.Entity.UpdateDate = DateTime.Now;
                        entry.Entity.UpdateUser = "Customer";
                        entry.Entity.IsDeleted = true;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

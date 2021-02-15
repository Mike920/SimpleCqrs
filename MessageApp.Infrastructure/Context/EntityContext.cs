using MessageApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MessageApp.Infrastructure.Context
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
            .HasData(
                new Contact(1, "Initial user"),
                new Contact(2, "John Smith")
            );

            modelBuilder.Entity<Message>()
            .HasData(
                new Message(1, "message 1",1, 2),
                new Message(2, "message 2",1 ,2)
            );
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}

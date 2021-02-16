using MessageApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Infrastructure.Tests.Infrastructure
{
    public class BaseTest : IDisposable
    {
        protected readonly EntityContext Context;

        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<EntityContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new EntityContext(options);

            Context.Database.EnsureCreated();

            DbInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}

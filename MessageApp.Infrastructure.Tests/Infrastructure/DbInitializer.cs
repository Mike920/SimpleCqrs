using MessageApp.Domain.Entities;
using MessageApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageApp.Infrastructure.Tests.Infrastructure
{
    public class DbInitializer
    {
        public static void Initialize(EntityContext context)
        {
            if (context.Messages.Any())
                context.RemoveRange(context.Messages);          

            Seed(context);
        }

        private static void Seed(EntityContext context)
        {
            var messages = new[]
            {
                new Message(1, "abc",1, 2),
                new Message(2, "cb",1 ,2),
                new Message(3, "zza",1 ,2),
                new Message(4, "gg",1 ,2),
                new Message(5, "gga",1 ,2)
            };

            context.Messages.AddRange(messages);
            context.SaveChanges();
        }
    }
}

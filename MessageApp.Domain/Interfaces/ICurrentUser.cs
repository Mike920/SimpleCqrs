using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Domain.Interfaces
{
    public interface ICurrentUser
    {
        public int UserId { get;  }
    }
}

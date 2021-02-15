using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageApp.Domain.Mocks
{
    public class CurrentUserMock : ICurrentUser
    {
        public int UserId => 1;
    }
}

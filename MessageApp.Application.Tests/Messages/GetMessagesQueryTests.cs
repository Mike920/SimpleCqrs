using FluentAssertions;
using MessageApp.Application.Messages;
using MessageApp.Domain.Entities;
using MessageApp.Domain.Interfaces;
using MessageApp.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace MessageApp.Application.Tests.Messages
{
    public class GetMessagesQueryTests
    {
        Moq.Mock<IMessageRepository> _repositoryMock;
        Moq.Mock<ICurrentUser> _currentUserMock;
        GetMessagesQueryHandler _query;
        public GetMessagesQueryTests()
        {
            _repositoryMock = new Moq.Mock<IMessageRepository>();
            _currentUserMock = new Moq.Mock<ICurrentUser>();
            _query = new GetMessagesQueryHandler(_repositoryMock.Object, _currentUserMock.Object);

        }

        [Fact]
        public async void ShouldReturnPaginatedResultAfterSuccess()
        {
            _repositoryMock.Setup(x => x.Query(1, "content", 1, 1, "Content", true, 1, false))
                .ReturnsAsync(new PaginatedList<Message>(new List<Message>(),1,1,1));

            var commandResult = await _query.Handle(new GetMessagesQuery(1,"content",1,1,"Content",true,1,false), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.OK);
            commandResult.Content.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldReturn422HttpCodeIfModelIsInvalid()
        {
            var commandResult = await _query.Handle(new GetMessagesQuery(1, "content", 1, 1, "column", true, 1, false), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);
            commandResult.Content.Should().Be(null);
        }
    }
}

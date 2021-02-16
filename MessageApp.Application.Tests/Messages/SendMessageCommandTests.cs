using FluentAssertions;
using MessageApp.Application.Messages;
using MessageApp.Domain.Entities;
using MessageApp.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace MessageApp.Application.Tests.Messages
{
    public class SendMessageCommandTests
    {
        Moq.Mock<IMessageRepository> _repositoryMock;
        Moq.Mock<ICurrentUser> _currentUserMock;
        SendMessageCommandHandler _command;
        public SendMessageCommandTests()
        {
            _repositoryMock = new Moq.Mock<IMessageRepository>();
            _currentUserMock = new Moq.Mock<ICurrentUser>();
            _command = new SendMessageCommandHandler(_repositoryMock.Object, _currentUserMock.Object);

        }

        [Fact]
        public async void ShouldReturnMessageIdAfterSuccess()
        {
            _repositoryMock.Setup(x => x.Create(It.IsAny<Message>())).ReturnsAsync(1);

            var commandResult = await _command.Handle(new SendMessageCommand("content", 1), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.OK);
            commandResult.Content.Should().BeGreaterThan(0);
        }

        [Fact]
        public async void ShouldReturn422HttpCodeIfModelIsInvalid()
        {
            var commandResult = await _command.Handle(new SendMessageCommand(null, 1), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.UnprocessableEntity);
            commandResult.Content.Should().Be(null);
        }
    }
}

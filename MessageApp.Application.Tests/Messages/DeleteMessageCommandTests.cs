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
    public class DeleteMessageCommandTests
    {
        Moq.Mock<IMessageRepository> _repositoryMock;
        DeleteMessageCommandHandler _command;
        public DeleteMessageCommandTests()
        {
            _repositoryMock = new Moq.Mock<IMessageRepository>();
            _command = new DeleteMessageCommandHandler(_repositoryMock.Object);

        }

        [Fact]
        public async void ShouldReturn204HttpCodeAfterSuccess()
        {
            _repositoryMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new Message(1,"",1,1));

            var commandResult = await _command.Handle(new DeleteMessageCommand(1), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.NoContent);
            commandResult.Content.Should().BeNull();
        }

        [Fact]
        public async void ShouldReturn404HttpCodeIfMessageWasntFound()
        {
            _repositoryMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((Message)null);

            var commandResult = await _command.Handle(new DeleteMessageCommand(0), default);

            commandResult.Status.Should().Be((int)HttpStatusCode.NotFound);
            commandResult.Content.Should().BeNull();
        }
    }
}

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
    public class MarkMessageAsReadCommandTests
    {
        Moq.Mock<IMessageRepository> _repositoryMock;
        MarkMessageAsReadCommandHandler _command;
        public MarkMessageAsReadCommandTests()
        {
            _repositoryMock = new Moq.Mock<IMessageRepository>();
            _command = new MarkMessageAsReadCommandHandler(_repositoryMock.Object);

        }

        [Fact]
        public async void ShouldReturn204HttpCodeAfterSuccess()
        {
            _repositoryMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(new Message(1,null,1,1));
            //_repositoryMock.Setup(x => x.Update(It.IsAny<Message>())).ReturnsAsync(1);

            var commandResult = await _command.Handle(new MarkMessageAsReadCommand(1), default);

            commandResult.Status.Should().Be(HttpStatusCode.NoContent);
            commandResult.Content.Should().Be(null);
        }

        [Fact]
        public async void ShouldReturn404HttpCodeIfMessageDoesntExist()
        {
            _repositoryMock.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync((Message)null);

            var commandResult = await _command.Handle(new MarkMessageAsReadCommand(0), default);

            commandResult.Status.Should().Be(HttpStatusCode.NotFound);
            commandResult.Content.Should().Be(null);
        }
    }
}

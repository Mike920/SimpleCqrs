using MessageApp.Infrastructure.Repositiories;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MessageApp.Infrastructure.Context;
using MessageApp.Infrastructure.Tests.Infrastructure;
using Xunit;
using MessageApp.Domain.Entities;
using FluentAssertions;
using System.Linq;

namespace MessageApp.Infrastructure.Tests
{
    public class MessageRepositoryTests : BaseTest
    {
        MessageRepository _messageRepository;
        public MessageRepositoryTests()
        {
            _messageRepository = new MessageRepository(Context);
        }

        [Fact]
        public async void ShouldReturnPaginatedResult()
        {
            var paginatedResult = await _messageRepository.Query(null, "a", 1, 2, nameof(Message.Content),true,null,null);
            var resultMessageIds = new[] { 3, 5 };

            paginatedResult.Should().NotBeNull();
            paginatedResult.Items.Count.Should().Be(2);

        }

        [Fact]
        public async void ShouldReturnPaginatedResultWithAppliedFilters()
        {
            var paginatedResult = await _messageRepository.Query(null, "a", 1, 2, nameof(Message.Content), true, null, null);
            var resultMessageIds = new[] { 3, 5 };

            paginatedResult.Items.Select(x => x.Id).Should().BeEquivalentTo(resultMessageIds, options => options.WithStrictOrdering());

        }
    }
}

using MediatR;
using MessageApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MessageApp.Domain.Entities;
using MessageApp.Application.Models;

namespace MessageApp.Application.Messages
{
    public class DeleteMessageCommand : IRequest<Result<object>>
    {
        public DeleteMessageCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Result<object>>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }
        public async Task<Result<object>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            if((await _messageRepository.Get(request.Id)) == null)
                return Result.NotFound<object>(null);

            await _messageRepository.Delete(request.Id);

            return Result.NoContent<object>(null);
        }
    }
}

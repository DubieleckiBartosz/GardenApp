﻿namespace Panels.Application.Handlers.Commands;

public sealed class AddProjectImageHandler : ICommandHandler<AddProjectImageCommand, Response>
{
    public record AddProjectImageCommand() : ICommand<Response>;
    private readonly IContractorRepository _contractorRepository;

    public AddProjectImageHandler(IContractorRepository contractorRepository)
    {
        _contractorRepository = contractorRepository;
    }

    public Task<Response> Handle(AddProjectImageCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
namespace BuildingBlocks.Application.Contracts.Mediator;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
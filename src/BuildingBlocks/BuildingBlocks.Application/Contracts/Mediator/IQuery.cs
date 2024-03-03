namespace BuildingBlocks.Application.Contracts.Mediator;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
namespace BuildingBlocks.Application.Contracts.Services;

public interface ICurrentUser
{
    bool IsAdmin { get; }

    bool IsInRole(string roleName);

    List<string>? AvailableRoles();

    string UserId { get; }
    string UserName { get; }
    string Email { get; }
}
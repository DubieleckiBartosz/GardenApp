namespace Users.Domain.Users.Exceptions;

internal class BusinessExistsException : BaseException
{
    public BusinessExistsException(string userId, string businessId)
        : base($"the user is already assigned to the business. [UserId: {userId}, BusinessId: {businessId}].")
    {
    }
}
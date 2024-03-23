namespace Users.Application.Helpers;

internal static class IdentityResultHelper
{
    public static List<IdentityErrorResponse> ReadResult(this IdentityResult result)
    {
        var errors = new List<IdentityErrorResponse>();
        foreach (var error in result.Errors)
        {
            errors.Add(new IdentityErrorResponse(error.Code, error.Description));
        }

        return errors;
    }

    public static IEnumerable<string> ReadErrors(this IdentityResult result) => result.Errors.Select(_ => _.Description);
}
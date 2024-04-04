namespace Works.Domain.WorkItems.Exceptions;

internal class RecordNotFoundException : BaseException
{
    public RecordNotFoundException(int recordId) : base($"Record not found [RecordId: {recordId}].")
    {
    }
}
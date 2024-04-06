namespace Works.Application.Helpers;

internal static class StreamHelper
{
    public static T? ReadAndDeserializeFromJson<T>(this Stream stream)
    {
        if (stream == null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (!stream.CanRead)
        {
            throw new NotSupportedException("Can't read from this stream.");
        }

        using var streamReader = new StreamReader(stream);

        using var jsonTextReader = new JsonTextReader(streamReader);

        var jsonSerializer = new JsonSerializer();
        return jsonSerializer.Deserialize<T>(jsonTextReader);
    }
}
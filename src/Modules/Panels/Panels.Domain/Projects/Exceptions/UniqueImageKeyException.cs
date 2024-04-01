﻿namespace Panels.Domain.Projects.Exceptions;

internal class UniqueImageKeyException : BaseException
{
    internal UniqueImageKeyException(string key) : base($"The image key must be unique. [Duplicate: {key}]")
    {
    }
}
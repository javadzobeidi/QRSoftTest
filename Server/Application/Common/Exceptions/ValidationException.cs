using FluentValidation.Results;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{


    public ValidationException(IReadOnlyDictionary<string, string> errorsDictionary)
          => ErrorsDictionary = errorsDictionary;

    public IReadOnlyDictionary<string, string> ErrorsDictionary { get; }


}

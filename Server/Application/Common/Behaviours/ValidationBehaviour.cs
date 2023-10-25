using Application.Common.Interfaces.Messaging;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Common.Behaviours;


public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var errorsDictionary = _validators
                   .Select(x => x.Validate(context))
                   .SelectMany(x => x.Errors)
                   .Where(x => x != null)
                   .GroupBy(
                       x => x.PropertyName,
                       x => x.ErrorMessage,
                       (propertyName, errorMessages) => new
                       {
                           Key = propertyName,
                           Values = errorMessages.Distinct().ToArray().FirstOrDefault()
                       })
                   .ToDictionary(x => x.Key, x => x.Values);


            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }

        }

        return await next();

    }


}

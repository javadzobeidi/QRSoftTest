using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Application;

public record UserLoginCommand :  ICommand<Guid>
{
    public string UserName { init; get; }
    public string Password { init; get; }
}



public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
{
    private readonly IApplicationDbContext _context;

    public UserLoginCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(v => v.UserName)
            .NotEmpty().WithMessage("UserName is Required");
            //.MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
                    RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required");

    }

   
}



public class UserLoginCommandHandler : ICommandHandler<UserLoginCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;

    public UserLoginCommandHandler(IApplicationDbContext context, IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<Guid> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {

   var user=    await
            _context.Users.Where(d => d.UserName.CompareTo(request.UserName) == 0).FirstOrDefaultAsync();
        if (user == null)
            throw new NotFoundException("User And Password is wrong");

        if (PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, 3).CompareTo(user.Password) != 0)
        {
            user.FailedCount += 1;
            if (user.FailedCount >= 10)
                user.UserLocked = true;
           await _context.SaveChangesAsync(cancellationToken);

            throw new NotFoundException("UserName and Password is wrong");
        }
        if (user.UserLocked)
            throw new RequestException("Current user is locked   ");

        var token = user.AddUserToken();
       await _context.SaveChangesAsync(cancellationToken);

        return token.UserTokenId;

    }
}

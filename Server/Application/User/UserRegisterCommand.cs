using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Application;

public record UserRegisterCommand :  ICommand<int>
{
    public string UserName { init; get; }
    public string Password { init; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }

    public string? Email { set; get; }
    public string? Job { set; get; }
    public Guid? RegisterId { set; get; }
    public string InvCode { set; get; }


}



public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
{
    private readonly IApplicationDbContext _context;

    public UserRegisterCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        
        RuleFor(v => v.UserName)
            .NotEmpty().WithMessage("UserName is Required");
            //.MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
                    RuleFor(v => v.Password)
            .NotEmpty().WithMessage("Password is required");

    }

   
}



public class UserRegisterCommandHandler : ICommandHandler<UserRegisterCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;
    private readonly ISender _sender;
    public UserRegisterCommandHandler(IApplicationDbContext context, IDateTime dateTime,ISender sender)
    {
        _context = context;
        _dateTime = dateTime;
        _sender = sender;
    }

    public async Task<int> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {


   var user=    await
  _context.Users.Where(d => d.UserName.CompareTo(request.UserName) == 0).FirstOrDefaultAsync();
        if (user != null)
            throw new NotFoundException("User Name is exist");

        User entity = new User
        {
            UserName = request.UserName,
          
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Job = request.Job,
            UserTypeId=(int)UserTypeEnum.Customer
        };
        entity.PasswordSalt = PasswordHasher.GenerateSalt();
        entity.Password = PasswordHasher.ComputeHash(request.Password, entity.PasswordSalt, 3);


        if (request.RegisterId.HasValue==true)
        {
          var registerRefferal= await _sender.Send(new ValidateReferallLinkQuery(request.RegisterId.Value, request.InvCode));
            registerRefferal.User = entity;
            entity.UserTypeId = (int)UserTypeEnum.Manager;
        }
        _context.Users.Add(entity);
       await _context.SaveChangesAsync(cancellationToken);

        return entity.UserId;


    }
}

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

public record GetUserByTokenQuery(Guid Id) :  IQuery<UserToken>;



public class GetUserByTokenQueryHandler : IQueryHandler<GetUserByTokenQuery, UserToken>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;

    public GetUserByTokenQueryHandler(IApplicationDbContext context, IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<UserToken> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
    {

       var userToken=await _context.UserTokens
            .Where(d => d.UserTokenId == request.Id).Include(d=>d.User).AsNoTracking().FirstOrDefaultAsync();

        return userToken;

    }
}

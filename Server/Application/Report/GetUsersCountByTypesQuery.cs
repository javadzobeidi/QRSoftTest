using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Messaging;
using Application.Contract;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Application;

public record GetUsersCountByTypesQuery() :  IQuery<List<UsersCountTypesResponse>>;



public class GetUsersCountByTypesQueryHandler : IQueryHandler<GetUsersCountByTypesQuery, List<UsersCountTypesResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;

    public GetUsersCountByTypesQueryHandler(IApplicationDbContext context, IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<List<UsersCountTypesResponse>> Handle(GetUsersCountByTypesQuery request, CancellationToken cancellationToken)
    {

        var list = await (from us in _context.Users
                           group us by us.UserTypeId into g
                           select new UsersCountTypesResponse
                           {
                               Role = ((UserTypeEnum)g.Key).GetEnumDescription(),
                               Count = g.Count(),

                           }).AsNoTracking().ToListAsync();

        return list;

    }
}

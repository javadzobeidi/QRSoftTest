using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Messaging;
using Domain.Entities;

namespace Application;


public sealed record ValidateReferallLinkQuery(Guid token,string invCode):IQuery<RegisterReferralLink>;

public class ValidateReferallLinkQueryHandler :IQueryHandler<ValidateReferallLinkQuery, RegisterReferralLink>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;
    public ValidateReferallLinkQueryHandler(  IApplicationDbContext context, IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<RegisterReferralLink> Handle(ValidateReferallLinkQuery request, CancellationToken cancellationToken)
    {
    var entity=   await _context.RegisterReferralLinks.FindAsync(request.token);
        if (entity == null)
            throw new NotFoundException("Referral Link is incorrect");

        if (entity.Expire < _dateTime.Now)
            throw new RequestException("Link was expire");

        if (entity.InvCode.CompareTo(request.invCode)!=0)
            throw new RequestException("Invition Code is incorrect  ");


        if (entity.UserId.HasValue)
            throw new RequestException("link is expire");

        return entity;
    
    }
}

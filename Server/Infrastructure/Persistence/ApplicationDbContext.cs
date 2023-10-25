using System.Reflection;
using System.Reflection.Emit;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;

namespace test.Infrastructure.Persistence;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
        dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
        dateTime => DateOnly.FromDateTime(dateTime))
    { }
}

public class ApplicationDbContext :DbContext, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) 
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;

    }

    public DbSet<User> Users => Set<User>();
    public DbSet<RegisterReferralLink> RegisterReferralLinks => Set<RegisterReferralLink>();

    public DbSet<UserToken> UserTokens => Set<UserToken>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    //protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    //{
    //    base.ConfigureConventions(builder);
    //    builder.Properties<DateOnly>()
    //        .HaveConversion<DateOnlyConverter>();

     

    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    
    Task<IDbContextTransaction> IApplicationDbContext.BeginTransaction()
    {
        return this.Database.BeginTransactionAsync();
    }

    void IApplicationDbContext.ChangeState(object entity, EntityState state)
    {
       
        this.Entry(entity).State = state;
    }

 
}

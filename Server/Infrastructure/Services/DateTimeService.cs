using Application.Common.Interfaces;
using Domain.Common;
using System.Globalization;

namespace Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTimeOffset Now => DateTimeOffset.UtcNow;

    


}

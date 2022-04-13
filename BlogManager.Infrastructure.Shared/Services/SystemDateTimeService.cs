using BlogManager.Application.Interfaces.Shared;
using System;

namespace BlogManager.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
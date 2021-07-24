using System;
using Quran.Server.Application.Common.Interfaces;

namespace Quran.Server.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}

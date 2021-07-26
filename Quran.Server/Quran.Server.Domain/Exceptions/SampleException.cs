using System;

namespace Quran.Server.Domain.Exceptions
{
    public class SampleException : Exception
    {
        public SampleException(string code) : base($"Sample error with code {code} occured")
        {

        }
    }
}

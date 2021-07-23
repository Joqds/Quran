using System.Collections.Generic;
using System.Linq;
using Quran.Server.Domain.Common;
using Quran.Server.Domain.Exceptions;

namespace Quran.Server.Domain.ValueObjects
{
    public class SampleColourValueObject : ValueObject
    {
        private SampleColourValueObject()
        {
        }

        private SampleColourValueObject(string code)
        {
            Code = code;
        }

        public static SampleColourValueObject From(string code)
        {
            var colour = new SampleColourValueObject { Code = code };

            if (!SupportedColours.Contains(colour))
            {
                throw new SampleException(code);
            }

            return colour;
        }

        public static SampleColourValueObject White => new SampleColourValueObject("#FFFFFF");

        public static SampleColourValueObject Red => new SampleColourValueObject("#FF5733");

        public static SampleColourValueObject Orange => new SampleColourValueObject("#FFC300");

        public static SampleColourValueObject Yellow => new SampleColourValueObject("#FFFF66");

        public static SampleColourValueObject Green => new SampleColourValueObject("#CCFF99 ");

        public static SampleColourValueObject Blue => new SampleColourValueObject("#6666FF");

        public static SampleColourValueObject Purple => new SampleColourValueObject("#9966CC");

        public static SampleColourValueObject Grey => new SampleColourValueObject("#999999");

        public string Code { get; private set; }

        public static implicit operator string(SampleColourValueObject sampleColourValueObject)
        {
            return sampleColourValueObject.ToString();
        }

        public static explicit operator SampleColourValueObject(string code)
        {
            return From(code);
        }

        public override string ToString()
        {
            return Code;
        }

        protected static IEnumerable<SampleColourValueObject> SupportedColours
        {
            get
            {
                yield return White;
                yield return Red;
                yield return Orange;
                yield return Yellow;
                yield return Green;
                yield return Blue;
                yield return Purple;
                yield return Grey;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}

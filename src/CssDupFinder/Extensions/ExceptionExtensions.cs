using System;

namespace CssDupFinder.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowIfNull(this Object value, String parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void ThrowIfNull(this String value, String parameterName)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentNullException(parameterName);
        }
    }
}
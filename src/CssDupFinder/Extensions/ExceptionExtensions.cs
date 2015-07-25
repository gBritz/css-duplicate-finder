using System;
using System.Collections.Generic;

namespace CssDupFinder.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowIfNull(this Object value, String parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }
    }
}